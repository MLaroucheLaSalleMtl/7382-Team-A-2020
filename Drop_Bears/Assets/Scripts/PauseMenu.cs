using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;

    public GameObject PauseMenuUI;

    float saveTimeScale;

    private AsyncOperation async; //<--For future Shit
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = saveTimeScale;
        isGamePaused = false;
    }
    void Pause()
    {
        saveTimeScale = Time.timeScale;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        #region QuitFromEditor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit()
        #endif
        #endregion QuitFromEditor
    }
}
