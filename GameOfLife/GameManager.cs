using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace GameOfLife
{
    public class GameManager
    {
        private List<Game> games = new List<Game>();
        private List<Thread> threads = new List<Thread>();
        private string saveLocation = @"C:\Users\mikelis.k.bernsons\source\repos\MKBernsons\GameOfLife\GameOfLife\save.json";
        public void AddGame(int size)
        {
            games.Add(new Game(size));
        }
        public void AddVisualizedGame(int size)
        {
            games.Add(new Game(size));
            games[games.Count - 1].SelectToVisualize();
        }
        public void PlayAllGames()
        {
            for (int i = 0; i < games.Count; i++)
            {
                threads.Add(new Thread(games[i].PlayGame));
                threads[threads.Count - 1].Start();
            }
        }
        public void PlayOneGame(int gameId)
        {
            try
            {
                games[gameId].SelectToVisualize();
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
            foreach (Game game in games)
            {
                game.StopVisualizing();
                game.Stop();
            }
        }
        public void ShowAllGames()
        {
            if(games.Count > 0)
            {
                foreach (Game game in games)
                {
                    Console.WriteLine($"Id: {game.GameId}, iterations: {game.iterationCount}, live cells: {game.liveCells}");
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
            File.WriteAllText(saveLocation, json);
        }
        public void LoadOneGame()
        {
            string json = File.ReadAllText(saveLocation);
            games.Add(JsonConvert.DeserializeObject<Game>(json));
        }
        public bool GamesExist()
        {
            if (games.Count > 0)
                return true;
            else
                return false;
        }
    }
}
