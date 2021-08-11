using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace WebApplication1
{
    public partial class _default : System.Web.UI.Page
    {
        // Server端的變數
        public int ForJSint { get; set; } = 500;
        public bool FOrJSBOOL { get; set; } = true;
        public string ForJSString { get; set; } = "Hello World";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.hf2.Value = "伺服器端的訊息";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //this.lblName.Text = this.txt1.Text;
            this.lblName.Text = this.hf1.Value;
            // 問題：Button Click事件後，是執行頁面刷新嗎?
            // 伺服器端的警告訊息
            // MessageBox.Show("MessageBox.Show");
        }
    }
}