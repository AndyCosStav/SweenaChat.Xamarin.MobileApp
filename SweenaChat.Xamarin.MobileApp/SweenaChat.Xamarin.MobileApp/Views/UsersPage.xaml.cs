using Newtonsoft.Json;
using SweenaChat.Xamarin.MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SweenaChat.Xamarin.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {

        public UsersPage()
        {
            InitializeComponent();
            EntryUsername.Completed += searchUser;
            
        }

        string loadedUser { get; set; }

        async void searchUser(object sender, System.EventArgs e)
        {
            var username = EntryUsername.Text;

            List<UserViewModel> UsersList = new List<UserViewModel>();

            List<string> ListViewPayload = new List<string>();

            HttpClient client = new HttpClient();

            string url = "http://myapi/api/user/getuserbyname?username="+username;

            var res = await client.GetAsync(url);

            var user = JsonConvert.DeserializeObject<UserViewModel>(res.Content.ReadAsStringAsync().Result);

            if (user!= null)
            {
                UsersList.Add(user);

                foreach (UserViewModel u in UsersList)
                {
                    ListViewPayload.Add(user.Username);
                    loadedUser = user.Username;
                }
                usersListView.ItemsSource = ListViewPayload;        
            }

            else
            {
                usersListView.ItemsSource = new string[] { "user not found" };
            }

        }


        async void SelectUserForMessage(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MessageForm(loadedUser, sender, e)); 
        }

    }
}