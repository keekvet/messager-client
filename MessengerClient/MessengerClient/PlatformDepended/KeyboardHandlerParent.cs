using MessengerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MessengerClient.PlatformDepended
{
    public abstract class KeyboardHandlerParent
    {
        public bool ShortcutListenerActive { get; set; }

        public abstract void RegisterShortcut(MessageViewModel viewModel);
    }
}
