using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote1.SystemAdmin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
        }
    }
}