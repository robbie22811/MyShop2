using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BlindDraw
{
    public partial class _Default : Page
    {
        private SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lynds\source\repos\BlindDraw\App_Data\Schedule.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                gvbind();
            }
        }

        protected void gvbind()
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("Select * From Players", _connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            _connection.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPlayers.DataSource = ds;
                gvPlayers.DataBind(); 
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvPlayers.DataSource = ds;
                gvPlayers.DataBind();
                int columncount = gvPlayers.Columns.Count;
                gvPlayers.Rows[0].Cells.Clear();
                gvPlayers.Rows[0].Cells.Add(new TableCell());
                gvPlayers.Rows[0].Cells[0].ColumnSpan = columncount;
                gvPlayers.Rows[0].Cells[0].Text = "No records";
            }
        }

        protected void gvPlayers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPlayers.EditIndex = e.NewEditIndex;
            gvbind();
        }

        protected void gvPlayers_RowUpdating(object sender, GridViewUpdateEventArgs e) 
        {
            string player_id = gvPlayers.DataKeys[e.RowIndex].Values["playerId"].ToString();
            TextBox player_name = (TextBox)gvPlayers.Rows[e.RowIndex].FindControl("txtname");
            _connection.Open();
            SqlCommand cmd = new SqlCommand("update players set playerName = '" + player_name.Text + "' where playerId = " + player_id, _connection);
            cmd.ExecuteNonQuery();
            _connection.Close();
            gvPlayers.EditIndex = -1;
            gvbind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
        }
    }
}