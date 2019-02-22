using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void OnStartNewGame()
    {
        SaveGameData.current = new SaveGameData();
        SceneManager.LoadScene("Globals");
    }
}