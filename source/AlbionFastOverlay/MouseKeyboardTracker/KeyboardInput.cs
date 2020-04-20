using System;

namespace InputTracking
{
        public class KeyEvent : EventArgs
        {
            public Int32 keyCode;
            public DateTime time; 
            public KeyEvent(Int32 keyCode, DateTime time)
            {
                this.keyCode = keyCode;
                this.time = time;
            }
        }
    public class KeyboardInput : IDisposable
    {


        public event EventHandler<KeyEvent> KeyBoardKeyPressed;

        private WindowsHookHelper.HookDelegate keyBoardDelegate;
        private IntPtr keyBoardHandle;
        private const Int32 WH_KEYBOARD_LL = 13;
        private bool disposed;

        public KeyboardInput()
        {
            keyBoardDelegate = KeyboardHookDelegate;
            keyBoardHandle = WindowsHookHelper.SetWindowsHookEx(
                WH_KEYBOARD_LL, keyBoardDelegate, IntPtr.Zero, 0);
        }

        private IntPtr KeyboardHookDelegate(
            Int32 Code, IntPtr wParam, IntPtr lParam)
        {
            if (Code < 0)
            {
                return WindowsHookHelper.CallNextHookEx(
                    keyBoardHandle, Code, wParam, lParam);
            }

            if (KeyBoardKeyPressed != null)
                KeyBoardKeyPressed(this, new KeyEvent(Code, new DateTime(100)));
            Console.WriteLine("gdfgdfgdfgdfgdfgdfgdfg");

            return WindowsHookHelper.CallNextHookEx(
                keyBoardHandle, Code, wParam, lParam);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (keyBoardHandle != IntPtr.Zero)
                {
                    WindowsHookHelper.UnhookWindowsHookEx(
                        keyBoardHandle);
                }

                disposed = true;
            }
        }

        ~KeyboardInput()
        {
            Dispose(false);
        }
    }
}