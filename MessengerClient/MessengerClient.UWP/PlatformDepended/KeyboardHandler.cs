using MessengerClient.PlatformDepended;
using MessengerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Xamarin.Forms;

[assembly: Dependency(typeof(MessengerClient.UWP.PlatformDepended.KeyboardHandler))]
namespace MessengerClient.UWP.PlatformDepended
{
    class KeyboardHandler : KeyboardHandlerParent
    {
        private bool controlPressed = false;
        private bool enterPressed = false;

        public override void RegisterShortcut(MessageViewModel viewModel)
        {
            Window.Current.Content.PreviewKeyDown += (object sender, KeyRoutedEventArgs e) =>
            {
                switch (e.Key)
                {
                    case Windows.System.VirtualKey.Enter:
                        if (controlPressed && !enterPressed && ShortcutListenerActive)
                        {
                            viewModel.SendMessage();
                            enterPressed = true;
                        }
                        break;

                    case Windows.System.VirtualKey.Control:
                        controlPressed = true;
                        break;

                    default:
                        break;
                }
            };

            Window.Current.Content.KeyUp += (object sender, KeyRoutedEventArgs e) =>
            {

                switch (e.Key)
                {
                    case Windows.System.VirtualKey.Enter:
                        if (controlPressed)
                        {
                            viewModel.WroteMessageText = string.Empty;
                            enterPressed = false;
                        }
                        break;

                    case Windows.System.VirtualKey.Control:
                        controlPressed = false;
                        break;

                    default:
                        break;
                }

            };
        }

    }
}
