using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApi.Model;
using Xamarin.Forms.Maps;

namespace WeatherApi.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        public WeatherModel Weather { get; set; }
        public WeatherPage()
        {
            InitializeComponent();
            Weather = new WeatherModel();
            BindingContext = Weather;
        }

        private async void SBSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            var city = SBSearch.Text;
            Weather = await App.RequestManager.GetWeather(city);
            MoveMap();
            BindingContext = Weather;
        }
        private void MoveMap()
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(Weather.Position, Distance.FromKilometers(50)));

            map.ItemsSource = new List<WeatherModel>
            {
                new WeatherModel
                {
                    Name = Weather.Name,
                    Coord = new Coord
                    {
                        Lat = Weather.Coord.Lat,
                        Lon = Weather.Coord.Lon,
                    },
                    Sys = new Sys
                    {
                        Country = Weather.Sys.Country
                    }
                }
            };
            BindingContext = Weather;
        }
    }
}