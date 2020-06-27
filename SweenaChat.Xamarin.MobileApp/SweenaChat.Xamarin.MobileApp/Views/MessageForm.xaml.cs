using Microsoft.AspNetCore.SignalR.Client;
using SweenaChat.Xamarin.MobileApp.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SweenaChat.Xamarin.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageForm : ContentPage
    {
        private HubConnection hubConnection;

        public Command SendMessageCommand { get; }
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }

        public ObservableCollection<string> MessageViewListPayLoad = new ObservableCollection<string>();

        public MessageForm(string name, object sender, System.EventArgs e)
        {
            InitializeComponent();
            Title = name;
            MessageEntry.Completed += StartMessage;
            SendMessageCommand = new Command(async () => { await SendMessage(sender, e); });
            ConnectCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());

            hubConnection = new HubConnectionBuilder()
                .WithUrl($"http://myapi/chatHub")
                .Build();

            hubConnection.On<string>("ReceiveMessage", message =>
            {
                
            });

            hubConnection.On<List<MessageViewModel>>("ReceiveConversation", conversations =>
            {
                MessageViewListPayLoad.Clear();

                foreach (var message in conversations)
                {
                    MessageViewListPayLoad.Add(message.MessageContent);
                }

                    MessageListView.ItemsSource = MessageViewListPayLoad;

            });


        }


        async Task Connect()
        {
            await hubConnection.StartAsync();

        }


        async Task Disconnect()
        {
            await hubConnection.StopAsync();

        }


        public async Task SendMessage(object sender, System.EventArgs e)
        {

            await Connect(); 

            var model = new MessageViewModel
            {
                MessageContent = MessageEntry.Text,
                Receiver = Title,
                Sender = await SecureStorage.GetAsync("currentUser")
            };

            await hubConnection.InvokeAsync("SendMessage", model);

            await UpdateCurrentConvo(await SecureStorage.GetAsync("currentUser"), Title);

            await Disconnect();
        }

        public async Task UpdateCurrentConvo(string user1, string user2)
        {

            await hubConnection.InvokeAsync("UpdateCurrentConvo", user1, user2);
        }

        async void StartMessage(object sender, System.EventArgs e)
        {
            await SendMessage(sender, e); 
        }

    }
}