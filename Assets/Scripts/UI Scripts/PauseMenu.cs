using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    //public static PauseMenu instance;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    public static bool isGamePaused = false;

    public GameObject PauseMenuUI;

    float saveTimeScale;

    private bool onlyOnce = false;

    

    private AsyncOperation async; //<--For future Shit


    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(isGamePaused)
            {
                Resume();
                isGamePaused = false;
            }
            else if (!isGamePaused)
            {
                Pause();
                isGamePaused = true;
            }
            onlyOnce = true;
            Debug.Log("poop");
      
        }
    }
    // Update is called once per frame
    void Update()
    {

        //if (onlyOnce)
        //{
        //    if (isGamePaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}

    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = saveTimeScale;
        isGamePaused = false;
        onlyOnce = false;
    }
    void Pause()
    {
        saveTimeScale = Time.timeScale;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
        isGamePaused = true;
        onlyOnce = false;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        //#region QuitFromEditor
        //#if UNITY_EDITOR
        //UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //Application.Quit()
        //#endif
        //#endregion QuitFromEditor
    }
}
