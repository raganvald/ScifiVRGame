using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public void DoRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
