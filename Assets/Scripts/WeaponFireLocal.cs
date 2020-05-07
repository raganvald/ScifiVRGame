using System.Collections;
//using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

    
    public class WeaponFireLocal : MonoBehaviour
    {
        [SerializeField]
        Transform _tip;

        [SerializeField]
        LayerMask _hitLayer;

        [SerializeField]
        float _maxBulletDistance;

        [SerializeField]
        private GameObject aimingLaser;

        RaycastHit _hitInfo;

        [SerializeField]
        float decreaseSpeed = 2;

        [SerializeField]
        GameObject impactEffect;

        private bool aimingLaserState = true;


    public void ShootAttempt()
    {
        Vector3 finalPosition = new Vector3();
        //Check if we've hit the target
        if (Physics.Raycast(_tip.position, _tip.forward, out _hitInfo, _maxBulletDistance, _hitLayer))
        {
            finalPosition = _hitInfo.transform.position;
            Debug.Log(_hitInfo.transform.name);
            var hittedObject = _hitInfo.collider.gameObject;
            Debug.Log(hittedObject.transform.name);
            if (hittedObject != null)
            {
                EnemyHealth enemy = hittedObject.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    //Hit enemy
                    enemy.Damage(4);
                }
                Instantiate(impactEffect, _hitInfo.point, Quaternion.LookRotation(_hitInfo.normal));

            }
            else
            {
                finalPosition = _tip.position + _tip.forward * _maxBulletDistance;
            }
        }

    }
    }