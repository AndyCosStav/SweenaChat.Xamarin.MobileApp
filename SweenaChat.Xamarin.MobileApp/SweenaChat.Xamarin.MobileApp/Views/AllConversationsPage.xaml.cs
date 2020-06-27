using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SweenaChat.Xamarin.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllConversationsPage : ContentPage
    {
        public AllConversationsPage()
        {
            InitializeComponent();
        }

        async void NavigateToSearchAllUsers(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new UsersPage());
        }


        async void GetAllConvos()
        {

        }
    }

}