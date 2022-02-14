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
            int byeID = -1;

            // Get the events
            // Textbox on webform -- enter game names
            List<string> eventList = new List<string> { "bags", "darts"};

            // Get the teams.
            // Textbox on the webform -- string all_teams = txtTeams.Text;
            string all_teams = "One\rTwo\nThree";
            List<string> team_names = GenerateTeams(all_teams);

            if (team_names.Count % 2 > 0)
            {
                //team_names.Add("BYE");
                byeID = 1;
            }


            // Get the schedule.
            int num_teams = team_names.Count;
            int[,] gamesToPlay = GenerateGames(num_teams);

            int numofRounds = num_teams;

            int[,] eventCounter = new int[num_teams, eventList.Count];
            int[,] roundEvents = new int[numofRounds, eventList.Count];
            int[,] roundTeams = new int[numofRounds, num_teams];

            string results = SetTournament(gamesToPlay, team_names, eventList, eventCounter, roundEvents, roundTeams, byeID);
            
            Console.WriteLine(results);

            for (int i = 0; i <= gamesToPlay.GetUpperBound(0); i++)
            {
                for (int ii = 0; ii <= gamesToPlay.GetUpperBound(1); ii++)
                    if (gamesToPlay[i,ii] == 0)
                        Console.WriteLine($"{team_names[i]} vs. {team_names[ii]}");
            }

            Console.ReadLine();
        
        }

        static string SetTournament(int[,] gamesToPlay, List<string> teamNames, List<string> games, int[,] eventCounter, int[,] roundCounter, int[,] teamRoundCounter, int byeID)
        {
            string roundPair = "";
            bool gameFound = false;

            int n = teamNames.Count;
            int numofRounds = n - 1;
            int gameLoops = games.Count;
  
            for (int r = 0; r < numofRounds; r++)
            {
                if (byeID == 1)
                {
                    roundPair += "\n" + r + ": " + teamNames[n - 1 - r] + " BYE";
                    teamRoundCounter[r, n - 1 - r] = 1;
                }

                for (int gg = 0; gg < gameLoops; gg++)
                {
                    gameFound = false;
                    int loopCount = 0;
                    if (roundCounter[r, gg] == 0)
                    {
                        int value1 = 0;
                        int value2 = 0;
                        int currentLow = 0;
                        int saveValue = 0;

                        while (gameFound == false && loopCount <= 100)
                        {
                            if (loopCount > 0)
                            {
                                if (value2 == numofRounds)
                                {
                                    value1 = currentLow + 1;
                                    value2 = value1;
                                    currentLow++;
                                }
                                else if (value1 == value2)
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

                            for (int t1 = 0; t1 <= gamesToPlay.GetUpperBound(0) && gameFound == false; t1++)
                            {
                                for (int t2 = 0; t2 <= gamesToPlay.GetUpperBound(1) && gameFound == false; t2++)
                                {
                                    if (gamesToPlay[t1, t2] == 0 && teamRoundCounter[r, t1] == 0 && teamRoundCounter[r, t2] == 0)
                                    {
                                        if (eventCounter[t1, gg] == value1 && eventCounter[t2, gg] == value2)
                                        {
                                            roundPair += "\n" + r + ": " + teamNames[t1] + " will play " + teamNames[t2] + " in " + games[gg];
                                            eventCounter[t1, gg]++;
                                            eventCounter[t2, gg]++;
                                            gamesToPlay[t1, t2] = r + 1;
                                            roundCounter[r, gg] = 1;
                                            teamRoundCounter[r, t1] = 1;
                                            teamRoundCounter[r, t2] = 1;
                                            gameFound = true;
                                        }
                                    }
                                }
                            }
                            loopCount++;
                        }
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
                    if (value2 == maxGames)
                    {
                        value1 = currentLow + 1;
                        value2 = value1;
                        currentLow++;
                    }
                    else if (value1 == value2)
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

        static int[,] GenerateGames(int numTeams)
        {
            int[,] results = new int[numTeams,numTeams];

            int[] teams = new int[numTeams];
            for (int i = 0; i < numTeams; i++) teams[i] = i;

            for (int i = 0; i < numTeams; i++)
            {
                for (int ii = 0; ii < numTeams; ii++)
                {
                    if (i == ii || i > ii)
                    {
                        results.SetValue(-1, i, ii);
                    }
                }
            }

            return results;
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
