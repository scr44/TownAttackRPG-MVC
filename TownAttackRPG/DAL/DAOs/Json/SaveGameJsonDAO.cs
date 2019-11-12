using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.DAL.Interfaces;
using TownAttackRPG.Models.ViewModels.Game;

namespace TownAttackRPG.DAL.DAOs.Json
{
    public class SaveGameJsonDAO : ISaveGameDAO
    {
        readonly string JsonFolderPath;
        public SaveGameJsonDAO(string jsonFolderPath)
        {
            this.JsonFolderPath = jsonFolderPath;
        }

        public void SaveGame(int userID, int slot, GameData gameData)
        {
            gameData.SaveGameSlot = slot;

            /// 
            gameData.Atomize(); /// deconstruct the ActiveScenario object
            ///

            string path = JsonFolderPath + $"/SaveGame{slot}.json";
            string jsonData = JsonConvert.SerializeObject(gameData, new JsonSerializerSettings()
            { 
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });
            File.WriteAllText(path, jsonData);
        }

        public GameData LoadGame(int userID, int slot)
        {
            string path = JsonFolderPath + $"/SaveGame{slot}.json";
            using (StreamReader sr = new StreamReader(path))
            {
                string jsonData = sr.ReadToEnd();
                GameData gameData = JsonConvert.DeserializeObject<GameData>(jsonData,new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                });
                gameData.SaveGameSlot = slot;

                if (gameData.ScenarioName is null)
                {
                    gameData.ActiveScenario = null;
                }
                else
                {
                    ///
                    gameData.DeAtomize(); /// construct the ActiveScenario object
                    ///
                }


                return gameData;
            }
        }

        public void DeleteAllSaves(int userID)
        {
            for (int i=1;i<=3;i++)
            {
                string path = JsonFolderPath + $"/SaveGame{i}.json";
                string wipe = @"{

}";
                File.WriteAllText(path, wipe);
            }
        }
    }
}
