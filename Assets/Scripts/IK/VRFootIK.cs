using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{
    [Range(0, 1)]
    public float rightFootPoseWeight = 1;
    [Range(0, 1)]
    public float leftFootPoseWeight = 1;

    [Range(0, 1)]
    public float rightFootRotWeight = 1;
    [Range(0, 1)]
    public float leftFootRotWeight = 1;

    public Vector3 footOffset;
    public LayerMask GroundLayerMask;

    private int maxPlayerHeight = 2;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
        RaycastHit hit;

        //Handle the Right foot
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        bool hasHit = Physics.Raycast(rightFootPos + (Vector3.up * maxPlayerHeight), Vector3.down, out hit, 3, GroundLayerMask);
        if (hasHit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPoseWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset);

            //Fix rotation of foot with offset
            Quaternion rightFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }

        //Handle the Left foot
        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        hasHit = Physics.Raycast(leftFootPos + (Vector3.up * maxPlayerHeight), Vector3.down, out hit, 3, GroundLayerMask);
        if (hasHit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPoseWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footOffset);
            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            
            //Fix rotation of foot with offset
            Quaternion leftFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }

    }
}
