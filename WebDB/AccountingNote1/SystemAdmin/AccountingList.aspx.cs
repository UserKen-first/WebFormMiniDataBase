﻿using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check is logined
            //if(this.Session["UserLoginInfo"] == null)
            if(!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var currentUser = AuthManager.GetCurrentUser();
            //string account = this.Session["UserLoginInfo"] as string;
            //var dr = UserInfoManager.GetUserInfoByAccount(account);

            if(currentUser == null)
            // 帳號不存在，導至登入頁
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }
            // Read Accounting data
            var dt = AccountingManager.GetAccountingList(currentUser.ID);
            
            if(dt.Rows.Count > 0)    // check is empty data
            {
                int totalPages = this.GetTotalpage(dt); //讀取現在的總頁數
                var dtPaged = this.GetPagedDataTable(dt);

                this.gvAccountingList.DataSource = dtPaged;  //資料繫結
                this.gvAccountingList.DataBind();

                var pages = (dt.Rows.Count / 10);   //分頁數量: 筆數/10
                if (dt.Rows.Count % 10 > 0)
                    pages += 1;


                this.ltPage.Text = $"{dt.Rows.Count}筆，共{pages}頁，目前在第{this.GetCurrentPage()}頁";
                
                for(var i = 1; i <= totalPages; i++)   //分頁超連結
                {
                    this.ltPage.Text += $"<a href='AccountingList.aspx?page={i}'>{i}</av>&nbsp";
                }
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.PlcNoData.Visible = true;
            }
            //this.gvAccountingList.DataSource = dt;   //如沒註解，會顯示10筆以上的資料，因其跳過dt審核條件，導致資料全部顯示
            //this.gvAccountingList.DataBind(); 
        }
        
        private int GetTotalpage(DataTable dt)
        {
            int pagers = dt.Rows.Count / 10;

            if ((dt.Rows.Count % 10) > 0)
                pagers += 1;

            return pagers;

            // 1  --> 0
            // 9  --> 0
            // 10 --> 1
            // 15 --> 1
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
        
        private DataTable GetPagedDataTable(DataTable dt)
        {
            DataTable dtPaged = dt.Clone();  //為不複製資料，複製欄位
            //dt.Copy();                    //如果資料為0筆，會出錯

            // 複製現有的資料並回傳
            //foreach(DataRow dr in dt.Rows)

            int startIndex = (this.GetCurrentPage() - 1) * 10;
            int endIndex = this.GetCurrentPage() * 10;

            if (endIndex > dt.Rows.Count)   //預防索引值超過範圍
                endIndex = dt.Rows.Count;

            for(var i = startIndex; i < endIndex; i++)      //只拿出前10筆的資料
            {
                DataRow dr = dt.Rows[i];
                var drNew = dtPaged.NewRow();
                
                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }
                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }

        protected void btnAddAcc_Click1(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }
        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            if (row.RowType == DataControlRowType.DataRow)
            {
                //Literal ltl = row.FindControl("ItActType") as Literal;
                Label lbl = row.FindControl("lbltype") as Label;
                //ltl.Text = "OK";

                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType"); //確定裡面裝的是整數，可使用此方法轉型

                if (actType == 0)
                {
                    //ltl.Text = "支出";
                    lbl.Text = "支出";
                }
                else
                {
                    //ltl.Text = "收入";
                    lbl.Text = "收入";
                }

                if (dr.Row.Field<int>("Amount") > 1500)
                {
                    if (dr.Row.Field<int>("Amount") < 500000)
                    {
                        lbl.ForeColor = Color.Red;
                    }
                    else
                    {
                        lbl.ForeColor = Color.Blue;
                    }
                }
            }
        }
    }
}