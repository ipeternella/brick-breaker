using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace UI.LevelMap
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance => _instance;

        private const string MapLocation = "Key";

        [SerializeField] private StarCountController starCountController;

        private List<Map> _maps;

        public int CurrentLevel { get; set; }
        public int HighestLevel { get; private set; } //level lớn nhất hiện thời

        private float _starCount;
        public float StarCount => _starCount;

        public int MapCount => _maps.Count;
        public List<Map> Maps => _maps;


        private void Awake()
        {
            if (_instance != null) return;
            _instance = this;
            _maps = LoadMap();// bắt đầu game load map
        }

        public List<Map> LoadMap()
        {
            List<Map> maps = JsonConvert.DeserializeObject<List<Map>>(PlayerPrefs.GetString(MapLocation));
            if (maps == null) // nếu map rỗng player mới vào game
            {
                maps = new List<Map>(); //mở khoá màn 1 cho player
                for (int i = 0; i < 40; i++)
                {
                    maps.Add(new Map()
                    {
                        level = i + 1,
                        starCount = 0,
                        isLock = i != 0
                    });
                }
            }

           // _starCount = 0;
            foreach (var map in maps)
            {
                _starCount += map.starCount;// load số sao mỗi map
                if (map.isLock) continue; //load khoá
                if (map.level > HighestLevel) //nếu map.level cao hơn level cao nhất thì =
                    HighestLevel = map.level;
            }

            starCountController.Render();
            return maps; //dừng
        }

        public void SaveMap()
        {
            PlayerPrefs.SetString(MapLocation, JsonConvert.SerializeObject(_maps));
        }

        public void SetVictory(int level)
        {
            //Convert level to index of maps
            CurrentLevel++;
            if (level - 1 < 0 || level - 1 >= _maps.Count) return;
            _maps[level - 1].starCount = 3;
            if (level <= _maps.Count)
            {
                _maps[level].isLock = false;
            }

            SaveMap();
        }
    }
}