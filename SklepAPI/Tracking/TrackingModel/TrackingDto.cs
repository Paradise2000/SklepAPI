namespace SklepAPI.Tracking.TrackingModel
{
    public class TrackingDto
    {
        public string tracking_number { get; set; }
        public string type { get; set; }
        public string service { get; set; }
        public string status { get; set; }
        public List<TrackingHistoryDto> tracking_details { get; set; }
    }
}
