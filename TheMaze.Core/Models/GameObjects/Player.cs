using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using TheMaze.Core.Configurations;
using TheMaze.Core.Enums;
using TheMaze.Core.Interfaces;

namespace TheMaze.Core.Models.GameObjects
{
    [KnownType(typeof(GameObject))]
    [DataContract]
    public class Player : GameObject, IGameSerialization
    {
        public const int MAX_LIFE_POINTS = 3;
        public const int MAX_STEP_PER_TIME = 2;

        [DataMember]
        public int CountLifePoints { get; private set; } = MAX_LIFE_POINTS;
        [DataMember]
        public int CountSteps { get; private set; }
        [DataMember]
        public int CountKeys { get; private set; }
        [DataMember]
        public int CountCoins { get; private set; }
        [DataMember]
        public string PlayerName { get; set; }
        [DataMember]
        public DateTime StarTime { get; set; }
        [DataMember]
        public int StepsPerTime { get; set; }
        [DataMember]
        public int CountCrystals { get; set; }
        [DataMember]
        public int CountGamePoints { get; set; }

        public Player()
        {
            CountSteps = 0;
            CountKeys = 0;
            CountCoins = 0;
            CountCrystals = 0;
            CountGamePoints = 0;

            PositionTop = 0;
            PositionLeft = 0;
            ColorForeground = CellColor.PlayerForeground;
            ColorBackground = CellColor.PlayerBackground;
            Symbol = '0';
            StepsPerTime = 1;
        }

        public void IncreaseStepPerTime()
        {
            StepsPerTime = StepsPerTime + 1 <= MAX_STEP_PER_TIME ? ++StepsPerTime : StepsPerTime;
        }

        public void DecreaseLifePoints()
        {
            CountLifePoints = CountLifePoints - 1 >= 0 ? --CountLifePoints : CountLifePoints;
        }

        public void IncreaseSteps()
        {
            CountSteps++;
        }

        public void IncreaseGamePoints(int value)
        {
            CountGamePoints += value;
        }

        public void IncreaseKeys()
        {
            CountKeys++;
            IncreaseGamePoints(Configuration.KEY_VALUE);
        }

        public void IncreaseCoins()
        {
            CountCoins++;
            IncreaseGamePoints(Configuration.COIN_VALUE);
        }

        public void Save()
        {
            var serializer = new DataContractJsonSerializer(typeof(Player));
            var path = $"{Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}Player.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(stream, (object)this);
            }
        }

        public bool Load()
        {
            var result = true;
            var serializer = new DataContractJsonSerializer(typeof(Player));
            var path = $"{Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}Player.json";

            if (!File.Exists(path))
            {
                return false;
            }

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (stream.Length > 0)
                {
                    var playerObject = serializer.ReadObject(stream);
                    if (playerObject != null)
                    {
                        var player = (Player) playerObject;
                        CountCoins = player.CountCoins;
                        CountKeys = player.CountKeys;
                        CountLifePoints = player.CountLifePoints;
                        CountSteps = player.CountSteps;
                        PlayerName = player.PlayerName;
                        PositionTop = player.PositionTop;
                        PositionLeft = player.PositionLeft;
                        StarTime = player.StarTime;
                    }
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public void SetPosition(int top, int left)
        {
            PositionTop = top;
            PositionLeft = left;
        }

        public void IncreaseCrystals()
        {
            CountCrystals++;
            IncreaseGamePoints(Configuration.CRYSTAL_VALUE);
        }

        public void ResetData()
        {
            CountSteps = 0;
            CountKeys = 0;
            CountCoins = 0;
            CountCrystals = 0;
            CountGamePoints = 0;

            PositionTop = 0;
            PositionLeft = 0;
            StepsPerTime = 1;
            CountLifePoints = MAX_LIFE_POINTS;            
        }
    }
}
