using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Übernimmt das Ein- und Ausblenden der Szenen.
/// </summary>
public class ScreenFader : MonoBehaviour
{
    /// <summary>
    /// Das Bild dessen Alpha Kanal geändert wird,
    /// um das Ein- und Ausblenden der Szene zu realisieren.
    /// </summary>
    public Image overlay;

    /// <summary>
    /// Führt das Ein- und Ausblenden des Schwetztbildes durch.
    /// </summary>
    /// <returns>(Enumerator)</returns>
    /// <param name="toAlpha">Ziel-Tranzparenz zwischen 0 und 1</param>
    /// <param name="revertToSaveGame">Wenn true, dann wird nach dem Überblenden der letzte Spielstand geladen.</param>
    /// <param name="delay">Anzahl von Sekunden, die gewartet werden soll, bevor das Überblenden startet.</param>
    private IEnumerator performFading(float toAlpha, bool revertToSaveGame, float delay)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        overlay.CrossFadeAlpha(toAlpha, 1f, false);

        yield return new WaitForSeconds(1f);

        if (revertToSaveGame)
        {
            SaveGameData.current = SaveGameData.load();
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.loadScene(SaveGameData.current.recentScene);
        }
    }

    /// <summary>
    /// Blendet die Szene ein.
    /// </summary>
    /// <param name="revertToSaveGame">Wenn true, dann wird nach dem Überblenden der letzte Spielstand geladen.</param>
    /// <param name="delay">Sekunden, die vor dem Einblenden gewartet werden sollen</param>
    public void fadeIn(bool revertToSaveGame, float delay = 0f)
    {
        StartCoroutine(performFading(0f, revertToSaveGame, delay));
    }

    /// <summary>
    /// Blendet die Szene aus.
    /// </summary>
    /// <param name="revertToSaveGame">Wenn true, wird nach dem Überblenden der letzte Speicherstand geladen.</param>
    /// <param name="delay">Sekunden, die vor dem Einblenden gewartet werden sollen</param>
    public void fadeOut(bool revertToSaveGame, float delay = 0f)
    {
        StartCoroutine(performFading(1f, revertToSaveGame, delay));
    }


    private void Awake()
    {
        //overlay.gameObject.SetActive(true);
        SceneManager.sceneLoaded += WhenLevelWasLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= WhenLevelWasLoaded;
    }

    private void WhenLevelWasLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeIn(false);
    }
}
