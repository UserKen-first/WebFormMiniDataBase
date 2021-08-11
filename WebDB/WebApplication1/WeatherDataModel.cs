using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class WeatherDataModel  // 做一個額外的類別來裝屬性
    {
        public string Name { get; set; }
        public int T { get; set; }
        public int Pop { get; set; }
    }
}