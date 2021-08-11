using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote1.SystemAdmin.UserControl
{
    public partial class ucPager : System.Web.UI.UserControl
    {
        //先完成4個屬性的宣告
        public string Url { get; set; }
        /// <summary> 頁面url /// </summary>
        public int Totalsize { get; set; }
        /// <summary> 總筆數 /// </summary>
        public int PageSize { get; set; }
        /// <summary> 頁面筆數 /// </summary>
        public int CurrentPage { get; set; }
        /// <summary> 當前頁數 /// </summary>
         
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Bind();
        }

        public void Bind()
        {
            int totalPages = this.GetTotalpage();
            this.ltpager.Text = $"共{this.Totalsize}筆，共{totalPages}頁，目前在第{ this.GetCurrentPage()} 頁<br/>";

            for (var i = 1; i <= totalPages; i++)
            {
                this.ltpager.Text += $"<a href='{this.Url}?page={i}'>{i}</a>&nbsp;";
            }
        }
        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];
            // 取得目前是第幾頁
            if (string.IsNullOrWhiteSpace(pageText))
                return 1;    //空字串回傳1
            int intpage;
            if (!int.TryParse(pageText, out intpage))
                return 1;    //數字轉換失敗
            if (intpage <= 0)
                return 1;

            return intpage;
        }
        private int GetTotalpage()
        {
            int pagers = this.Totalsize / this.PageSize;

            if ((this.Totalsize % this.PageSize) > 0)
                pagers += 1;

            return pagers;
        }
    }
}