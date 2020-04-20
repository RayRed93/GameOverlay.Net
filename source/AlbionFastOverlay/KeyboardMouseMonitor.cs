using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbionFastOverlay
{
    public class KeyboardMouseMonitor
    {

        private IKeyboardMouseEvents _globalHook;


        public void Subscribe()
        {
            if (_globalHook == null)
            {
                // Note: for the application hook, use the Hook.AppEvents() instead
                _globalHook = Hook.GlobalEvents();
                _globalHook.KeyPress += GlobalHookKeyPress;
                Console.WriteLine("Key hook registred");
            }
            else
            {
                Console.WriteLine("global hook already used");

            }
        }

        private static void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }

        public void Unsubscribe()
        {
            if (_globalHook != null)
            {
                _globalHook.KeyPress -= GlobalHookKeyPress;
                _globalHook.Dispose();
            }
        }
    }
}
