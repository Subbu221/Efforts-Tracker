using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Efforts_Tracker.Models;


namespace Efforts_Tracker
{
    public partial class Login : System.Web.UI.Page
    {
        EffortsTrackingToolEntities DBContext = new EffortsTrackingToolEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Login1_Authenticate(object sender, EventArgs e)
        {

            var users = (from f in DBContext.UsersLogins
                         where f.UserName == TextBox1.Text && f.Password == TextBox2.Text select f).FirstOrDefault();
            if(users!=null)
            {
                Session["UserName"] = users.UserName;
                Application["Users"]= users.LandId;
                Session["Id"] = users.Id;
                Response.Redirect("Home.aspx");
            }
        }
    }
}