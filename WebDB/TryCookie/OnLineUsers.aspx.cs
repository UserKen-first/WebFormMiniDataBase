using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TryCookie
{
    public partial class OnLineUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userCount = Application["UserCount"] as int?;

            if (userCount.HasValue)
                this.ItOnlineUser.Text = userCount.ToString();
            else
                this.ItOnlineUser.Text = "1";
        }
    }
}