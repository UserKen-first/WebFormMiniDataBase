using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // 伺服器端的變數
        

         public string StringObject { get; set; } // "{ \"Name\":\"123\" }"; 可用但很難維護
        
        public class Temp
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public int ForJSint { get; set; } = 500;
            public bool FOrJSBOOL { get; set; } = true;
            public string ForJSString { get; set; } = "Hello World";

            // JS 使用function將此兩變數做行為
            public int ForJSBase { get; set; } = 100;
            public int ForJSCoefficient { get; set; } = 100;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // 做序列化
            Temp temp = new Temp()
            {
                Name = "Tim",
                Age = 30
            };

            // 轉換為字串
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(temp);
            this.StringObject = jsonText;
        }
    }
}
