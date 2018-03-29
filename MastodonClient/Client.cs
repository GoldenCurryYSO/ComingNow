using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MastodonClient
{
    public class Client
    {
        private IUpdater updater;
        private HttpClient client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5.0)
        };

        private const string info_path = "account";

        private string host;
        private const string redirect_uri = "urn:ietf:wg:oauth:2.0:oob";
        private const string client_id = "53894874fb989236d4b3024f790abf806bba5d776cae0542b0ee5bd6a2c48388";
        private const string client_secret = "568937c77c34638b4b952af5c07a925b6c54c99c5b6ec4c50b4736577835ba21";

        private string token;

        private const int BUFFER_SIZE = 4096;

        private bool streaming = false;
        public bool Streaming
        {
            get
            {
                return streaming;
            }
        }

        public Client(string host, IUpdater updater)
        {
            this.host = host;
            this.updater = updater;

            try
            {
                if (!File.Exists(info_path))
                {
                    StreamWriter writer = new StreamWriter(info_path, false);
                }

                using (StreamReader reader = new StreamReader(info_path))
                {
                    token = reader.ReadLine();
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("Error occured while reading file 'account'.");
                Console.Error.WriteLine(e);
            }
        }

        /// <summary>
        /// 認証画面を開く
        /// </summary>
        static public void Authorization(string host)
        {
            System.Diagnostics.Process.Start("https://" + host + "/oauth/authorize?"
                + "response_type=" + "code"
                + "&client_id=" + client_id
                + "&scope=" + "read write follow"
                + "&redirect_uri=" + redirect_uri); //"urn%3Aietf%3Awg%3Aoauth%3A2.0%3Aoob");
        }

        /// <summary>
        /// 認証コードをマストドンサーバーに送信し、アクセストークンを入手する（結果はinfo_pathのファイルに書き込む）
        /// </summary>
        /// <param name="code">認証コード（認証画面に表示）</param>
        static public async Task<bool> GetAccessToken(string host, string code)
        {
            string url = "https://" + host + "/oauth/token";
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code"},
                { "redirect_uri", redirect_uri},
                { "client_id", client_id },
                { "client_secret", client_secret},
                { "scope", "read write follow" },
                { "code", code }
            });
            try
            {
                HttpResponseMessage res = await (new HttpClient()).PostAsync(url, content);
                if(res.IsSuccessStatusCode)
                {
                    //Console.WriteLine(res.ToString());
                    using (StreamReader reader = new StreamReader(await res.Content.ReadAsStreamAsync()))
                    {
                        string str = "";
                        //Regex rgx = new Regex("\"access_token\":\"<token>\"");
                        while ((str = reader.ReadLine()) != null)
                        {
                            try
                            {
                                JObject obj = JObject.Parse(str);
                                using (StreamWriter writer = new StreamWriter(info_path, false))
                                {
                                    writer.Write((string)obj["access_token"]);
                                }
                                return true;
                            }
                            catch (Exception) { }
                            /*
                            Match m = rgx.Match(str);
                            if (m.Success)
                            {
                                
                            }
                            */
                        }
                    }
                    return false;
                }
                else
                {
                    Console.Error.WriteLine("アクセストークンの取得に失敗しました。");
                    Console.Error.WriteLine(await res.Content.ReadAsStringAsync());
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
                return false;
            }
        }

        public async Task GetLocalTimeline()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("https://{0}/api/v1/timelines/public?local=true", host))
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage res;
            try
            {
                res = await client.SendAsync(request);

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return;
            }

            if (!res.IsSuccessStatusCode)
            {
                Console.Error.WriteLine("{0} への接続に失敗しました。");           
                Console.Error.WriteLine(res.ToString());
                return;
            }

            StreamReader reader = new StreamReader(await res.Content.ReadAsStreamAsync());
            List<Item.Status> statusList = JsonConvert.DeserializeObject<List<Item.Status>>(await reader.ReadLineAsync());
            statusList.Reverse();
            foreach(Item.Status s in statusList)
            {
                try
                {
                    await updater.Update(s);
                }
                catch(Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
            return;
        }

        public async Task StreamingLocalTimeline()
        {
            ClientWebSocket ws = new ClientWebSocket();
            try
            {
                await ws.ConnectAsync(new Uri(string.Format("ws://{0}/api/v1/streaming/?access_token={1}&stream=public:local", host, token)), CancellationToken.None);
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("{0} への接続に失敗しました。", host);
                Console.Error.WriteLine(e);
                return;
            }
            
            var utf = new UTF8Encoding();
            streaming = true;
            while(ws.State == WebSocketState.Open)
            {
                var buffer = new byte[BUFFER_SIZE];
                var segment = new ArraySegment<byte>(buffer);
                WebSocketReceiveResult recieved = await ws.ReceiveAsync(segment, CancellationToken.None);
                if (recieved.EndOfMessage)
                {
                    string str = utf.GetString(buffer, 0, recieved.Count);
                    try
                    {
                        JObject obj1 = JObject.Parse(str);
                        switch ((string)obj1["event"])
                        {
                            case ("update"):
                                JObject payload = JObject.Parse((string)obj1["payload"]);
                                Item.Status status = JsonConvert.DeserializeObject<Item.Status>((string)obj1["payload"]);
                                updater.Update(status);
                                break;
                            case ("delete"):
                                string id = (string)obj1["payload"];
                                updater.Delete(id);
                                break;
                        }
                        
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Error: at parsing {0}", str);
                        Console.Error.WriteLine(e);
                    }
                }
                else
                {
                    Console.Error.WriteLine("recieved message is too long.");
                }

                
            }

            streaming = false;
            Console.WriteLine("Streaming end");
        }

        public async Task<bool> PostToot(string content, List<Item.Attachment> attachments)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(string.Format("https://{0}/api/v1/statuses", host)),
                Content = new FormUrlEncodedContent(new Dictionary<string, string>{
                    {"status" , content }
                })
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage res;
            try
            {
                res = await client.SendAsync(request);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return false;
            }

            if (!res.IsSuccessStatusCode)
            {
                Console.Error.WriteLine("トゥートに失敗しました。");
                Console.Error.WriteLine(res.Headers);
                Console.Error.WriteLine(await res.Content.ReadAsStringAsync());
                return false;
            }

            return true;
        }
    }
}
