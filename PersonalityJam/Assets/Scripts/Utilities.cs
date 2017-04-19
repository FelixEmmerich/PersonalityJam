using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    public void LoadScene(string name)
    {
        //Avoid The Singleton disc persisting across scenes
        if (Disc.Instance != null)
        {
            Destroy(Disc.Instance);
        }

        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}