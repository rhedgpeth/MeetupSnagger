using Newtonsoft.Json;

namespace MeetupSnagger.Models
{
    public class Group
    {
        public double Score { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Link { get; set; }
        public string UrlName { get; set; }
        public string Description { get; set; }
        public long Created { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [JsonProperty(PropertyName = "localized_country_name")]
        public string LocalizedCountryName { get; set; }

        [JsonProperty(PropertyName = "localized_location")]
        public string LocalizedLocation { get; set; }

        public string State { get; set; }

        [JsonProperty(PropertyName = "join_mode")]
        public string JoinMode { get; set; }

        public string Visibility { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }

        public int Members { get; set; }
        public string Timezone { get; set; }

        public NextEvent Next_Event { get; set; }

        public Category Category { get; set; }

        [JsonProperty(PropertyName = "meta_category")]
        public MetaCategory Meta_Category { get; set; }
    }
}
