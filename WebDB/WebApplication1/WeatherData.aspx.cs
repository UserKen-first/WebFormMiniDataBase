using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WeatherData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var model = WeatherDataReader.ReadData();

            this.Itlocation.Text = model.Name;
            this.ItTemp.Text = model.T.ToString();
            this.ItPop24.Text = model.Pop.ToString();
        }
    }
}