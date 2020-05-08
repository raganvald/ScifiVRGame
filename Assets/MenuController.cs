using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject restartMenuCanvas;
    void Start()
    {
        restartMenuCanvas = transform.Find("RestartMenuCanvas").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
            restartMenuCanvas.SetActive(!restartMenuCanvas.activeSelf);
    }
}
