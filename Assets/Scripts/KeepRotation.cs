using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour
{
    public GameObject trackedObj;
    Vector3 offset;
    private void Awake()
    {
        offset = new Vector3(0, -1.1f, -0.1f);
    }
    void LateUpdate()
    {

        transform.position = trackedObj.transform.position + offset;               
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, trackedObj.transform.eulerAngles.y, transform.eulerAngles.z);

    }
}
