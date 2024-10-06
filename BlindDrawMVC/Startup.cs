using Microsoft.Owin;
using Owin;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System;

[assembly: OwinStartupAttribute(typeof(BlindDrawMVC.Startup))]
namespace BlindDrawMVC
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
