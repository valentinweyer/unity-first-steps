using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    /// <summary>
    /// Wurzelelement, das das gesamte Menü ein- oder ausblendet.
    /// </summary>
    public GameObject menuRoot;

    // Use this for initialization
    void Start ()
    {
        menuRoot.SetActive(false);
    }

    /// <summary>
    /// Wahr, wenn die Taste bereits zuvor bereits als gedrückt erkannt wurde.
    /// Nötig, um Mehrfachauswertungen der Menütaste zu verhindern.
    /// </summary>
    private bool keyWasPressed = false;
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetAxisRaw ("Menu") > 0f)
        {
            if (!keyWasPressed)
            {
                menuRoot.SetActive(!menuRoot.activeSelf);

                Time.timeScale = menuRoot.activeSelf ? 0f : 1f;
            }
            keyWasPressed = true; 
        }
        else
        {
            keyWasPressed = false;
        }
	}

    /// <summary>
    /// Startet ein neues Spiel, wenn auf den Neu-Button im Menü geklickt wird.
    /// </summary>
    public void OnButtonNewPressed()
    {
        if (Time.timeScale != 0f)
        {
            return; //menü ist nicht sichtbar
        }
        SaveGameData.current = new SaveGameData();
        LevelManager lm = FindObjectOfType<LevelManager>();
        lm.loadScene("Szene1");

        menuRoot.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Beendet das Spiel, wenn auf den Quit-Button im Menü gedrückt wird.
    /// </summary>
    public void OnButtonQuitPressed()
    {
        Debug.Log("Spiel beenden...");
        Application.Quit();
    }
}
