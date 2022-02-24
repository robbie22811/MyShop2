using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackyardSchedules
{
    class Program
    {
        static DataSet dataSet;

        private static DataTable MakeTable(int gameCount, string tableName)
        {
            DataTable table = new DataTable(tableName);
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            column.ReadOnly = true;
            column.Unique = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "round";
            table.Columns.Add(column);

            for (int ii = 0; ii < gameCount; ii++)
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "game" + (ii + 1).ToString();
                table.Columns.Add(column);
            }

            return table;
        }

        private static void InsertRows(DataTable table, List<Matches> matches, int rounds)
        {
            for (int round = 0; round < rounds; round++)
            {
                int gameCount = 0;

                DataRow row = table.NewRow();
                row["id"] = round;
                row["round"] = round + 1;
                foreach(Matches match in matches)
                {
                    if(match.RoundNumber==round && match.TeamOne != "Bye" && match.TeamTwo != "Bye")
                    {
                        gameCount++;
                        row["game" + gameCount.ToString()] = match.TeamOne + "|" + match.TeamTwo;
                    }
                }
                table.Rows.Add(row);
            }
                
        }

        static void Main(string[] args)
        {
            // Get the events
            // Textbox on webform -- enter game names
            List<string> eventList = new List<string> { "bags", "darts", "bocce", "rollors" };

            // Get the teams.
            // Textbox on the webform -- string all_teams = txtTeams.Text;
            string all_teams = "One\rTwo\nThree\nFour\nFive\nSix\nSeven\nEight";
            List<string> team_names = GenerateTeams(all_teams);

            int teamsThatPlay = 0;
            if (team_names.Count % 2 != 0)
            {
                teamsThatPlay = team_names.Count - 1;
                team_names.Add("Bye");
            }
            else
            {
                teamsThatPlay = team_names.Count;
            }

            int rotations = 1; //input option
            int numofTeams = team_names.Count;
            int cycleVal = numofTeams / 2 - 1;

            DataTable firstPass = MakeTable(eventList.Count, "First");
            DataTable secondPass = MakeTable(eventList.Count, "Second");
            DataTable scheduleTable = MakeTable(eventList.Count, "Schedule");

            //string results = SetTournament(team_names, eventList, rotations);
            RoundRobin rr = new RoundRobin();
            List<Matches> mm = rr.SetRoundRobin(team_names, eventList, rotations);

            foreach (Matches match in mm)
                Console.WriteLine($"{match.RoundNumber}: {match.TeamOne} will play {match.TeamTwo} in {match.Event}");

            Console.ReadLine();
        
        }

        static List<string> GenerateTeams(string teams)
        {
            char[] separators = { '\r', '\n' };
            string[] teamArray = teams.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            List<string> results = new List<string>();

            foreach (string t in teamArray)
            {
                results.Add(t);
            }

            return results;
        }

        public static List<Matches> ListMatches(List<string> ListTeam)
        {
            List<Matches> matchResult = new List<Matches>();

            if (ListTeam.Count % 2 != 0)
            {
                ListTeam.Add("Bye");
            }

            int numTeams = ListTeam.Count;

            int numDays = (numTeams - 1);
            int halfSize = numTeams / 2;

            List<string> teams = new List<string>();

            teams.AddRange(ListTeam);
            teams.RemoveAt(0);

            int teamsSize = teams.Count;

            for (int day = 0; day < numDays; day++)
            {
                //Console.WriteLine("Day {0}", (day + 1));

                int teamIdx = day % teamsSize;

                matchResult.Add(new Matches { RoundNumber = day, TeamOne = teams[teamIdx], TeamTwo = ListTeam[0] });
                //Console.WriteLine("{0} vs {1}", teams[teamIdx], ListTeam[0]);

                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;
                    matchResult.Add(new Matches { RoundNumber = day, TeamOne = teams[firstTeam], TeamTwo = teams[secondTeam] });
                    //Console.WriteLine("{0} vs {1}", teams[firstTeam], teams[secondTeam]);
                }
            }

            return matchResult;
        }
    }
}
