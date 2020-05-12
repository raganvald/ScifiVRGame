using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    [Range(0,1)]
    public float smoothing = 1f;
    private Animator animator;
    private Vector3 previousPos;
    private VRIKRig vrRig;


    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<VRIKRig>();
        previousPos = vrRig.head.vrTarget.position;
    }

    void Update()
    {

        Vector3 headsetSpead = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpead.y = 0;

        //Local speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpead);
        previousPos = vrRig.head.vrTarget.position;


        //Set Animator Values
        float previousDirectionX = animator.GetFloat("DirectionX");
        float previousDirectionY = animator.GetFloat("DirectionY");

        animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
        animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));

    }
}
