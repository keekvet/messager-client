using MessengerClient.PlatformDepended;
using MessengerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(MessengerClient.iOS.PlatformDepended.KeyboardHandler))]
namespace MessengerClient.iOS.PlatformDepended
{
    class KeyboardHandler : KeyboardHandlerParent
    {
        public override void RegisterShortcut(MessageViewModel viewModel){}

    }
}
