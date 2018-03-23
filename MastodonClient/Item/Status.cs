using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MastodonClient.Item
{
    [JsonObject("status")]
    public class Status
    {
        [JsonProperty("id")]
        public string Entity_id { get; set; }
        [JsonProperty("content")]
        public string Entity_content { get; set; }
        [JsonProperty("account")]
        public Account Entity_account { get; set; }

        public Status()
        {

        }
    }
}
