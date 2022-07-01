using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CatApi.Model;

namespace CatApi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatsListPage : ContentPage
    {
        public CatsListPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            LVCats.ItemsSource = await App.RequestManager.GetCats();
        }

        private async void ToolbaItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateOrEditCatPage(true)
            {
                BindingContext = new CatModel()
                {
                    Id = Guid.NewGuid().ToString(),
                }
            });
        }

        private void LVCats_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new CreateOrEditCatPage()
            {
                BindingContext = e.SelectedItem as CatModel
            });
        }
    }
}