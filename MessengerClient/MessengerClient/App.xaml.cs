using MessageCore.Models;
using MessengerClient.Datasource;
using MessengerClient.Models;
using MessengerClient.Views;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessengerClient
{
    public partial class App : Application
    {
        ServerConnectionHandler connectionModel;
        SqliteDatasource repository;

        public App()
        {
            InitializeComponent();

            MainPage = new LogInPage();
        }

        protected override void OnStart()
        {
            repository = new SqliteDatasource();

            connectionModel = new ServerConnectionHandler();
            connectionModel.Start();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
