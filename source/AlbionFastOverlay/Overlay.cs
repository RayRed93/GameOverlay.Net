using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using InputTracking;

namespace AlbionFastOverlay
{
    public static class Overlay
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);


        public const string albionProcessName = "notepad";//"Albion-Online";
        public static GraphicsWindow window;
        public static Graphics overlayGraphics;
        public static SolidBrush redBrush;
        public static SolidBrush transparentBrush;
        public static Font consolasFont;
        //public int width = Screen.PrimaryScreen.Bounds.Width;
        //public int height = Screen.PrimaryScreen.Bounds.Height;
        public static Rect albionWindowRect;
        public static Process albionProcess;
        public static Screen albionScreen;

       

        public static void Stop()
        {
            Console.WriteLine("Overlay Stop.");
            overlayGraphics.Dispose();
            window.Dispose();
            //albionProcess.Exited -= AlbionProcess_Exited;
            //window.DestroyGraphics -= window_DestroyGraphics;
            //window.DrawGraphics -= window_DrawGraphics;
            //window.SetupGraphics -= window_SetupGraphics;
        }

        public static void Start()
        {
            

            Process[] processes = Process.GetProcessesByName(albionProcessName);//Albion-Online

            if (processes.Length > 0)
            {
                albionProcess = processes[0];
                albionProcess.Exited += AlbionProcess_Exited;
                Console.WriteLine("\nAlbion process found! \nDrawing overlay :)");
                albionWindowRect = new Rect();
                IntPtr ptr = albionProcess.MainWindowHandle;
                albionScreen = Screen.FromHandle(ptr);
                GetWindowRect(ptr, ref albionWindowRect);

            }
            else
            {
                Console.WriteLine("Albion process not found!");
                System.Threading.Thread.Sleep(5000);
                return;

            }

            var gfx = new Graphics()
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = false,
                TextAntiAliasing = false
            };

            overlayGraphics = gfx;



            window = new GraphicsWindow(albionScreen.Bounds.Left, albionScreen.Bounds.Top, albionScreen.Bounds.Width, albionScreen.Bounds.Height, gfx)
            {
                FPS = 60,
                IsTopmost = true,
                IsVisible = true
            };

            window.DestroyGraphics += window_DestroyGraphics;
            window.DrawGraphics += window_DrawGraphics;
            window.SetupGraphics += window_SetupGraphics;


            window.Create();
            window.Join();

            //System.Threading.Thread.Sleep(5000);




        }

      

        private static void window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            var gfx = e.Graphics;
            redBrush = gfx.CreateSolidBrush(255, 0, 0);
            transparentBrush = gfx.CreateSolidBrush(0, 0, 0, 0.5f);
            consolasFont = gfx.CreateFont("Consolas", 14);
        }

        private static void window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            if (albionProcess.HasExited)
            {
                return;
            }
            var gfx = e.Graphics;

            IntPtr ptr = albionProcess.MainWindowHandle;
            GetWindowRect(ptr, ref albionWindowRect);
            //
            gfx.ClearScene();
            int textPadding = 20;
            int padding = 30;
            var infoText = new StringBuilder()
            .Append("Process: ").Append(albionProcess.ProcessName.ToString().PadRight(textPadding))
            .Append("FPS: ").Append(gfx.FPS.ToString().PadRight(textPadding))
            .Append("FrameTime: ").Append(e.FrameTime.ToString().PadRight(textPadding))
            .Append("FrameCount: ").Append(e.FrameCount.ToString().PadRight(textPadding))
            .Append("DeltaTime: ").Append(e.DeltaTime.ToString().PadRight(textPadding))
            .ToString();


            var titleBarHeight = SystemInformation.CaptionHeight; //         
            gfx.DrawTextWithBackground(consolasFont, redBrush, transparentBrush, albionWindowRect.Left + padding + 10, albionWindowRect.Top + titleBarHeight + padding + 10, infoText);
            gfx.DrawRectangle(redBrush, albionWindowRect.Left + padding, albionWindowRect.Top + titleBarHeight + padding, albionWindowRect.Right - padding, albionWindowRect.Bottom - padding, 2);
            Random rnd = new Random();

            var point = new Point(albionWindowRect.Left + albionWindowRect.Width / 2, albionWindowRect.Top + albionWindowRect.Height / 2);
            gfx.DrawCrosshair(redBrush, point, 10, 1, CrosshairStyle.Gap);
        }

        private static void AlbionProcess_Exited(object sender, EventArgs e)
        {
            Console.WriteLine("Albion Process Exited...");
            Stop();
        }

        private static void window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            redBrush.Dispose();
            consolasFont.Dispose();
        }
    }
}
