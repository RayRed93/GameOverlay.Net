using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using Gma.System.MouseKeyHook;
using InputTracking;

namespace AlbionFastOverlay
{
    class Program
    {
        public static IKeyboardMouseEvents _globalHook;



        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Albion Overlay v0.01 by @Reyzeee");
            
            Task tsk2 = new Task(() => {
                KeyboardMouseMonitor kmmon = new KeyboardMouseMonitor();
                kmmon.Subscribe();
            });
            tsk2.Start();

            Task tsk = new Task(() => Overlay.Start());
            tsk.Start();


            while (true)
            {
                //if (Console.ReadKey(true).Key == ConsoleKey.Spacebar) Overlay.Stop();
            }




        }

        private static void _globalHook_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("DUPa");
        }
    }
}
