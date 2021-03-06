using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)      //可能是按鈕跳回本頁，所以要判斷 postback
            {
                //if (this.Session["UserLoginInfo"] == null)   //如果尚未登入，導至豋入頁
                if (!AuthManager.IsLogined())
                {
                    Response.Redirect("/Login.aspx");  //   / ==> 意思為從route開始算
                    return;
                }
                //string account = this.Session["UserLoginInfo"] as string;
                //DataRow dr = UserInfoManager.GetUserInfoByAccount(account);

                var currentuser = AuthManager.GetCurrentUser();

                if (currentuser == null)      //如果帳號不存在，導至登入頁
                {
                    this.Session["UserLoginInfo"] = null;   //有可能帳號已被砍掉，要把session清掉
                    Response.Redirect("/Login.aspx");
                    return;
                }
                this.ItAccount.Text = currentuser.Account;
                this.ItName.Text = currentuser.Name;
                this.ItEmail.Text = currentuser.Email;
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthManager.Logout();    //登出，並導至登入頁
            //this.Session["UserLoginInfo"] = null;      //清除登入資訊，導至豋入頁
            Response.Redirect("/Login.aspx");
        }
    }
}