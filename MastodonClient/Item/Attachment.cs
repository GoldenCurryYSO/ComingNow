using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MastodonClient.Item
{
    [JsonObject("attachment")]
    public class Attachment
    {
        [JsonProperty("id")]
        public string Entity_id { get; set; }
        [JsonProperty("type")]
        public string Entity_type { get; set; }
        [JsonProperty("url")]
        public string Entity_url { get; set; }
        [JsonProperty("remote_url")]
        public string Entity_remote_url { get; set; }
        [JsonProperty("preview_url")]
        public string Entity_preview_url { get; set; }
        [JsonProperty("text_url")]
        public string Entity_text_url { get; set; }

        public Attachment()
        {

        }
    }
}
