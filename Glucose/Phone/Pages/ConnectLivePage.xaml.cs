using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Live;

namespace Rosier.Glucose.Phone.Pages
{
    public partial class ConnectLivePage : ConnectLiveTypedPage
    {
        private LiveConnectClient liveClient;

        public ConnectLivePage()
            : base()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
        }

        private async void liveSignIn_SessionChanged(object sender, Microsoft.Live.Controls.LiveConnectSessionChangedEventArgs e)
        {
            if (e.Status == LiveConnectSessionStatus.Connected)
            {
                liveClient = new LiveConnectClient(e.Session);
                LiveOperationResult operationResult = await liveClient.GetAsync("me");
                try
                {
                    dynamic meResult = operationResult.Result;
                    if (meResult.first_name != null && meResult.last_name != null)
                    {
                        infoTextBlock.Text = "Hello " + meResult.first_name + " " + meResult.last_name + "!";
                    }
                    else
                    {
                        infoTextBlock.Text = "Hello, signed-in user!";
                    }
                }
                catch (LiveConnectException exception)
                {
                    this.infoTextBlock.Text = "Error calling API: " + exception.Message;
                }
            }
            else
            {
                infoTextBlock.Text = "Not signed in.";
            }
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LiveAuthClient auth = new LiveAuthClient(this.ViewModel.LiveClientId);
                LiveLoginResult loginResult = await auth.LoginAsync(new string[] { "wl.signin", "wl.basic" });
                if (loginResult.Status == LiveConnectSessionStatus.Connected)
                {
                    this.infoTextBlock.Text = "Signed in.";
                }
            }
            catch (LiveConnectException exception)
            {
                this.infoTextBlock.Text = "Error calling API: " + exception.Message;
            }
        }
    }
}