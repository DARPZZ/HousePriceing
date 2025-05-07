using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousePriceing.Helpers
{
    public class ConnectivityTest
    {
        public ConnectivityTest() =>
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

        ~ConnectivityTest() =>
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        public bool IsThereInternetAccess { get; set; }
        public event EventHandler<bool> NetworkStatusChanged;

        public void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            bool hasInternet = e.NetworkAccess == NetworkAccess.Internet;
            IsThereInternetAccess = hasInternet;

            Console.WriteLine(hasInternet ? "Internet access is available" : "Internet access has been lost.");


            NetworkStatusChanged?.Invoke(this, hasInternet);
        }
        public bool GetfirstNetworkState ()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                return true;
            }
            return false;
        }
    }
}
