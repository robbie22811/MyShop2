using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackyardSchedules
{
    internal class RoundRobin
    {
        public List<Matches> SetRoundRobin(List<string> teams, List<string> games, int rotations)
        {
            List<Matches> matchResult = new List<Matches>();

            int[,] gamesToPlay = GenerateRoundRobin(teams.Count);
            int rounds = gamesToPlay.GetUpperBound(1) + 1;

            int[,] eventCounter = new int[teams.Count, games.Count];
            int[,] roundCounter = new int[rounds, games.Count];
            int[,] teamRoundCounter = new int[teams.Count, rounds];
            int maxLoops = rounds * teams.Count + 1;
            int currentRound = 0;
            int teamId1 = 0;

            for (int rotation = 0; rotation < rotations; rotation++)
            {
                for (int round = 0; round < rounds; round++)
                {
                    currentRound++;
                    int startID = round;
                    if (startID >= teams.Count)
                        startID = 0;

                    for (int id = 0; id < teams.Count; id++)
                    { 
                        if (id > 0)
                            teamId1++;
                        else
                            teamId1 = startID;

                        if (teamId1 >= teams.Count)
                            teamId1 = 0;

                        bool gameFound = false;
                        int teamId2 = gamesToPlay[teamId1, round];
                        if (teamId2 == -1)
                        {
                            matchResult.Add(new Matches { RoundNumber = currentRound, Event = "BYE", TeamOne = teams[teamId1] });
                        }
                        else if (teamId1 < teamId2)
                        {
                            for (int gameLoops = 0; gameLoops < games.Count && gameFound == false; gameLoops++)
                            {
                                int loopCount = 0;
                                int value1 = 0;
                                int value2 = 0;
                                int currentLow = 0;
                                int saveValue;

                                while (gameFound == false && loopCount <= maxLoops) //prevent infinite loop if something goes wrong
                                {
                                    if (loopCount > 0)
                                    {
                                        if (value2 == rounds)
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

                                    if (round % 2 == 0)
                                    {
                                        for (int gg = (games.Count-1); gg >= 0; gg--)
                                        {
                                            if (roundCounter[round, gg] == rotation)
                                            {
                                                if (eventCounter[teamId1, gg] == value1 && eventCounter[teamId2, gg] == value2)
                                                {
                                                    matchResult.Add(new Matches { RoundNumber = currentRound, Event = games[gg], TeamOne = teams[teamId1], TeamTwo = teams[teamId2] });
                                                    eventCounter[teamId1, gg]++;
                                                    eventCounter[teamId2, gg]++;
                                                    teamRoundCounter[teamId1, round]++;
                                                    teamRoundCounter[teamId2, round]++;
                                                    roundCounter[round, gg]++;
                                                    gameFound = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int gg = 0; gg < games.Count; gg++)
                                        {
                                            if (roundCounter[round, gg] == rotation)
                                            {
                                                if (eventCounter[teamId1, gg] == value1 && eventCounter[teamId2, gg] == value2)
                                                {
                                                    matchResult.Add(new Matches { RoundNumber = currentRound, Event = games[gg], TeamOne = teams[teamId1], TeamTwo = teams[teamId2] });
                                                    eventCounter[teamId1, gg]++;
                                                    eventCounter[teamId2, gg]++;
                                                    teamRoundCounter[teamId1, round]++;
                                                    teamRoundCounter[teamId2, round]++;
                                                    roundCounter[round, gg]++;
                                                    gameFound = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    loopCount++;
                                }
                            }
                        }
                    }
                }    
            }
            return matchResult;
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
