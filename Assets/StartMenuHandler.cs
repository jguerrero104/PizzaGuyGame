using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuHandler : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("TutScene"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
