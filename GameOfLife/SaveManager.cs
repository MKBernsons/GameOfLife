using Newtonsoft.Json;
using System;
using System.IO;
using static GameOfLife.GameManager;

namespace GameOfLife
{
    public static class SaveManager
    {
        private static string oneGameSave;
        private static string allGameSave;

        public static void SetupSaveManager()
        {
            oneGameSave = Directory.GetCurrentDirectory() + @"\OneSave.json";
            allGameSave = Directory.GetCurrentDirectory() + @"\saves\";
            Directory.CreateDirectory(allGameSave);
        }

        public static void SaveOneGame(Game game)
        {
            string json = JsonConvert.SerializeObject(game);
            File.WriteAllText(oneGameSave, json);
        }

        public static void LoadOneGame()
        {
            try
            {
                string json = File.ReadAllText(oneGameSave);
                AddGameObject(JsonConvert.DeserializeObject<Game>(json));
            }
            catch (Exception)
            {

                throw new Exception("Load file error, the file probably does not exist");
            }
        }

        public static void SaveAllGames()
        {
            Console.Clear();
            Directory.CreateDirectory(allGameSave);// creates the folder if it doesn't exist
            foreach (FileInfo file in new DirectoryInfo(allGameSave).GetFiles())//deletes all previous save files
            {
                file.Delete();
            }

            try
            {
                for (int i = 0; i < Games.Count; i++)
                {
                    string json = JsonConvert.SerializeObject(Games[i]);
                    File.WriteAllText(allGameSave + $"game{i}.json", json);
                }
                Console.WriteLine("Saved all existing games (can be none if none exist)");
            }
            catch (Exception)
            {
                Console.WriteLine("Save all games failed");
                throw new Exception();
            }
        }

        public static void LoadAllGames()
        {
            Console.Clear();
            Directory.CreateDirectory(allGameSave);
            Games.Clear();
            Game.TotalGames = 0;
            try
            {
                string[] files = Directory.GetFiles(allGameSave);
                foreach (string file in files)
                {
                    if (Path.GetExtension(file) == ".json")// checks if the file extension is .json
                    {
                        string json = File.ReadAllText(file);
                        Games.Add(JsonConvert.DeserializeObject<Game>(json));
                    }
                }
                Console.WriteLine("Loaded all saved games (can be none if none exist)");
            }
            catch (Exception)
            {
                throw new Exception("Load all games failed");
            }
        }

    }
}
