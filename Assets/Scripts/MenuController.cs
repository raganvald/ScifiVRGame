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
        StartCoroutine(StopTime());
    }


    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            restartMenuCanvas.SetActive(!restartMenuCanvas.activeSelf);
            Time.timeScale = restartMenuCanvas.activeSelf ? 0 : 1;
        }
    }

    public void DoContinue()
    {
        restartMenuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void DoRestart()
    {
        DoContinue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator StopTime()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
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
