using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackyardSchedules
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the events
            // Textbox on webform -- enter game names
            List<string> eventList = new List<string> { "bags", "darts", "bocce", "rollors" };

            // Get the teams.
            // Textbox on the webform -- string all_teams = txtTeams.Text;
            string all_teams = "0One\rTwo\nThree\nFour\nFive\nSix\nSeven\nEight";

            List<string> team_names = GenerateTeams(all_teams);

            int rotations = 1; //input option

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

    }
}
