using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace TryReadWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            // string jspnText = client.DownloadString("https://apiservice.mol.gov.tw/OdService/download/A17010000J-000135-Sfk");

            // 送一個HTTP Request到伺服器，伺服器再回傳Response內容給瀏覽器
            byte[] sourceByte = client.DownloadData("https://apiservice.mol.gov.tw/OdService/rest/datastore/A17010000J-000135-MOZ");
            string jspnText = Encoding.UTF8.GetString(sourceByte);  // 下載位元組陣列，強制轉換成指定編碼的字串，再輸出


            //byte[] sourceByte = client.DownloadData("https://www.google.com.tw/");
            //string jspnText = Encoding.UTF8.GetString(sourceByte);
            // string jspnText = client.DownloadString("https://www.google.com.tw/");

            Rootobject obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(jspnText);  //將json字串，轉換型別，再反序列化

            foreach (var item in obj.result.records)   // 把裡面所有內容跑過一圈
            {
                Console.WriteLine(item.年度);
            }

            //Console.WriteLine(jspnText);
            Console.ReadLine();
        }
        // {"success":true,"result":{"resource_id":"A17010000J-000135-MOZ","records":
        // [{"年度":"2009","平均年齡-男":"34.77","平均年齡-女":"31.28"},
        // {"年度":"2010","平均年齡-男":"35.06","平均年齡-女":"31.63"},
        // { "年度":"2011","平均年齡-男":"35.27","平均年齡-女":"31.85"},
        // { "年度":"2012","平均年齡-男":"36.2","平均年齡-女":"32.05"},
        // { "年度":"2013","平均年齡-男":"36.4","平均年齡-女":"32.48"},
        // { "年度":"2014","平均年齡-男":"36.33","平均年齡-女":"32.78"},
        // { "年度":"2015","平均年齡-男":"36.31","平均年齡-女":"33.65"},
        // { "年度":"2016","平均年齡-男":"36.61","平均年齡-女":"33.49"},
        // { "年度":"2017","平均年齡-男":"35.96","平均年齡-女":"33.39"},
        // { "年度":"2018","平均年齡-男":"35.67","平均年齡-女":"33.49"},
        // { "年度":"2019","平均年齡-男":"35.52","平均年齡-女":"33.57"},
        // { "年度":"2020","平均年齡-男":"35.55","平均年齡-女":"33.37"}]}}

        // 反序列化須先建立型別
        //public class Temp
        //{
        //    public bool success { get; set; }

        //    public Temp2 result { get; set; }
        //}

        //public class Temp2
        //{
        //    public string resource_id { get; set; }
        //    public List<Temp3> records { get; set; }
        //    // 反序列化為list
        //}
        //public class Temp3
        //{
        //    public string 年度 { get; set; }
        //}

        // 由網站的json原始碼複製後，Visual Studio 編輯 > 選擇性貼上 > json類別
        public class Rootobject
        {
            public bool success { get; set; }
            public Result result { get; set; }
        }
        public class Result
        {
            public string resource_id { get; set; }
            public Record[] records { get; set; }
        }

        public class Record
        {
            public string 年度 { get; set; }
            public string 平均年齡男 { get; set; }
            public string 平均年齡女 { get; set; }
        }
    }
}
