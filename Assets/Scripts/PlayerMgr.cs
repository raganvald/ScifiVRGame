using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour{

    #region Singleton

    public static PlayerMgr instance;

    void Awake ()
    {
        instance = this;
    }

    #endregion


    public GameObject player;

}
