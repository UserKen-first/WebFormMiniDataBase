using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// WeatherDataHandler 的摘要描述
    /// </summary>
    public class WeatherDataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string acc = context.Request.QueryString["account"];
            string pwd = context.Request.Form["Password"];

            // 執行簡單的帳號密碼驗證

            if(acc == "Ken" && pwd == "12345678")
            {
                context.Response.ContentType = "application/json";
                
                WeatherDataModel model = WeatherDataReader.ReadData();
                model.Name += acc;      //將QueryString取得的值放入Name屬性的最後面
                
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                context.Response.Write(jsonText);
            }
            else
            {
                context.Response.StatusCode = 401;
                context.Response.End();
                // context.Response.Write("");
            }
            
            //context.Response.ContentType = "application/json"; //強制瀏覽器當成json處理
            //context.Response.Write(                            //輸出字串給瀏覽器
            //    // 指定回傳格式
            //    @"{
            //        ""Name"":""太魯閣國家公園太魯閣遊客中心"",
            //        ""T"": 20,
            //        ""Pop"": 28
            //    }"
            //);
            
            // 資料讀出後，轉成jspn再回傳
            //WeatherDataModel model = WeatherDataReader.ReadData();
            //string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            //context.Response.Write(jsonText);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}