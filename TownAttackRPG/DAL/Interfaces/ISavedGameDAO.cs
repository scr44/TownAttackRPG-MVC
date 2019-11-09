using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.Models.ViewModels.Game;

namespace TownAttackRPG.DAL.Interfaces
{
    public interface ISaveGameDAO
    {
        // TODO User ID: save/set user ID using telemetry: https://docs.microsoft.com/en-us/azure/azure-monitor/app/usage-send-user-context
        // TODO better deserialization: https://skrift.io/articles/archive/bulletproof-interface-deserialization-in-jsonnet/

        void SaveGame(int userID, int slot, GameData gameData);

        GameData LoadGame(int userID, int slot);
    }
}
