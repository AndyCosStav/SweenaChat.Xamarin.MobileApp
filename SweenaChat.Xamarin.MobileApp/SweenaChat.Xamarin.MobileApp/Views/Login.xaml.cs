using Newtonsoft.Json;
using SweenaChat.Xamarin.MobileApp.ViewModels;
using SweenaChat.Xamarin.MobileApp.ViewModels.Token;
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

            var uri = new Uri("http://myapi/login");

            var content = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = response.Content.ReadAsStringAsync().Result;

                var token = JsonConvert.DeserializeObject<AuthTokenModel>(tokenResponse);

                await SecureStorage.SetAsync("token", token.Token);

                await SecureStorage.SetAsync("currentUser", LoginEmail.Text);

                await Navigation.PushAsync(new AllConversationsPage());


                return token.Token;
            }

            return ("User Not found");

        }

        async void NavigateToRegister(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
    }
}