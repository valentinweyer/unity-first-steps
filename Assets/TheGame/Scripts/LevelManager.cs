using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private void Awake()
    {
        SaveGameData.current = SaveGameData.load();
    }

    private void Start()
    { 
        loadScene(SaveGameData.current.recentScene);
    }

    public void loadScene(string name)
    {
        if (name == "")
            return; //unglültiger Aufruf.

        for (int i = SceneManager.sceneCount-1; i > 0; i--) // i-- == i=i-1
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
        }

        Debug.Log("Lade jetzt Szene:" + name);
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    #if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            SaveGameData.current = SaveGameData.load();
    }
    #endif
}
