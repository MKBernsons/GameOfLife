using System;
using System.Collections.Generic;
using System.Threading;

namespace GameOfLife
{
    public static class GameManager
    {
        private static bool liveStats = false;
        private static List<Game> games = new();
        private static List<Thread> threads = new();
        public static List<Game> Games { get => games; set => games = value; }

        public static int AddGame(int size)
        {
            games.Add(new Game(size));
            return games.Count - 1;
        }

        public static void AddGameObject(Game game)
        {
            games.Add(game);
        }
        public static void AddAmountOfGames(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                games.Add(new Game(12));
            }
        }

        public static int AddVisualizedGame(int size)
        {
            games.Add(new Game(size));
            games[games.Count - 1].StartVisualizing();
            return games.Count - 1;
        }

        public static void PlayAllGames(bool enabledStats = true)
        {
            for (int i = 0; i < games.Count; i++)
            {
                games[i].Activate();
                threads.Add(new Thread(games[i].PlayGame));
                threads[threads.Count - 1].Start();
            }

            if (enabledStats)
            {
                liveStats = true;
                threads.Add(new Thread(LiveAllGameStatistics));
                threads[threads.Count - 1].Start();
            }
        }

        public static void PlayOneGame(int gameId, bool visualize = false)
        {
            try
            {
                if (visualize)
                    games[gameId].StartVisualizing();
                games[gameId].Activate();
                threads.Add(new Thread(games[gameId].PlayGame));
                threads[threads.Count - 1].Start();
            }
            catch (Exception)
            {
                Console.WriteLine("Game doesn't exist");
            }
        }

        public static void StopAllGames()
        {
            liveStats = false;
            foreach (Game game in games)
            {
                game.StopVisualizing();
                game.Stop();
            }
            for (int i = 0; i < threads.Count; i++)
            {
                if (!threads[i].IsAlive)
                    threads.RemoveAt(i);
            }
        }

        public static bool PrintAllGameIds()
        {
            if(games.Count > 0)
            {
                foreach (Game game in games)
                {
                    Console.WriteLine($"Id: {game.GameId}, iterations: {game.IterationCount}, live cells: {game.LiveCells}");
                }
                return true;
            }
            else
            {
                Console.WriteLine("there are no games yet\n");
                return false;
            }
        }

        public static Game GetGameById(int id)
        {
            return games[id];
        }

        public static bool GamesExist()
        {
            if (games.Count > 0)
                return true;
            else
                return false;
        }

        private static void LiveAllGameStatistics()
        {
            while (liveStats)
            {
                Console.Clear();
                int activeGames = 0;
                foreach (Game game in games)
                {
                    if (game.IsActive)
                        activeGames++;
                }
                Console.WriteLine($"total games:        {Game.TotalGames}\n" +
                                  $"total active games: {activeGames}\n" +
                                  $"total live cells:   {Game.TotalLiveCells}");
                Thread.Sleep(1000);
            }
        }

        public static void PlayAllGamesAndVisualizeSome(int[] gamesToPlay)
        {
            if(gamesToPlay.Length <= games.Count && games.Count > 0)
            {
                foreach (int id in gamesToPlay)
                {
                    games[id].StartVisualizing();
                }
                PlayAllGames(false);
            }
            else
                Console.WriteLine("There was nothing to visualize, either the action got cancelled or there were 0 games");

        }
    }
}
