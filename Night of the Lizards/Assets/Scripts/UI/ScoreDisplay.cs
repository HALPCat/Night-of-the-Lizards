using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LizardNight
{
    public class ScoreDisplay : MonoBehaviour
    {

        string name = "";
        string floor = "";
        string kills = "";
        List<Scores> highscore;
        public List<Text> namesList;
        public List<Text> floorList;
        public List<Text> killsList;

        // Use this for initialization
        void Start()
        {

            highscore = HighScoreManager._instance.GetHighScore();

            UpdateScore();

        }

        void UpdateScore()
        {

            if (highscore != null)
            {
                for (int i = 0; i < highscore.Count; i++)
                {
                    if (highscore[i].name != null)
                    {
                        Scores temp = highscore[i];

                        namesList[i].text = temp.name;
                        floorList[i].text = temp.floor.ToString();
                        killsList[i].text = temp.kills.ToString();
                    }

                    else
                    {
                        namesList[i].text = "no";
                        floorList[i].text = "nope";
                        killsList[i].text = "none";
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    namesList[i].text = "no";
                    floorList[i].text = "nope";
                    killsList[i].text = "none";
                }
            }

        }
    



    public void ClearScore()
    {
        HighScoreManager._instance.ClearLeaderBoard();

        UpdateScore();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //void OnGUI()
    //{      

    //    GUILayout.Space(200);

    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("Name", GUILayout.Width(Screen.width / 3));
    //    GUILayout.Label("Floor", GUILayout.Width(Screen.width / 3));
    //    GUILayout.Label("Kills", GUILayout.Width(Screen.width / 3));
    //    GUILayout.EndHorizontal();

    //    GUILayout.Space(25);

    //    foreach (Scores _score in highscore)
    //    {
    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label(_score.name, GUILayout.Width(Screen.width / 3));
    //        GUILayout.Label("" + _score.floor, GUILayout.Width(Screen.width / 3));
    //        GUILayout.Label("" + _score.kills, GUILayout.Width(Screen.width / 3));
    //        GUILayout.EndHorizontal();
    //    }
    //}
}
}
