using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;

namespace BlindDrawMVC
{
    public partial class _Default : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\lynds\source\repos\BlindDrawMVC\App_Data\aspnet-BlindDrawMVC-20230511074310.mdf;Initial Catalog=aspnet-BlindDrawMVC-20230511074310;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                refreshdata();
            }
        }
        public void refreshdata()
        {
            SqlCommand cmd = new SqlCommand("select * from Players", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var activeIds = new List<int>();
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                var checkbox = gvrow.FindControl("CheckBox1") as CheckBox;
                if (checkbox.Checked)
                {
                    var lblID = gvrow.FindControl("Label1") as Label;
                    activeIds.Add(int.Parse(lblID.Text));
                }
            }

            var schedule = new Schedule(activeIds, con);
            var group = schedule.CreateGroup(activeIds[0], 4);
        }

        public class Schedule
        {
            public List<int> PlayerIds { get; set; }
            public SqlConnection SqlConnection { get; set; }

            public Schedule(List<int> players, SqlConnection conn) 
            {
                this.PlayerIds = players;
                this.SqlConnection = conn;
            }

            public List<int> CreateGroup(int id, int groupSize)
            {
                SqlCommand cmd = new SqlCommand("select top " + (groupSize - 1).ToString() + " g.pid2 from players p left outer join gamesbyid g on p.id = g.pid1 where p.id = 2 order by g.gamecount");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                

            }
        }
    }
}