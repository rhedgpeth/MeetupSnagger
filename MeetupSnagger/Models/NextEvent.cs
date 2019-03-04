using Newtonsoft.Json;

namespace MeetupSnagger.Models
{
    public class NextEvent
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "yes_rsvp_count")]
        public int YesRSVPCount { get; set; }

        public long Time { get; set; }

        [JsonProperty(PropertyName = "utc_offset")]
        public int UTCOffset { get; set; }
    }
}
