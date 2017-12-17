using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight
{


    public class HighScoreManager : MonoBehaviour
    {

        private static HighScoreManager m_instance;
        private const int LeaderboardLength = 10;

        public static HighScoreManager _instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GameObject("HighScoreManager").AddComponent<HighScoreManager>();
                }
                return m_instance;
            }
        }

        void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else if (m_instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void SaveHighScore(string name, int floor, int kills)
        {
            List<Scores> HighScores = new List<Scores>();

            int i = 1;
            while (i <= LeaderboardLength && PlayerPrefs.HasKey("HighScore" + i + "floor"))
            {
                Scores temp = new Scores();
                temp.floor = PlayerPrefs.GetInt("HighScore" + i + "floor");
                temp.kills = PlayerPrefs.GetInt("HighScore" + i + "kills");
                temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
                HighScores.Add(temp);
                i++;
            }
            if (HighScores.Count == 0)
            {
                Scores _temp = new Scores();
                _temp.name = name;
                _temp.floor = floor;
                _temp.kills = kills;
                HighScores.Add(_temp);
            }
            else
            {
                for (i = 1; i <= HighScores.Count && i <= LeaderboardLength; i++)
                {
                    if (floor > HighScores[i - 1].floor)
                    {
                        Scores _temp = new Scores();
                        _temp.name = name;
                        _temp.floor = floor;
                        _temp.kills = kills;
                        HighScores.Insert(i - 1, _temp);
                        break;
                    }
                    if (i == HighScores.Count && i < LeaderboardLength)
                    {
                        Scores _temp = new Scores();
                        _temp.name = name;
                        _temp.floor = floor;
                        _temp.kills = kills;
                        HighScores.Add(_temp);
                        break;
                    }
                }
            }

            i = 1;
            while (i <= LeaderboardLength && i <= HighScores.Count)
            {
                PlayerPrefs.SetString("HighScore" + i + "name", HighScores[i - 1].name);
                PlayerPrefs.SetInt("HighScore" + i + "floor", HighScores[i - 1].floor);
                PlayerPrefs.SetInt("HighScore" + i + "kills", HighScores[i - 1].kills);
                i++;
            }

        }

        public List<Scores> GetHighScore()
        {
            List<Scores> HighScores = new List<Scores>();

            int i = 1;
            while (i <= LeaderboardLength && PlayerPrefs.HasKey("HighScore" + i + "floor"))
            {
                Scores temp = new Scores();
                temp.floor = PlayerPrefs.GetInt("HighScore" + i + "floor");
                temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
                temp.kills = PlayerPrefs.GetInt("HighScore" + i + "kills");
                HighScores.Add(temp);
                i++;
            }

            return HighScores;
        }

        public void ClearLeaderBoard()
        {
            //for(int i=0;i<HighScores.
            List<Scores> HighScores = GetHighScore();

            for (int i = 1; i <= HighScores.Count; i++)
            {
                PlayerPrefs.DeleteKey("HighScore" + i + "name");
                PlayerPrefs.DeleteKey("HighScore" + i + "floor");
                PlayerPrefs.DeleteKey("HighScore" + i + "kills");

            }
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }
    }

    public class Scores
    {
        public int floor;
        public int kills;
        public string name;

    }

}
