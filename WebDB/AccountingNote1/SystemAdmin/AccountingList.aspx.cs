using AccountingNote.Auth;
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
                this.gvAccountingList.DataSource = dt;
                this.gvAccountingList.DataBind();
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.PlcNoData.Visible = true;
            }
            this.gvAccountingList.DataSource = dt;
            this.gvAccountingList.DataBind();
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