using System.Collections.Generic;
using Newtonsoft.Json;

namespace MeetupSnagger.Models
{
    public class MetaCategory
    {
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "sort_name")]
        public string SortName { get; set; }

        [JsonProperty(PropertyName = "category_ids")]
        public List<int> CategoryIds { get; set; }
    }
}
