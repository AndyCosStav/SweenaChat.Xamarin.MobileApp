using Newtonsoft.Json;
using SweenaChat.Xamarin.MobileApp.ViewModels;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SweenaChat.Xamarin.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }
        
        async void RegisterUser(object sender, System.EventArgs e)
        {
            var model = new RegisterViewModel
            {
                Email = EntryEmail.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Password = EntryPasswrd.Text

            };

            var httpClient = new HttpClient();
            var content = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var uri = new Uri("http://10.2.0.0:45455/register");
            HttpResponseMessage response = await httpClient.PostAsync(uri, httpContent);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                string ss = data;
            }


        }

    }
}