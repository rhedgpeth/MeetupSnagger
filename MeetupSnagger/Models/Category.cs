using Newtonsoft.Json;

namespace MeetupSnagger.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "sort_name")]
        public string SortName { get; set; }
    }
}
