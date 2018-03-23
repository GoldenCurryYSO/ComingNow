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

        private string server_url = "imastodon.net";
        private string client_id = "53894874fb989236d4b3024f790abf806bba5d776cae0542b0ee5bd6a2c48388";
        private string client_secret = "568937c77c34638b4b952af5c07a925b6c54c99c5b6ec4c50b4736577835ba21";

        private string token = "6357890c3ed50bbd0b3fdfa06744827fea9aaf9dee01f19ab7194de783e56bf0";

        private const int BUFFER_SIZE = 4096;

        public Client(IUpdater updater)
        {
            this.updater = updater;
        }

        /// <summary>
        /// 認証画面を開く
        /// </summary>
        public void Authorization()
        {
            System.Diagnostics.Process.Start("https://" + server_url + "/oauth/authorize?"
                + "response_type=" + "code"
                + "&client_id=" + client_id
                + "&redirect_uri=" + "urn%3Aietf%3Awg%3Aoauth%3A2.0%3Aoob");
        }

        /// <summary>
        /// 認証コードをマストドンサーバーに送信し、アクセストークンを入手する
        /// </summary>
        /// <param name="code">認証コード（認証画面に表示）</param>
        /// <returns>アクセストークン</returns>
        public async Task<string> GetAccessToken(string code)
        {
            string url = "https://" + server_url + "/oauth/token";
            //string url = "http://localhost:8000";
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code"},
                { "redirect_uri", "urn:ietf:wg:oauth:2.0:oob"},//"urn%3Aietf%3Awg%3Aoauth%3A2.0%3Aoob"},
                { "client_id", client_id },
                { "client_secret", client_secret},
                { "scope", "read write follow" },
                { "code", code }
            });
            try
            {
                HttpResponseMessage res = await client.PostAsync(url, content);
                if(res.IsSuccessStatusCode)
                {
                    //Console.WriteLine(res.ToString());
                    using (StreamReader reader = new StreamReader(await res.Content.ReadAsStreamAsync()))
                    {
                        string str = "";
                        while((str = reader.ReadLine()) != null)
                        {
                            Regex rgx = new Regex("\"access_token\":\"?<token>\"");
                            Match m = rgx.Match(str);
                            if (m.Success)
                            {
                                code = m.Groups["token"].Value;
                                Console.WriteLine("access_token <- {0}", code);
                            }
                        }
                    }
                    return await res.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.Error.WriteLine("アクセストークンの取得に失敗しました。");
                    Console.Error.WriteLine(await res.Content.ReadAsStringAsync());
                    return "";
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
                return "";
            }
        }

        public async Task GetLocalTimeline()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("https://{0}/api/v1/timelines/public?local=true", server_url))
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
                await updater.Update(s);
            }
            return;
        }

        public async Task StreamingLocalTimeline()
        {
            ClientWebSocket ws = new ClientWebSocket();
            try
            {
                await ws.ConnectAsync(new Uri(string.Format("ws://{0}/api/v1/streaming/?access_token={1}&stream=public:local", server_url, token)), CancellationToken.None);
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("{0} への接続に失敗しました。", server_url);
                Console.Error.WriteLine(e);
                return;
            }
            
            var utf = new UTF8Encoding();
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

            Console.WriteLine("Streaming end");
        }
    }
}
