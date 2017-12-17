using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    [Tooltip("The name of the scene that is loaded when the player presses New Game Button.")]
    [SerializeField]
    private string levelName;

    public void NewGame() {
        SceneManager.LoadScene(levelName);
    }

    public void HighScore()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
