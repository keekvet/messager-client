﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessengerClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCell : ViewCell
    {
        public UserCell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //lastMessage.Text = 
        }
    }
}