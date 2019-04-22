using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using TheMaze.Models.GameObjects;

namespace TheMaze.Models
{
    [DataContract]
    public class Player : GameElement
    {
        public const int MAX_LIFE_POINTS = 3;

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

        public Player()
        {
            CountSteps = 0;
            CountKeys = 0;
            CountCoins = 0;
            PositionTop = 0;
            PositionLeft = 0;
        }

        public void IncreaseLifePoints()
        {
            CountLifePoints = CountLifePoints + 1 <= MAX_LIFE_POINTS ? ++CountLifePoints : CountLifePoints;
        }

        public void DecreaseLifePoints()
        {
            CountLifePoints = CountLifePoints - 1 >= 0 ? --CountLifePoints : CountLifePoints;
        }

        public void IncreaseSteps()
        {
            CountSteps++;
        }

        public void IncreaseKeys()
        {
            CountKeys++;
        }

        public void IncreaseCoins()
        {
            CountCoins++;
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
    }
}
