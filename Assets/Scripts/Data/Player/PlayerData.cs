using UnityEngine;
using Core;

namespace Data.User
{
    using System.Collections.Generic;
    using Utility;

    [System.Serializable]
    public class PlayerData
    {
        private const string saveKey = "PlayerData";

        public DynamicValue<int> gameplayLevel = new DynamicValue<int>();
        public DynamicValue<int> gameplayLevelAttempt = new DynamicValue<int>();
        public DynamicValue<int> coins = new DynamicValue<int>();


        public static PlayerData GetDummy()
        {
            PlayerData data = new PlayerData();
            data.gameplayLevel.Value = 0;
            data.coins.Value = Constants.CoinsValues.initialCoins;

            return data;
        }


        public static PlayerData Load()
        {
            PlayerData playerData = null;

            if (PlayerPrefs.HasKey(saveKey))
                playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(saveKey));
            else playerData = GetDummy();

            return playerData;
        }


        public static void Save(PlayerData data)
        {
            if (data == null) return;

            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(saveKey, json);
            PlayerPrefs.Save();
        }


        public static void BindChangeEvents(PlayerData data)
        {
            if (data == null)
                return;

            data.coins.OnChange += (v) => Save(data);
            data.gameplayLevel.OnChange += (v) => Save(data);
        }


        public static void CopyChangeEvents(PlayerData origin, PlayerData target)
        {
            if (origin == null || target == null)
                return;

            target.coins.CopyChangesRegister(origin.coins);
            target.gameplayLevel.CopyChangesRegister(origin.gameplayLevel);
        }
    }
}