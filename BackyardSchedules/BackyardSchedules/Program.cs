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
            List<string> eventList = new List<string> { "bags", "darts", "bocce" };

            // Get the teams.
            // Textbox on the webform -- string all_teams = txtTeams.Text;
            string all_teams = "One\rTwo\nThree\nFour\nFive\nSix";

            List<string> team_names = GenerateTeams(all_teams);

            int rotations = 1; //input option

            //string results = SetTournament(team_names, eventList, rotations);
            RoundRobin rr = new RoundRobin();
            string results = rr.SetRoundRobin(team_names, eventList, rotations);

            Console.WriteLine(results);
            Console.ReadLine();
        
        }

        static string SetTournament(List<string> teamNames, List<string> games, int rotations)
        {
            string roundPair = "";
            bool byes = false;
            bool gameFound = false;

            // Get the schedule.
            int gameCount = games.Count;
            int num_teams = teamNames.Count;
            if (num_teams % 2 > 0)
                byes = true;

            int roundsPerRotation;
            if (byes)
                roundsPerRotation = num_teams;
            else
                roundsPerRotation = (num_teams - 1);

            int numofRounds = roundsPerRotation * rotations;

            int[,] gamesToPlay = GenerateGames(num_teams);            
            int[,] eventCounter = new int[num_teams, gameCount];
            int[,] roundCounter = new int[numofRounds, gameCount];
            int[,] teamRoundCounter = new int[numofRounds, num_teams];

            int maxLoops = (3 * gameCount) * numofRounds + 1;

            int currentRound = -1;

            for (int rotation = 0; rotation < rotations; rotation++)
            {
                for (int round = 0; round < roundsPerRotation; round++)
                {
                    currentRound++;

                    //if (byes)
                    //{
                    //    roundPair += "\n" + (currentRound + 1) + ": " + teamNames[num_teams - 1 - round] + " BYE";
                    //    teamRoundCounter[currentRound, num_teams - 1 - round] = currentRound + 1;
                    //}

                    for (int gameLoops = 0; gameLoops < gameCount; gameLoops++)
                    {
                        int loopCount = 0;
                        gameFound = false;
                        int value1 = 0;
                        int value2 = 0;
                        int currentLow = 0;
                        int saveValue;

                        while (gameFound == false && loopCount <= maxLoops) //prevent infinite loop if something goes wrong
                        {
                            if (loopCount > 0)
                            {
                                if (value2 == roundsPerRotation)
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
                                    if (gamesToPlay[t1, t2] == rotation && teamRoundCounter[currentRound, t1] == 0 && teamRoundCounter[currentRound, t2] == 0)
                                    {
                                        for (int gg = 0; gg < gameCount; gg++)
                                        {
                                            if (roundCounter[currentRound, gg] == 0)
                                            {
                                                if (eventCounter[t1, gg] == value1 && eventCounter[t2, gg] == value2)
                                                {
                                                    roundPair += "\n" + (currentRound + 1) + ": " + teamNames[t1] + " will play " + teamNames[t2] + " in " + games[gg];
                                                    eventCounter[t1, gg]++;
                                                    eventCounter[t2, gg]++;
                                                    gamesToPlay[t1, t2]++;
                                                    roundCounter[currentRound, gg]++;
                                                    teamRoundCounter[currentRound, t1]++;
                                                    teamRoundCounter[currentRound, t2]++;
                                                    gameFound = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            loopCount++;
                        }
                    }
                }
            }

            //Safety check for missing games
            for (int i = 0; i <= gamesToPlay.GetUpperBound(0); i++)
            {
                for (int ii = 0; ii <= gamesToPlay.GetUpperBound(1); ii++)
                    if (gamesToPlay[i, ii] > -1 && gamesToPlay[i, ii] < rotations)
                        roundPair += "\n" + teamNames[i] + " vs. " + teamNames[ii] + "No Game Set";
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
