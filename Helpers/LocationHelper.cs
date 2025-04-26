using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousePriceing.Helpers
{
    public class LocationHelper
    {
        public double lati { get; set; }
        public double longi { get; set; }
        public string vejNavn { get; set; }
        public string postNummer { get; set; }
        public string HusNummer { get; set; }
        public string by { get; set; }

        public async Task<string> GetURLStringFromCordiantes(double latitude, double longitude)
        {
            IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

            Placemark placemark = placemarks?.FirstOrDefault();

            if (placemark != null)
            {
                vejNavn = placemark.Thoroughfare;
                postNummer = placemark.PostalCode;
                HusNummer = placemark.SubThoroughfare;
                by = placemark.Locality;
                return
                   $"{placemark.PostalCode}-{placemark.Locality}/{placemark.Thoroughfare}-{placemark.SubThoroughfare}";

            }

            return "";
        }
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public async Task  GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best);

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request);

                if (location != null)
                {
                    longi = location.Longitude;
                    lati = location.Latitude;
                }
            }
        
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }

    }
}
