using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MastodonClient.Item
{
    [JsonObject("account")]
    public class Account
    {
        [JsonProperty("id")]
        public string Entity_id { get; set; }
        [JsonProperty("username")]
        public string Entity_username { get; set; }
        [JsonProperty("display_name")]
        public string Entity_display_name { get; set; }
        [JsonProperty("avatar")]
        public string Entity_avatar { get; set; }

        public Account()
        {

        }
    }
}
