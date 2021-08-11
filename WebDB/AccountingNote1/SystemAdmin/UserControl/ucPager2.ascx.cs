using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote1.SystemAdmin.UserControl
{
    public partial class ucPager2 : System.Web.UI.UserControl
    {
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

        }

        private int GetCurrentPage()
        {
            string pageText = this.Request.QueryString["Page"];  //0804問題，["Page"]如何宣告的，頁面的id

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int pageIndex = 0;   //如果不是空字串做TryParse

            if (!int.TryParse(pageText, out pageIndex))  //轉換成功回傳現在第幾頁
                return 1;                                //轉換失敗回傳預設值

            return pageIndex;
        }
        // 分頁控制項不連資料庫，用讀取的資料作判斷
        public void Bind()   // 由外界決定 計算頁數及超連結
        {
            if (this.PageSize <= 0)
                throw new DivideByZeroException();   //除數為0時發生錯誤

            int totalPage = this.Totalsize / this.PageSize;  //整數相除會捨去餘數的部分 // 總共有幾頁
            if (this.Totalsize % this.PageSize > 0)
                totalPage += 1;          // 有餘數的情況下總頁數會加一

            //先行處理兩個連結(aaa.aspx?page=1) //  希望其讀到的超連結
            this.aLinkFirst.HRef = $"{this.Url}?page=1";
            this.aLinkLast.HRef = $"{this.Url}?page={totalPage}";
            
            // 問題：頁面的URL連結及其?Page為用程式碼寫完丟上去的嗎

            //this.aLink1.HRef = "https://www.google.com.tw/"; //改變連結
            //this.aLink1.InnerText = "50";                    //改內容輸出

            this.CurrentPage = this.GetCurrentPage();
            this.ItCurrentPage.Text = this.CurrentPage.ToString(); 
            
            //if (this.CurrentPage == 1)
            //{
            //    this.aLink1.Visible = false;
            //    this.aLink2.Visible = false;

            //    this.aLink3.HRef = "";   // 自己不需要再連到自己
            //}
            //else if (this.CurrentPage == totalPage) //自己的頁面是最後一頁，後面兩頁做隱藏
            //{
            //    this.aLink4.Visible = false;
            //    this.aLink5.Visible = false;

            //    this.aLink3.HRef = "";
            //}
            
            //else  (this.CurrentPage == (totalPage - 1))
            //{
            //    this.aLink4.Visible = false;
            //}
            
            //else if (this.CurrentPage == 2)
            //{
            //    this.aLink1.Visible = false;
            //    this.aLink3.Visible = false;
            //}

            //else取消 不管上面如何下面皆須計算筆數    //不是第一筆也不是最後一筆的時候 先算小於自己的兩頁
            
            // 計算頁數
            int prevM1 = this.CurrentPage - 1;
            int prevM2 = this.CurrentPage - 2;
            int nextP1 = this.CurrentPage + 1;
            int nextP2 = this.CurrentPage + 2;

            this.aLink2.HRef = $"{this.Url}?page={prevM1}";   // $"{this.Url}?page={prev1}"用法確認，?的意思 = where
            this.aLink2.InnerText = prevM1.ToString();
            this.aLink1.HRef = $"{this.Url}?page={prevM2}";
            this.aLink1.InnerText = prevM2.ToString();

            this.aLink4.HRef = $"{this.Url}?page={nextP1}";
            this.aLink4.InnerText = nextP1.ToString();
            this.aLink5.HRef = $"{this.Url}?page={nextP2}";
            this.aLink5.InnerText = nextP2.ToString();

            //this.aLink3.InnerText = this.CurrentPage.ToString();


            //依頁數，決定是否隱藏超連結，並處理提示文字
            //this.aLink1.Visible = (prevM2 > 0);
            //this.aLink2.Visible = (prevM1 > 0);
            //this.aLink4.Visible = (nextP1 <= totalPage);
            //this.aLink5.Visible = (nextP2 <= totalPage);
            
            //依頁數，決定是否隱藏超連結，並處理提示文字
            if (prevM2 <= 0)  // 前一筆 <= 0
                this.aLink1.Visible = false;

            if (prevM1 <= 0)
                this.aLink2.Visible = false;

            if (nextP1 > totalPage)   // 大於上限才做隱藏
                this.aLink4.Visible = false;

            if (nextP2 > totalPage)
                this.aLink5.Visible = false;

            this.ItPager.Text = $"共{ this.Totalsize}，共{totalPage}頁，" +
                $"目前在第{this.GetCurrentPage()} 頁<br/>";

            //if(nextP1 > this.CurrentPage)
            //    this.aLink4.Visible = false;

            //if (nextP2 > this.CurrentPage)
            //    this.aLink5.Visible = false;


        }
    }
}
