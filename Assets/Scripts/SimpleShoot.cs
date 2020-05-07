using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotType
{
    PysicalProjectile,
    RaycastProjectile
}

public class SimpleShoot : MonoBehaviour
{
    //General Variables
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public List<GameObject> particleFX = new List<GameObject>();
    public Transform barrelLocation;
    public Transform casingExitLocation;
    public AudioSource audioSource;
    public ShotType shotType;


    //RayCast Variables
    public LayerMask _hitLayer;
    public float _maxBulletDistance;
    public GameObject aimingLaser;
    public float decreaseSpeed = 2;
    public float shotPower = 100f;

    public GameObject impactEffect;


    //Private Variables
    private bool aimingLaserState = true;
    RaycastHit _hitInfo;
    

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;
    }


    public void TriggerShoot()
    {
        GetComponent<Animator>().SetTrigger("Fire");
    }

    void Shoot()
    {
        //  GameObject bullet;
        //  bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        // bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

       
        if (shotType == ShotType.PysicalProjectile)
        {
            PhysicalProjectileShoot();
        }

        if(shotType == ShotType.RaycastProjectile)
        {
            RayCastProjectileShoot();
        }


        foreach (GameObject pfx in particleFX)
        {
            pfx.GetComponent<ParticleSystem>().Play();
        }



        if (audioSource != null)
        {
            audioSource.Play();
        }
        
       //  Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation).GetComponent<Rigidbody>().AddForce(casingExitLocation.right * 100f);

    }

    void PhysicalProjectileShoot()
    {
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        
    }

    void RayCastProjectileShoot()
    {
        Vector3 finalPosition = new Vector3();
        if (Physics.Raycast(barrelLocation.position, barrelLocation.forward, out _hitInfo, _maxBulletDistance, _hitLayer))
        {
            finalPosition = _hitInfo.transform.position;
            Debug.Log(_hitInfo.transform.name);
            var hittedObject = _hitInfo.collider.gameObject;
            Debug.Log(hittedObject.transform.name);

            if (hittedObject != null)
            {
                EnemyHealth enemy = hittedObject.transform.root.GetComponent<EnemyHealth>();

                if (enemy != null)
                {
                    //Hit enemy
                    enemy.Damage(4);
                }
                //Create a vfx for the bullet impact
                GameObject go = Instantiate(impactEffect, _hitInfo.point, Quaternion.LookRotation(_hitInfo.normal));
                Destroy(go, 1);
            }
        }
        else
        {
            finalPosition = barrelLocation.position + barrelLocation.forward * _maxBulletDistance;
        }
    }

    void CasingRelease()
    {
        if (casingPrefab != null)
        {
            GameObject casing;
            casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
            casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
            casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
            Destroy(casing, 1);
        }
    }


}
