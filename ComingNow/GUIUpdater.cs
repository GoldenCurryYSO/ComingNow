using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MastodonClient;
using MastodonClient.Item;

namespace ComingNow
{
    class GUIUpdater : IUpdater
    {
        private Panel timelinePanel;

        private Regex reg_p = new Regex("(?<=<p>).+?(?=</p>)");
        private Regex reg_br = new Regex("<br.*?/>");
        private Regex reg_a = new Regex("^(.*?)(<a.*?</a>)(.*)$");

        private const int avatarSize = 40;

        //トゥートのIDと作成したTootPanelを関連付ける辞書。作成(Update)時に追加、削除(Delete)時に削除
        private Dictionary<string, TootPanel> dictionary = new Dictionary<string, TootPanel>();
        //既に取得したことのあるアバター画像を格納しておく辞書
        private Dictionary<string, BitmapImage> avatarTable = new Dictionary<string, BitmapImage>();

        private System.Windows.Threading.Dispatcher dispatcher;

        private HttpClient client = new HttpClient();

        public GUIUpdater(Panel timelinePanel, System.Windows.Threading.Dispatcher dispatcher)
        {
            this.timelinePanel = timelinePanel;
            this.dispatcher = dispatcher;
        }

        public async Task Update(Status toot)
        {
            if(dictionary.TryGetValue(toot.Entity_id, out var val))
            {
                return;
            }

            var tootPanel = new TootPanel();
            tootPanel.Entity_username.Text = "@" + toot.Entity_account.Entity_username;
            tootPanel.Entity_display_name.Text = toot.Entity_account.Entity_display_name;
            ContentToTextBlock(toot.Entity_content, tootPanel.Entity_content.Inlines);
            
            if (toot.Entity_media_attachments != null)
            {
                foreach (Attachment atch in toot.Entity_media_attachments)
                {
                    var link = new Hyperlink()
                    {
                        NavigateUri = new Uri(atch.Entity_url)
                    };
                    link.RequestNavigate += new System.Windows.Navigation.RequestNavigateEventHandler((obj, e) =>
                    {
                        System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
                    });
                    link.Inlines.Add(atch.Entity_text_url);
                    tootPanel.Entity_content.Inlines.Add(link);
                }
            }

            //タイムラインの更新と辞書への追加、ここまでは同期的に即座に実行
            timelinePanel.Children.Insert(0, tootPanel);           
            dictionary.Add(toot.Entity_id, tootPanel);

            //アバターの取得、非同期的に実行
            var img = await LoadAvatar(toot.Entity_account);
            await dispatcher.InvokeAsync(new Action(() =>
            {
                tootPanel.Avatar.Source = img;
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        public void Delete(string id)
        {
            try
            {
                timelinePanel.Children.Remove(dictionary[id]);
                dictionary.Remove(id);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        private void ContentToTextBlock(string str, InlineCollection inlines)
        {
            //<p>タグ部を改行タグ<br/>に置き換え
            var ms = reg_p.Matches(str);
            string result = ms[0].Value;
            for(int i = 1; i < ms.Count; i++)
            {
                result += "<br />" + ms[i].Value;
            }
            
            //文章全体を対象に<a>タグを検索し、リンクを作成する
            Match m;
            string after = result;
            while((m = reg_a.Match(after)).Success)
            {
                inlines.Add(reg_br.Replace(m.Groups[1].Value, "\n"));
                XElement xlink = XDocument.Parse(m.Groups[2].Value).Element("a");
                string uri = xlink.Attribute("href").Value;

                string text = "";

                if (xlink.Attribute("rel").Value == "tag")
                {
                    text = xlink.Value;
                }
                else
                {
                    foreach (XElement span in xlink.Elements("span"))
                    {
                        bool b = true;
                        foreach (XAttribute att in span.Attributes())
                        {
                            b = !(att.Name == "class" && att.Value == "invisible");
                        }
                        if (b)
                            text += span.Value;
                    }
                }

                var link = new Hyperlink()
                {
                    NavigateUri = new Uri(uri)
                };
                link.RequestNavigate += new System.Windows.Navigation.RequestNavigateEventHandler((obj, e) =>
                {
                    System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
                });
                link.Inlines.Add(text);
                inlines.Add(link);
                
                after = m.Groups[3].Value;
            }
            inlines.Add(reg_br.Replace(after, "\n"));
        }

        private async Task<BitmapImage> LoadAvatar(Account account)
        {
            if (avatarTable.TryGetValue(account.Entity_id, out BitmapImage img))
            {
                return img;
            }
            else
            {
                avatarTable.Add(account.Entity_id, null);

                var bytes = await client.GetByteArrayAsync(account.Entity_avatar).ConfigureAwait(false);
                using (var stream = new MemoryStream(bytes))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();

                    //var transform = new TransformedBitmap(bitmap, new ScaleTransform(avatarSize / bitmap.DpiX, avatarSize / bitmap.DpiY));
                    
                    avatarTable[account.Entity_id] = bitmap;
                    return bitmap;
                }
            }
        }
    }
}
