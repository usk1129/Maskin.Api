using Newtonsoft.Json;
using System;

namespace Maskin.Client.Models
{
    public class machine
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }
        [JsonProperty(PropertyName = "status")]
        public bool Status { get; set; }
    }
}
