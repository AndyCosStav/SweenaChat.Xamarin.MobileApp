using Newtonsoft.Json;
using SweenaChat.Xamarin.MobileApp.ViewModels;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SweenaChat.Xamarin.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        async void LoginUser(object sender, System.EventArgs e)
        {
            await LoginMethod(sender);
        }

        public async Task<string> LoginMethod(object sender)
        {

            var model = new LoginViewModel
            {
                UserName = LoginEmail.Text,
                Password = LoginPassword.Text
            };

            HttpClient client = new HttpClient();

            var uri = new Uri("http://10.2.0.0:45455/login");

            var content = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var token = response.Content.ReadAsStringAsync().Result;

                await SecureStorage.SetAsync("token", token);

                await SecureStorage.SetAsync("user", LoginEmail.Text);


                return token;
            }

            return ("User Not found");

        }
    }
}