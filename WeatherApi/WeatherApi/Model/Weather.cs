using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WeatherApi.Model
{
    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public UriImageSource IconImage
        {
            get
            {
                return new UriImageSource
                {
                    Uri = new Uri($"https://openweathermap.org/img/wn/{Icon}@2x.png", UriKind.Absolute),
                    CachingEnabled = true,
                    CacheValidity = TimeSpan.FromDays(1)
                };
            }
        }
    }
}
