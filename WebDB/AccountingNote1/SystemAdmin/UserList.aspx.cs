using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote1.SystemAdmin
{
    public partial class Userlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccountingNote.Auth.AuthManager.IsLogined())
            {
                return;
            }

            var cUser = AccountingNote.Auth.AuthManager.GetCurrentUser();

            this.GridView1.DataSource = AccountingNote.DBSource.
                AccountingManager.GetAccountingList(cUser.ID);
            this.GridView1.DataBind();
        }
    }
}