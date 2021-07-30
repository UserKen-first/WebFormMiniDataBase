using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.Session["UserLoginInfo"] != null) //是否登入過的判斷
            {
                this.PlaceHolder1.Visible = false;
                Response.Redirect("/SystemAdmin/UserInfo.aspx"); //如果登入過就強導至UserInfo頁面
            }
            else
            {
                this.PlaceHolder1.Visible = true;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //string db_account = "Admin"; //不寫死
            //string db_password = "12345";

            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPWD.Text;

            string msg;
            AuthManager.TryLogin(inp_Account, inp_PWD, out msg);

            // check empty
            if (!AuthManager.TryLogin(inp_Account, inp_PWD, out msg))
            {
                this.ltlMsg.Text = msg;
                return;
            }
            Response.Redirect("/SystemAdmin/UserInfo.aspx");

            //        // 到DB查使用者資料
            //        var dr = UserInfoManager.GetUserInfoByAccount(inp_Account); //查詢資料庫是否有這筆資料

            //        // check null
            //        if(dr == null)
            //        {
            //            this.ltlMsg.Text = "Account doesn't exist";
            //            return;
            //        }
            //        //資料如果存在，帳號密碼進行比對，帳號本身忽略大小寫比對，密碼大小寫有差異
            //        // check account / pwd
            //        if (string.Compare(dr["Account"].ToString(), inp_Account, true) == 0 &&
            //            string.Compare(dr["PWD"].ToString(), inp_PWD , false) == 0)
            //        {
            //            this.Session["UserLoginInfo"] = dr["Account"].ToString(); //現在的帳號寫進Session去
            //            Response.Redirect("/SystemAdmin/UserInfo.aspx"); //導頁
            //        }
            //        else
            //        {
            //            this.ltlMsg.Text = "Login fail. Please check Account / PWD.";
            //            return;
            //        }
        }
    }
}