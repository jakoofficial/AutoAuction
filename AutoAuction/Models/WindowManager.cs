using Avalonia.Controls;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
    public class WindowManager
    {
        //List for open Windows
        private List<Window> openWindows = new List<Window>();

        //checks if window of Type W is already open
        public bool IsWindowOpen<W>() where W : Window
        {
            return openWindows.Any(w => w.GetType() == typeof(W));
        }

        // Get the open window with the type of W
        public W GetOpenWindow<W>() where W : Window
        {
            return (W)openWindows.FirstOrDefault(w => w.GetType() == typeof(W));
        }

        //Show window W
        public void ShowWindow<W>(W window) where W : Window
        {
            if (!IsWindowOpen<W>())
            {
                openWindows.Add(window);
                // Remove the Window from the list openWindows when its closed
                // - Can act as a listener
                window.Closed += (sender, args) => openWindows.Remove(window);
                window.Show();
            }
        }
    }
}
