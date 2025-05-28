using CommunityToolkit.Mvvm.Messaging;
using HousePriceing.Helpers;
using Mapsui.Layers; 
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.Utilities;

namespace HousePriceing.Views;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
       
        BindingContext = viewModel;
        WeakReferenceMessenger.Default.Register<GenericValueChangedMessage<(double, double)>>(this, (r, message) =>
        {
            MapView.Map?.Layers.Add(OpenStreetMap.CreateTileLayer());
            var (lat, lon) = message.Value;
            ShowAndPlaceMap(lat, lon);
        });
    }

    private void ShowAndPlaceMap(double userPinLongitude, double userPinLattitude)
    {

        var pinLonLatTuple = SphericalMercator.FromLonLat(userPinLongitude, userPinLattitude);
        Mapsui.MPoint pinPoint = new Mapsui.MPoint(pinLonLatTuple.x, pinLonLatTuple.y);
        var pinFeature = new PointFeature(pinPoint);
        var pinStyle = new SymbolStyle
        {
            Fill = new Mapsui.Styles.Brush(Mapsui.Styles.Color.HotPink),
            Outline = new Mapsui.Styles.Pen(Mapsui.Styles.Color.Black, 2),
            SymbolScale = 0.5,
    

        };

        pinFeature.Styles.Add(pinStyle);

        var pinLayer = new MemoryLayer("PinLayer")
        {
            Features = new[] { pinFeature },
            Style = null
        };

        MapView.Map.Layers.Add(pinLayer);
        var sphericalMercatorPoint = new Mapsui.MPoint(pinLonLatTuple.x, pinLonLatTuple.y);
        MapView.Map.Navigator.CenterOn(sphericalMercatorPoint);
      

        MapView.Map.Navigator.ZoomTo(1); 

    }

}
