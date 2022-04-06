using Flurl.Http;
using Newtonsoft.Json;
using SklepAPI.Exceptions;
using SklepAPI.Tracking.TrackingModel;

namespace SklepAPI.Tracking.InPost
{
    public class InPostTrackingService
    {
        public async Task<TrackingDto?> GetTracking (string TrackingNumber)
        {
            try
            {
                var posts = await $"https://api-shipx-pl.easypack24.net/v1/tracking/{TrackingNumber}"
                .GetAsync()
                .ReceiveJson<InPostTrackingDto>();

                TrackingDto Tracking = new TrackingDto()
                {
                    tracking_number = posts.tracking_number,
                    type = posts.type,
                    service = posts.service,
                    status = posts.status,
                    tracking_details = posts.tracking_details.Select(p => new TrackingHistoryDto()
                    {
                        status = p.status,
                        agency = p.agency,
                        datetime = p.datetime
                    }).ToList()
                };

                return Tracking;
            }
            catch
            {
                return null;
            }
            
        }
    }
}
