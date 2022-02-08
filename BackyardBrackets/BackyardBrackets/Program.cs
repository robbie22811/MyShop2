using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackyardBrackets
{
    
    class Program
    {
        static void Main(string[] args)
        {

            // Get the teams.
            //string all_teams = txtTeams.Text;
            string all_teams = "Team 1\rTeam 2\nTeam 3\nTeam 4\nTeam 5";
            char[] separators = { '\r', '\n' };
            string[] team_names = all_teams.Split(separators,
                StringSplitOptions.RemoveEmptyEntries);

            // Get the schedule.
            int num_teams = team_names.Length;
            if(num_teams < 3)
                Console.WriteLine("No reason to make a round robin. Just play what you want.");

            int[,] results = GenerateRoundRobin(num_teams);

            List<string> eventList = new List<string> { "bags", "darts" };

            int gamesPerRound = num_teams / 2;
            int noGame = 0;
            while (gamesPerRound > eventList.Count)
            {
                noGame++;
                eventList.Add("No Game Available " + noGame);
            }
            int[,] eventCounter = new int[num_teams, eventList.Count];
            for (int ee = 0; ee < num_teams; ee++)
            {
                //default array
                for (int ii = 0; ii < eventList.Count; ii++)
                {
                    eventCounter[ee, ii] = 0;
                    //Console.WriteLine($"Team {ee+1} Game {ii} = 0");
                }
            }

            int rotations = 1; //how many times do you want to play each team?

            int match = 0;
            string txt = "";

            for (int rr = 1; rr <= rotations; rr++)
            {
                // Display the result.
                int rounds = results.GetUpperBound(1) * rotations + rotations;
                int[,] roundCounter = new int[rounds, eventList.Count];
                int gameMax = 0;
                if (num_teams % 2 == 0)
                {
                    gameMax = (int)Math.Ceiling((double)rounds / (double)eventList.Count);
                }
                else
                {
                    gameMax = (int)Math.Floor((double)rounds / (double)eventList.Count);
                }
                gameMax *= rotations;
                
                for (int round = 0; round <= results.GetUpperBound(1); round++)
                {
                    //int gameToPlay = 0;
                    
                    //if (match > 0)
                    //{
                    //    if (match > eventList.Count - 1)
                    //    {
                    //        gameToPlay = match - eventList.Count;
                    //        while (gameToPlay > eventList.Count - 1)
                    //        {
                    //            gameToPlay -= (eventList.Count);
                    //            if (gameToPlay < 0)
                    //                gameToPlay = 0;
                    //        }
                    //    }
                    //    else
                    //        gameToPlay = match;

                    //}
                    txt += "Round " + (match + 1) + ":\r\n";
                    for (int team = 0; team < num_teams; team++)
                    {
                        if (results[team, round] == BYE)
                        {
                            txt += "    " + team_names[team] + " (bye)\r\n";
                        }
                        else if (team < results[team, round])
                        {
                            string matchPlusEvent = PickEvent(team, results[team, round], eventList, eventCounter, roundCounter, match, gameMax);

                            txt += "    " + team_names[team] + " v " +
                                team_names[results[team, round]] + "  " + matchPlusEvent + "\r\n";
                            
                            //gameToPlay++;

                            //if (gameToPlay > eventList.Count - 1)
                            //    gameToPlay = 0;
                        }
                    }
                    match++;
                }
            }

            Console.WriteLine(txt);
            Console.ReadLine();
        }

        static string PickEvent(int teamOne, int matchedTeam, List<string> listofGames, int[,] tracker, int[,] round, int currentRound, int max)
        {
            //would love this to be smarter to limit the number of times each team plays a specific game
            int maxGame = listofGames.Count;
            int maxTry = maxGame * 3 + 1;
            int gameToPlay = 0;
            int value1 = 0;
            int value2 = 0;
            
            for (int gg = 0; gg < maxTry; gg++)
            {
                if (gg > 0)
                {
                    if (gg == 2 || (gg - 2) % 3 == 0)
                    {
                        value1 = value2;
                        value2 = value1 - 1;
                    }
                    else
                        value2++;
                }

                for (int currentGame = 0; currentGame < maxGame; currentGame++)
                {
                    //for (int rr = 0; rr < maxGame; rr++)
                    //{
                        if (round[currentRound, currentGame] == 0 && tracker[teamOne, currentGame] == value1 && tracker[matchedTeam, currentGame] == value2 
                            && tracker[teamOne, currentGame] < max && tracker[matchedTeam, currentGame] < max)
                        {
                            //Console.WriteLine($"Team {teamOne} v Team {matchedTeam} will play game {listofGames[currentGame]}");
                            tracker[teamOne, currentGame]++;
                            tracker[matchedTeam, currentGame]++;
                            round[currentRound, currentGame]++;
                            return listofGames[currentGame];
                        }
                    //}
                }
            }

            //if we made this far, there is an uneven amount of games so now we need to try to find games a little differently
            value1 = 0;
            value2 = 2;
            int saveValue = 0;
            for (int gg = 0; gg < maxTry; gg++)
            {
                if (gg > 0)
                {
                    if (gg % 2 == 0)
                    {
                        saveValue = value1;
                        value1 = value2;
                        value2 = saveValue;
                    }
                    else
                    {
                        value1++;
                        value2++;
                    }
                }

                for (int currentGame = 0; currentGame < maxGame; currentGame++)
                {
                    //for (int rr = 0; rr < maxGame; rr++)
                    //{
                    if (round[currentRound, currentGame] == 0 && tracker[teamOne, currentGame] == value1 && tracker[matchedTeam, currentGame] == value2
                        && tracker[teamOne, currentGame] < max && tracker[matchedTeam, currentGame] < max)
                    {
                        //Console.WriteLine($"Team {teamOne} v Team {matchedTeam} will play game {listofGames[currentGame]}");
                        tracker[teamOne, currentGame]++;
                        tracker[matchedTeam, currentGame]++;
                        round[currentRound, currentGame]++;
                        return listofGames[currentGame] + " Un-even";
                    }
                    //}
                }
            }


            Console.WriteLine($"Team {teamOne + 1} v Team {matchedTeam + 1} will play game {listofGames[gameToPlay]} by default");
            return listofGames[gameToPlay];
        }

        private const int BYE = -1;

        // Return an array where results(i, j) gives
        // the opponent of team i in round j.
        // Note: num_teams must be odd.
        static int[,] GenerateRoundRobinOdd(int num_teams)
        {
            int n2 = (int)((num_teams - 1) / 2);
            int[,] results = new int[num_teams, num_teams];

            // Initialize the list of teams.
            int[] teams = new int[num_teams];
            for (int i = 0; i < num_teams; i++) teams[i] = i;

            // Start the rounds.
            for (int round = 0; round < num_teams; round++)
            {
                for (int i = 0; i < n2; i++)
                {
                    int team1 = teams[n2 - i];
                    int team2 = teams[n2 + i + 1];
                    results[team1, round] = team2;
                    results[team2, round] = team1;
                }

                // Set the team with the bye.
                results[teams[0], round] = BYE;

                // Rotate the array.
                RotateArray(teams);
            }

            return results;
        }

        static int[,] GenerateRoundRobinEven(int num_teams)
        {
            // Generate the result for one fewer teams.
            int[,] results = GenerateRoundRobinOdd(num_teams - 1);

            // Copy the results into a bigger array,
            // replacing the byes with the extra team.
            int[,] results2 = new int[num_teams, num_teams - 1];
            for (int team = 0; team < num_teams - 1; team++)
            {
                for (int round = 0; round < num_teams - 1; round++)
                {
                    if (results[team, round] == BYE)
                    {
                        // Change the bye to the new team.
                        results2[team, round] = num_teams - 1;
                        results2[num_teams - 1, round] = team;
                    }
                    else
                    {
                        results2[team, round] = results[team, round];
                    }
                }
            }

            return results2;
        }

        static int[,] GenerateRoundRobin(int num_teams)
        {
            if (num_teams % 2 == 0)
                return GenerateRoundRobinEven(num_teams);
            else
                return GenerateRoundRobinOdd(num_teams);
        }

        // Rotate the entries one position.
        static void RotateArray(int[] teams)
        {
            int tmp = teams[teams.Length - 1];
            Array.Copy(teams, 0, teams, 1, teams.Length - 1);
            teams[0] = tmp;
        }
    }
}
