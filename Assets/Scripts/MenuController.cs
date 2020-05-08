using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    GameObject restartMenuCanvas;
    void Start()
    {
        restartMenuCanvas = transform.Find("RestartMenuCanvas").gameObject;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            restartMenuCanvas.SetActive(!restartMenuCanvas.activeSelf);
            Time.timeScale = restartMenuCanvas.activeSelf ? 0 : 1;
        }
    }

    public void DoRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DoQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif   
    }
}
