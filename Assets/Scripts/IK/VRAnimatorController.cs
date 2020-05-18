using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    [Range(0,1)]
    public float smoothing = 0.1f;
    private Animator animator;
    private Vector3 previousPos;
    private VRIKRig vrRig;
    private float previousDirectionX = 0;
    private float previousDirectionZ = 0;



    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<VRIKRig>();
        previousPos = vrRig.head.vrTarget.position;
        animator.SetFloat("DirectionX", 0);
        animator.SetFloat("DirectionZ", 0);
    }

    void Update()
    {

        Vector3 headsetSpead = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpead.y = 0;

        //Local speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpead);
        previousPos = vrRig.head.vrTarget.position;

        //Calculate and store the x and y directions of movment
        //animator.SetFloat("DirectionX", headsetLocalSpeed.x);
        //animator.SetFloat("DirectionY", headsetLocalSpeed.z);
        //previousDirectionX = headsetLocalSpeed.x;
        //previousDirectionZ = headsetLocalSpeed.z;

        
        float xdir = Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing);
        float zdir = Mathf.Lerp(previousDirectionZ, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing);

        if (float.IsNaN(xdir) || float.IsNaN(zdir))
        {
            return;
        }
        animator.SetFloat("DirectionX", xdir);
        animator.SetFloat("DirectionZ", zdir);
        previousDirectionX = xdir;
        previousDirectionZ = zdir;
        

    }
}
