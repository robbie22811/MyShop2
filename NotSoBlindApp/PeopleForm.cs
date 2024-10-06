using DemoLibrary;
using NotSoBlindApp;
using System.Diagnostics;
using System.Text;

namespace WinFormUI
{
    public partial class PeopleForm : Form
    {
        List<PersonModel> people = new List<PersonModel>();
        List<PersonModel> activePlayers = new List<PersonModel>();
        
        public PeopleForm()
        {
            InitializeComponent();
            AddVersionNumber();
            RefreshLists();

            WindowState = FormWindowState.Maximized;
        }

        private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            this.Text += $" v.{versionInfo.FileVersion}";
        }

        private void LoadPeopleList()
        {
            people = SqliteDataAccess.LoadPeople();

            WireUpPeopleList();
        }

        private void LoadActiveList()
        {
            activePlayers = SqliteDataAccess.LoadActivePlayers();

            WireUpActiveList();
        }

        private void WireUpPeopleList()
        {
            listPeopleListBox.DataSource = null;
            listPeopleListBox.DataSource = people;
            listPeopleListBox.DisplayMember = "FullName";
        }

        private void WireUpActiveList()
        {
            listActivePlayers.DataSource = null;
            listActivePlayers.DataSource = activePlayers;
            listActivePlayers.DisplayMember = "FullName";
        }

        private void refreshListButton_Click(object sender, EventArgs e)
        {
            var playerCount = listActivePlayers.Items.Count;
            if (playerCount <= 1)
            {
                MessageBox.Show("You don't have enough players to play!");
                return;
            }

            var gs = new Games();
            var games = gs.CreateGroups(playerCount);

            var sb = new StringBuilder();
            foreach (var g in games)
            {
                sb.Append($"Court {g.CourtId} will have {g.PlayerCount} players\n");
            }
            sb.Append("\nDo you want to proceed?");
            var result = MessageBox.Show(sb.ToString(), "Verify Courts", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
            retry:
                var duplicateFound = false;
                var success = false;
                var dupeCount = 0;
                if (int.TryParse(txtFailCount.Text, out int maxFailCount) && int.TryParse(txtRetries.Text, out int retryCount))
                {
                    if (retryCount <= 0)
                        retryCount = 50;

                    for (var i = 0; i < retryCount && !success; i++)
                    {
                        lblTry.Text = $"Try {i + 1} of {retryCount}";
                        lblTry.Refresh();

                        dupeCount = 0;
                        gs.CreateSchedule(games);
                        foreach(var g in games)
                        {
                            var dupeCount2 = gs.CountDupes(g.PlayerIds);
                            g.DuplicateCount = dupeCount2;
                            if (dupeCount2 > maxFailCount)
                                dupeCount++;
                        }
                        if (dupeCount == 0)
                            success = true;

                        if(i == retryCount - 1 && !success)
                        {
                            duplicateFound = true; 
                        }
                    }
                }
                result = MessageBox.Show(gs.DisplayGames(games, duplicateFound) + "Failures: " + dupeCount + "\nStart Games?", "Preview Matches", MessageBoxButtons.CancelTryContinue);
                if (result == DialogResult.Continue)
                {
                    gs.UpdateGames(games);
                    MessageBox.Show("Done!");
                }
                else if (result == DialogResult.TryAgain)
                {
                    goto retry;
                }    
                
            }
            sb.Clear();
        }

        
        private void addPersonButton_Click(object sender, EventArgs e)
        {
            PersonModel p = new PersonModel();

            p.FullName = firstNameText.Text;
            p.Handicap = 0;

            SqliteDataAccess.SavePerson(p);

            firstNameText.Text = "";

            RefreshLists();
        }

        private void btnMoveToActive_Click(object sender, EventArgs e)
        {
            var selectedIds = listPeopleListBox.SelectedItems;

            foreach (var selectedId in selectedIds)
            {
                PersonModel p = (PersonModel)selectedId;
                p.Status = 1;
                SqliteDataAccess.UpdatePerson(p);
            }

            RefreshLists();
        }

        private void RefreshLists()
        {
            LoadPeopleList();
            LoadActiveList();

            var acount = listActivePlayers.Items.Count;
            var pcount = listPeopleListBox.Items.Count;

            listPeopleHeader.Text = $"Available Players ({pcount})";
            listActiveHeader.Text = $"Active Players ({acount})";
        }

        private void btnMoveToInactive_Click(object sender, EventArgs e)
        {
            var selectedIds = listActivePlayers.SelectedItems;

            foreach (var selectedId in selectedIds)
            {
                PersonModel p = (PersonModel)selectedId;
                p.Status = 0;
                SqliteDataAccess.UpdatePerson(p);
            }

            RefreshLists();
        }
        private void btnAllActive_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.UpdateAllPlayers(1);
            RefreshLists();
        }

        private void btnAllInactive_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.UpdateAllPlayers(0);
            RefreshLists();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            var displayText = string.Empty;
            var report = SqliteDataAccess.GetReports();
            if (report != null)
            {
                var previousId = "";
                foreach (var item in report)
                {
                    var pid1 = item.Player1;
                    var pid2 = item.Player2;
                    var count = item.matchcount;

                    if (pid1 != previousId)
                        displayText += $"{pid1} Match Counts:\n";

                    displayText += $"\t {pid2} = {count}\n";

                    previousId = pid1;
                }
            }

            ReportForm reportForm = new ReportForm(displayText);
            reportForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the schedule?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqliteDataAccess.DeleteAllGames();

                if (MessageBox.Show("Do you want to remove all players?", "Remove Players", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqliteDataAccess.DeleteAllPlayers();
                    RefreshLists();
                }
                else if (MessageBox.Show("Do you want to reset player handicaps?", "Clear Handicap", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqliteDataAccess.UpdatePersonQuery("Update person set handicap = 0");
                }
                MessageBox.Show("Done!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm();
            editForm.ShowDialog();
            RefreshLists();
        }

        private void PeopleForm_Load(object sender, EventArgs e)
        {
            this.txtFailCount.Text = "0";
            this.txtRetries.Text = "50";
        }
    }
}
