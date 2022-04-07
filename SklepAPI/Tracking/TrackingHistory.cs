using SklepAPI.Exceptions;
using SklepAPI.Tracking.InPost;
using SklepAPI.Tracking.PocztaPolska;
using SklepAPI.Tracking.TrackingModel;

namespace SklepAPI.Tracking
{
    public class TrackingHistory
    {       
        public async Task<TrackingDto?> CheckTrackingHistory(string TrackingNumber)
        {
            if (TrackingNumber.Length == 24) //InPost
            {
                InPostTrackingService tracking = new InPostTrackingService();
                var result = await tracking.GetTracking(TrackingNumber);
                return result;
            }
            else if (TrackingNumber.Length == 20) //Poczta Polska - not implemented
            {
                return null;
            }

            return null;
        }
    }
}
