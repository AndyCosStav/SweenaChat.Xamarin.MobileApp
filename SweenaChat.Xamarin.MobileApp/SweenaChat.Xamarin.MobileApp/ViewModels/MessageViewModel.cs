using System;
using System.Collections.Generic;
using System.Text;

namespace SweenaChat.Xamarin.MobileApp.ViewModels
{
    public class MessageViewModel
    {
        public string MessageContent { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
