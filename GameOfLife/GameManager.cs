﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace GameOfLife
{
    public class GameManager
    {
        private static bool liveStats = false;
        private List<Game> games = new List<Game>();
        private List<Thread> threads = new List<Thread>();
        private string oneGameSave = @"C:\Users\mikelis.k.bernsons\source\repos\MKBernsons\GameOfLife\GameOfLife\OneSave.json";
        private string allGameSave = @"C:\Users\mikelis.k.bernsons\source\repos\MKBernsons\GameOfLife\GameOfLife\saves\";

        public int AddGame(int size)
        {
            games.Add(new Game(size));
            return games.Count - 1;
        }
        public void AddAmountOfGames(int amount)
        {
            Console.WriteLine($"creating {amount} games");
            for (int i = 0; i < amount; i++)
            {
                games.Add(new Game(12));
            }
            Console.WriteLine("DONE");
        }

        public int AddVisualizedGame(int size)
        {
            games.Add(new Game(size));
            games[games.Count - 1].StartVisualizing();
            return games.Count - 1;
        }
        
        public void PlayAllGames()
        {            
            for (int i = 0; i < games.Count; i++)
            {
                games[i].Activate();
                threads.Add(new Thread(games[i].PlayGame));
                threads[threads.Count - 1].Start();
            }
            liveStats = true;
            threads.Add(new Thread(LiveAllGameStatistics));
            threads[threads.Count - 1].Start();
        }
        
        public void PlayOneGame(int gameId, bool visualize = false)
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
        
        public void StopAllGames()
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
        
        public void ShowAllGames()
        {
            if(games.Count > 0)
            {
                foreach (Game game in games)
                {
                    Console.WriteLine($"Id: {game.GameId}, iterations: {game.IterationCount}, live cells: {game.LiveCells}");
                }
            }
            else
                Console.WriteLine("there are no games yet");            
        }
        
        public Game GetGameById(int id)
        {
            return games[id];
        }
        
        public void SaveOneGame(Game game)
        {
            string json = JsonConvert.SerializeObject(game);
            File.WriteAllText(oneGameSave, json);
        }
        
        public void LoadOneGame()
        {
            string json = File.ReadAllText(oneGameSave);
            games.Add(JsonConvert.DeserializeObject<Game>(json));
        }

        public void SaveAllGames()
        {
            for(int i = 0; i < games.Count; i++)
            {
                string json = JsonConvert.SerializeObject(games[i]);
                File.WriteAllText(allGameSave + $"game{i}.json", json);
            }
            Console.WriteLine("Saved all games");
        }
        
        public void LoadAllGames()
        {
            games.Clear();
            string[] files = Directory.GetFiles(allGameSave);
            foreach (string file in files)
            {
                string json = File.ReadAllText(file);
                games.Add(JsonConvert.DeserializeObject<Game>(json));
            }
            Console.WriteLine("Loaded all games");
        }
        public bool GamesExist()
        {
            if (games.Count > 0)
                return true;
            else
                return false;
        }
        
        private void LiveAllGameStatistics()
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
    }
}
