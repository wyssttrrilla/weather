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
    public partial class CreateOrEditCatPage : ContentPage
    {
        bool isNewCat;
        public CreateOrEditCatPage(bool isNew = false)
        {
            InitializeComponent();
            isNewCat = isNew;
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            var cat = (CatModel)BindingContext;
            await App.RequestManager.DeleteCatAsync(cat);
            await Navigation.PopAsync();
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            var cat = (CatModel)BindingContext;
            await App.RequestManager.SaveCatAsync(cat, isNewCat);
            await Navigation.PopAsync();
        }
    }
}