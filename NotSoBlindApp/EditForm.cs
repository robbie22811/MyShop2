using DemoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NotSoBlindApp
{
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            LoadDataFromDB();
        }

        private void LoadDataFromDB()
        {
            var players = SqliteDataAccess.QueryPerson($"Select * From Person Order by Handicap Desc,FullName;");
            dataGridView1.DataSource = players;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<PersonModel>? playerstoupdate = dataGridView1.DataSource as List<PersonModel>;

            if (playerstoupdate != null)
            {
                foreach (var p in playerstoupdate)
                {
                    SqliteDataAccess.UpdatePlayer(p);
                }
            }
            MessageBox.Show("Update Done");
        }
    }
}
