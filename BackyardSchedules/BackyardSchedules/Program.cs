using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackyardSchedules
{
    class Program
    {
        private const int BYE = -1;
        
        static void Main(string[] args)
        {
            int byeID = -1;

            // Get the events
            // Textbox on webform -- enter game names
            List<string> eventList = new List<string> { "bags", "darts", "bocce", "rollors" };

            // Get the teams.
            // Textbox on the webform -- string all_teams = txtTeams.Text;
            string all_teams = "One\rTwo\nThree\nFour\nFive\nSix\nSeven\nEight\nNine";
            List<string> team_names = GenerateTeams(all_teams);

            if (team_names.Count % 2 > 0)
            {
                team_names.Add("BYE");
                byeID = team_names.Count - 1;
            }
                

            // Get the schedule.
            int num_teams = team_names.Count;
            Console.WriteLine(SetTournament(team_names, eventList, byeID));


            //int[,] schedule = GenerateRoundRobin(num_teams);

            //int gamesPerRound = (int)Math.Floor((double)num_teams / 2);
            //int roundsToPlay = schedule.GetUpperBound(1);
            //int maxAttempts = gamesPerRound * roundsToPlay;

            //int[,] eventCounter = new int[num_teams, eventList.Count];
            //int[,] roundCounter = new int[roundsToPlay + 1, num_teams];
            //for (int round = 0; round <= roundsToPlay; round++)
            //{
            //    for (int game = 0; game < gamesPerRound; game++)
            //    {
            //        string gameToPlay = SetGame(round, game, eventList, team_names, schedule, eventCounter, roundCounter, maxAttempts);
            //        Console.WriteLine($"Round {round + 1}, Game {game + 1} = {gameToPlay}");
            //    }
            //}

            //string txt = "";
            //for (int round = 0; round <= schedule.GetUpperBound(1); round++)
            //{
            //    txt += "Round " + (round + 1) + ":\r\n";
            //    for (int team = 0; team < num_teams; team++)
            //    {
            //        if (schedule[team, round] == BYE)
            //        {
            //            txt += "    " + team_names[team] + " (bye)\r\n";
            //        }
            //        else if (team < schedule[team, round])
            //        {
            //            txt += "    " + team_names[team] + " v " +
            //                team_names[schedule[team, round]] + "\r\n";
            //        }
            //    }
            //}

            //Console.WriteLine(txt);
            Console.ReadLine();

        }

        static string SetTournament(List<string> teamNames, List<string> games, int byeID)
        {
            string roundPair = "";

            int n = teamNames.Count;
            int numofRounds = n - 1;

            int teamId;
            int team2Id;
            
            int[,] eventCounter = new int[n, games.Count];
            int[,] roundCounter = new int[numofRounds, games.Count];

            for (var r = 1; r < n; r++)
            {
                roundPair += "\n" + formatter(r) + ":";
                for (var i = 1; i <= n/2; i++)
                {
                    if (i==1)
                    {
                        teamId = 0;
                        team2Id = (n - 1 + r - 1) % (n - 1) + 1;
                        //roundPair += " [" + formatter(1) + "," + formatter((n - 1 + r - 1) % (n - 1) + 2) + "]";
                    }
                    else
                    {
                        teamId = (r + i - 2) % (n - 1) + 1;
                        team2Id = (n - 1 + r - i) % (n - 1) + 1;
                        //roundPair += " [" + formatter( (r+i-2) % (n-1) + 2) + "," + formatter((n - 1 + r - i) % (n - 1) + 2) + "]";
                    }
                    if (byeID >= 0 && (teamId == byeID || team2Id == byeID))
                    {
                        roundPair += " BYE  [" + teamNames[teamId] + "," + teamNames[team2Id] + "]";
                    }
                    else
                    {
                        string rg = PickGame(roundCounter, r - 1, eventCounter, games, teamId, team2Id, numofRounds);
                        roundPair += " " + rg.ToUpper() + " [" + teamNames[teamId] + "," + teamNames[team2Id] + "]";
                    }
                    
                }
            }

            return roundPair;
        }

        static string PickGame(int[,] roundCounter, int roundNum, int[,] eventCounter, List<string> listofGames, int teamOne, int teamTwo, int rounds)
        {
            int gameID = 0;
            bool gameFound = false;
            int loopCount = 0;

            int numGames = listofGames.Count;
            int maxGames = numGames - 1;
            int maxLoops = numGames * rounds + 1; //prevent infinite loop below
            

            //figure out the most even game to play
            int value1 = 0;
            int value2 = 0;
            int currentLow = 0;
            int saveValue;

            while (gameFound == false && loopCount < maxLoops)
            {
                if (loopCount > 0)
                {
                    if(value2 == maxGames)
                    {
                        value1 = currentLow + 1;
                        value2 = value1;
                        currentLow++;
                    }
                    else if (value1 ==  value2)
                    {
                        value1++;
                    }
                    else if (value1 < value2)
                    {
                        saveValue = value1;
                        value1 = value2 + 1;
                        value2 = saveValue;
                    }
                    else
                    {
                        saveValue = value1;
                        value1 = value2;
                        value2 = saveValue;
                    }
                }

                for (int gg = 1; gg <= numGames; gg++)
                {
                    gameID = gg - 1;
                    if (roundCounter[roundNum, gameID] == 0)
                    {
                        if (eventCounter[teamOne, gameID] == value1 && eventCounter[teamTwo, gameID] == value2)
                            gameFound = true;

                        if (gameFound == true)
                        {
                            eventCounter[teamOne, gameID]++;
                            eventCounter[teamTwo, gameID]++;
                            roundCounter[roundNum, gameID] = 1;
                            break;
                        }
                    }
                }

                loopCount++;
            }

            if (gameFound == false)
                return "Not Found";
            else
                return listofGames[gameID];

        }

        static string formatter(int x)
        {
            if (x < 10)
                return "  " + x.ToString();
            if (x < 100)
                return " " + x.ToString();
            else
                return x.ToString();
        }

        static List<string> GenerateTeams(string teams)
        {
            char[] separators = { '\r', '\n' };
            string[] teamArray =  teams.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            List<string> results = new List<string>();

            foreach (string t in teamArray)
            {
                results.Add(t);
            }

            return results;
        }

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


        static string SetGame(int roundNum, int gameID, List<string> listofGames, string[] teamNames, int[,] matches, int[,] eventCounter, int[,] roundCounter, int maxLoops)
        {
            int numofTeams = teamNames.Length;
            string result = "";

            for (int team = 0; team < numofTeams; team++)
            {
                if (matches[team, roundNum] == BYE)
                {
                    //ignore for now
                }
                else if (roundCounter[roundNum,team] > 0)
                {
                    //ignore for now
                }
                //else if (team < matches[team, roundNum])
                //{
                    if (eventCounter[team, gameID] == 0 && eventCounter[matches[team, roundNum], gameID] == 0)
                    {
                        eventCounter[team, gameID]++;
                        eventCounter[matches[team, roundNum], gameID]++;
                        roundCounter[roundNum, team] = 1;
                        roundCounter[roundNum, matches[team, roundNum]] = 1;
                        result = listofGames[gameID].ToUpper() + " " + teamNames[team] + " vs " + teamNames[matches[team, roundNum]];
                        break;
                    }

                //}
            }

            //now it gets tricky, someone has already played this game and it's time to repeat
            int value1 = 0;
            int value2 = 1;
            int saveValue = 0;
            int loops = 0;

            while (result.Length == 0 && loops <= maxLoops)
            {
                if (loops > 0)
                {
                    value2++;
                }

                for (int team = 0; team < numofTeams; team++)
                {
                    if (matches[team, roundNum] == BYE)
                    {
                        //ignore for now
                    }
                    else if (roundCounter[roundNum, team] > 0)
                    {
                        //ignore for now
                    }
                    //else if (team < matches[team, roundNum])
                    //{
                        for (int attempt = 0; attempt < 2; attempt++)
                        {
                            if (attempt == 1 && value1 != value2)
                            {
                                saveValue = value2;
                                value2 = value1;
                                value1 = saveValue;
                            }
                            else if(attempt == 1)
                            {
                                team = numofTeams;
                                break;
                            }
                            if (eventCounter[team, gameID] == value1 && eventCounter[matches[team, roundNum], gameID] == value2)
                            {
                                eventCounter[team, gameID]++;
                                eventCounter[matches[team, roundNum], gameID]++;
                                roundCounter[roundNum, team] = 1;
                                roundCounter[roundNum, matches[team, roundNum]] = 1;
                                result = listofGames[gameID].ToUpper() + " " + teamNames[team] + " vs " + teamNames[matches[team, roundNum]];
                                team = numofTeams;
                                break;
                            }
                       // }

                    }

                }
                loops++;
            }

            //now it gets trickier, the games are becoming un-even.  
            value1 = 0;
            value2 = 2;
            saveValue = 0;
            loops = 0;
            while (result.Length == 0 && loops <= maxLoops)
            {
                if (loops > 0)
                {
                    value1++;
                    value2++;
                }

                for (int team = 0; team < numofTeams; team++)
                {
                    if (matches[team, roundNum] == BYE)
                    {
                        //ignore for now
                    }
                    else if (roundCounter[roundNum, team] > 0)
                    {
                        //ignore for now
                    }
                    else if (team < matches[team, roundNum])
                    {
                        for (int attempt = 0; attempt < 2; attempt++)
                        {
                            if (attempt == 1 && value1 != value2)
                            {
                                saveValue = value2;
                                value2 = value1;
                                value1 = saveValue;
                            }
                            else if (attempt == 1)
                            {
                                team = numofTeams;
                                break;
                            }
                            if (eventCounter[team, gameID] == value1 && eventCounter[matches[team, roundNum], gameID] == value2)
                            {
                                eventCounter[team, gameID]++;
                                eventCounter[matches[team, roundNum], gameID]++;
                                roundCounter[roundNum, team] = 1;
                                roundCounter[roundNum, matches[team, roundNum]] = 1;
                                result = listofGames[gameID].ToUpper() + " " + teamNames[team] + " vs " + teamNames[matches[team, roundNum]];
                                team = numofTeams;
                                break;
                            }
                        }

                    }

                }
                loops++;
            }

            if (result == "")
                return "No Match Found";
            else
                return result;
        }

    }
}
