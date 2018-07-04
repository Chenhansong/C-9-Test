using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        private static void Main(string[] args)
        {
            //List<string> list = new List<string>
            //{
            //    "One",
            //    "Two",
            //    "Three"
            //};
            //var getList = from n in list where n.StartsWith("T") select n;
            //Console.WriteLine("The First Test Result is:");
            //foreach (var x in getList)
            //{
            //    Console.WriteLine(x);
            //}
            //Console.WriteLine();
            //var ordinaryLinQList = from n in list where n=="One" select n;
            //Console.WriteLine("The Second Test Result is:");
            //foreach (var x in ordinaryLinQList)
            //{
            //    Console.WriteLine(x);
            //}

            //Console.WriteLine("Start the GetData!");
            //TestClass.GetData();
            //Console.WriteLine("End the GetData!");

            //IPAddress iPAddress = IPAddress.Parse("52.01.23.60");
            //byte[] address = iPAddress.GetAddressBytes();
            //string ipstring = iPAddress.ToString();

            //Console.WriteLine(ipstring);

            //IPHostEntry iPHostEntry = Dns.Resolve("www.baidu.com");
            //IPHostEntry iPHost = Dns.GetHostByAddress("208.215.179.178");

            //Console.WriteLine(iPHostEntry.HostName);


            //sendingMail();

            TestEmail testEmail = new TestEmail();
            testEmail.SendImgEmail("1281001690@qq.com", "2290937298@qq.com",@"E:\IT\Exercise\ConsoleApp2\ConsoleApp2\IMG\wallhaven-142977.jpg");

            Console.ReadLine();
        }

    }

    public class TestEmail
    {
        /// <summary>
        /// 发送带有图片的邮件
        /// </summary>
        /// <param name="sendAddress">发送方地址</param>
        /// <param name="receiveAddress">接受方地址</param>
        /// <param name="imgAddress">图片地址</param>
        public void SendImgEmail(string sendAddress,string receiveAddress,string imgAddress)
        {
            Bitmap bmp = new Bitmap(imgAddress);

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();

            String strbaser64 = Convert.ToBase64String(arr);
            MailMessage mm = new MailMessage(sendAddress, receiveAddress);//发送方(需要开启smpt) 接收方

            mm.Subject = "Test Email" + DateTime.Now;//标题
            mm.IsBodyHtml = true;

            string mailBody = "<BODY style=\"MARGIN: 10px\"><DIV><p>This is ChenHanSong Mail Test</p> <IMG src=\"data:image/png;base64," + strbaser64 + "\"> </IMG></DIV></BODY> ";
            mm.Body = mailBody;

            SmtpClient sc = new SmtpClient("smtp.qq.com");//利用QQ邮箱发送
            sc.Credentials = new NetworkCredential(sendAddress, "jeqvaqdsrjewbaed");//用户名和密码  密码应为开启stmp时给的授权码
            try
            {
                sc.Send(mm);
                Console.WriteLine("发送成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 发送纯文本邮件
        /// </summary>
        /// <param name="sendAddress"></param>
        /// <param name="receiveAddress"></param>
        public void SendTextEmail(string sendAddress,string receiveAddress)
        {
            MailMessage mm = new MailMessage(sendAddress, receiveAddress);//发送方(需要开启smpt) 接收方

            mm.Subject = "Test Email    " + DateTime.Now;//标题
            mm.Body = "This is ChenHanSong Test Mail";
            SmtpClient sc = new SmtpClient("smtp.qq.com");//利用QQ邮箱发送
            sc.Credentials = new NetworkCredential(sendAddress, "****");//用户名和密码  密码应为开启stmp时给的授权码
            sc.Send(mm);
        }
    }

    public class TestClass
    {
        public static async void GetData()
        {
            HttpClient httpClient = new HttpClient(new MessageHandler("error"));
            //httpClient.DefaultRequestHeaders.Add("Accept","application/json;odata=verbose");
            HttpResponseMessage httpResponse = httpClient.GetAsync("http://services.odata.org/Northwind/Northwind.svc/Regions").Result;

            Console.WriteLine(httpResponse.StatusCode);
        }
    }

    public class MessageHandler:HttpMessageHandler
    {
        string displayMessage = string.Empty;
        public MessageHandler(string msg)
        {
            displayMessage = msg;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("displayMessage is:" + displayMessage);
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            tsc.SetResult(response);
            return tsc.Task;
        }
    }

    public class AnyClass
    {
        [Obsolete("don't use Old method  use New method", true)]
        static void Old()
        {

        }

        static void New()
        {
            Type ty = typeof(double);
        }

        public AnyClass()
        {
            New();
        }
    }
}
