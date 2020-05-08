using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour{


    #region SINGLETON PATTERN
    public static GameMgr _instance;
    public static GameMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameMgr>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("Bicycle");
                    _instance = container.AddComponent<GameMgr>();
                }
            }

            return _instance;
        }
    }
    #endregion


    public bool gameOver = false;
    public GameObject player;
    public int killCount = 0;
    public GameObject scoreBoard;
    public GameObject scoreContainer;
    private TextMeshProUGUI score;

    void Awake()
    {
        gameOver = false;
        score = scoreContainer.GetComponent<TextMeshProUGUI>();
        score.text = "0";

    }

    public void GameOver()
    {
        gameOver = true;
        StartCoroutine(RestartMap());
    }
    

    IEnumerator RestartMap()
    {
        yield return new WaitForSeconds(1);
        score.text = killCount.ToString();
        scoreBoard.SetActive(true);
        
        yield return new WaitForSeconds(5);
        scoreBoard.SetActive(false);
        Debug.Log("Loading new map");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
