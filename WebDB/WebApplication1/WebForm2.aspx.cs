using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        // 伺服器端
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string product = this.ddlProduct.SelectedValue;
            string quantilyText = this.txtQuinty.Text;

            int tempInt;  // 宣告變數裝總數量的值
            if (!int.TryParse(quantilyText, out tempInt))
            {
                this.lblWrongShow.Text = "數量請輸入大於0的整數";
                return;
            }
            
            if(tempInt <= 0)
            {
                this.lblWrongShow.Text = "數量請輸入大於0的整數";
                return;
            }
            
            // 使用switch針對prodcut的key，給予其對應的value
            switch (product)
            {
                case "001":
                    this.lblWrongShow.Text = $"橘子，共{tempInt * 50}";
                    break;
                case "002":
                    this.lblWrongShow.Text = $"草莓，共{tempInt * 150}";
                    break;
                case "003":
                    this.lblWrongShow.Text = $"梨子，共{tempInt * 540}";
                    break;

                default:
                    break;
            }
        }
    }
}