using Newtonsoft.Json;

namespace SklepAPI.Tracking.InPost
{
    public class InPostTrackingDto
    {
        public string tracking_number { get; set; }
        public string type { get; set; }
        public string service { get; set; }
        public string status { get; set; }
        //[JsonProperty("tracking_details")]
        public List<InPostTracking_detailsDto> tracking_details { get; set; }
    }
}
