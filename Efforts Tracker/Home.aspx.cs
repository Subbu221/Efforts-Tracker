using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Efforts_Tracker.Models;

namespace Efforts_Tracker
{
    public partial class Home : System.Web.UI.Page
    {
        EffortsTrackingToolEntities DBContext = new EffortsTrackingToolEntities();
        int UserId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                Tickets.Visible = false;
                Adhocrequests.Visible = false;
                Mettings.Visible = false;
                GridView1.Visible = false;
                GridView2.Visible = false;
                GridView3.Visible = false;
                //if (Session["UserName"].ToString()=="")
                //    Response.Redirect("Login.aspx");
                //else
                Label21.Text = Session["UserName"].ToString();
                UserId = Convert.ToInt32(Session["Id"]);
            }
            else
            {
                Response.Write("<script>alert('Please Login');</script>");
                Response.Redirect("Login.aspx");

            }

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedItem.Value == "3")
            {
                MeetingsBindData();
                Mettings.Visible = true;
                GridView3.Visible = true;
            }
            else if (RadioButtonList1.SelectedItem.Value == "1")
            {
                TicketsBindData();
                Tickets.Visible = true;
                GridView1.Visible = true;
            }
            else if (RadioButtonList1.SelectedItem.Value == "2")
            {
                AdhocBindData();
                Adhocrequests.Visible = true;
                GridView2.Visible = true;
            }
        }

        protected void SaveTickets_Click(object sender, EventArgs e)
        {
            /*Get Values from Controls-Start */
            int UserId =Convert.ToInt32( Session["Id"]);
            int TicketType =Convert.ToInt32( TiketsTypesDropdown.SelectedItem.Value);
            string TicketNUmber = TicketNumberText.Text;
            string WDate = WorkedDate.Text;
            string TTime = TotalTime.Text;
            string TRemarks = TicketsRemarksText.Value;
            /*Get Values from Controls-End */

            TicketsMaster tickets = new TicketsMaster();
           
            tickets.UserId = UserId;
            tickets.TicketType = TicketType;
            tickets.TicketNumber = TicketNUmber;
            tickets.WorkedDate = WDate;
            tickets.TotalHours = TTime;
            tickets.Remarks = TRemarks;
            tickets.RequestType =Convert.ToInt32( RadioButtonList1.SelectedItem.Value);
            DBContext.TicketsMasters.Add(tickets);
            DBContext.SaveChanges();
           
            RadioButtonList1.SelectedItem.Value = "1";
            Tickets.Visible = true;
            GridView1.Visible = true;
            TicketsBindData();
            Label22.Text = "Record Added";
            /* Reset Controls- Start*/
            TiketsTypesDropdown.SelectedItem.Value = "0";           
            TicketNumberText.Text = "";
            WorkedDate.Text = "";
            TotalTime.Text = "";
            TicketsRemarksText.Value = "";
            /* Reset Controls- End*/

            //Response.Write("<script>alert('RecordAdded');</script>");
           
        }

        public void TicketsBindData()
        {
            GridView1.DataSource = null;
            GridView1.DataSource = (from f in DBContext.vw_TicketsDetails where f.UserId == UserId select f).ToList();
            GridView1.DataBind();
        }
        

        protected void Save_AdhocRequest(object sender, EventArgs e)
        {
            int UserId = Convert.ToInt32(Session["Id"]);
            string Description = ADescription.Text;
            string Requestedby = ARequested.Text;
            string WDate = AWorkeddate.Text;
            string TTime = ATTime.Text;
            string TRemarks = ARemarksText.Value;
           
            AdhocRequest adhoc = new AdhocRequest();
            adhoc.UserId = UserId;
            adhoc.Requestedby = Requestedby;
            adhoc.TotalTime = TTime;
            adhoc.WokedDate = WDate;
            adhoc.Remarks = TRemarks;
            adhoc.Description = Description;
            adhoc.RequestType =Convert.ToInt32(RadioButtonList1.SelectedItem.Value);

            DBContext.AdhocRequests.Add(adhoc);
            DBContext.SaveChanges();

            Adhocrequests.Visible = true;
            RadioButtonList1.SelectedItem.Value = "2";
            AdhocBindData();
            GridView2.Visible = true;
            ADescription.Text = "";
            ARequested.Text = "";
            AWorkeddate.Text = "";
            ATTime.Text = "";
            ARemarksText.Value = "";

        }
        public void AdhocBindData()
        {
            GridView2.DataSource = null;
            GridView2.DataSource = (from d in DBContext.AdhocRequests
                                    where d.UserId == UserId
                                    select d).ToList();
            GridView2.DataBind();

        }

        protected void Save_Meetings(object sender, EventArgs e)
        {
            Meeting meeting = new Meeting();
            meeting.UserId= Convert.ToInt32(Session["Id"]);
            meeting.Designation = MDesignation.Text;
            meeting.EmployeeCNT =Convert.ToInt32( EmployeeCNT.Text);
            meeting.EmployeeName = EmployeeNames.Value;
            meeting.Remarks = MRemarks.Value;
            meeting.RequestType= Convert.ToInt32(RadioButtonList1.SelectedItem.Value);
            DBContext.Meetings.Add(meeting);
            DBContext.SaveChanges();
            MeetingsBindData();
            Mettings.Visible = true;
            GridView3.Visible = true;
            RadioButtonList1.SelectedItem.Value = "3";
            MDesignation.Text = "";
            EmployeeCNT.Text = "";
            EmployeeNames.Value = "";
            MRemarks.Value = "";
        }
        public void MeetingsBindData()
        {
            GridView3.DataSource = null;
            GridView3.DataSource = (from d in DBContext.Meetings
                                    where d.UserId == UserId
                                    select d).ToList();
            GridView3.DataBind();

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session["UserName"] = "";
            Response.Redirect("Login.aspx");
        }
    }
    
}