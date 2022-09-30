using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    //Gun stats
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    int bulletsLeft, bulletsShot;
    public float damageTurret;
    public float delay;

    //bools 
    public bool shooting;
    bool reloading;
    bool readyToShoot;

    //Reference
    public Transform attackPoint;
    public RaycastHit rayHit;

    //Graphics
    public GameObject muzzleFlash;

    public GameObject playerObject;
    public GameObject turret;
    public AudioSource gunShot;
    public AudioSource gunReload;
    public ParticleSystem muzzle;

    public Transform titan;

    // Start is called before the first frame update
    void Start()
    {
        //gunShot.volume = 0.75f; 
    }
    public void Update()
    {
       
        if(titan != null)
        {
            MyInput();
        }
    }
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void MyInput()
    {
        transform.LookAt(titan);
        Vector3 angles = transform.localEulerAngles;
        angles.x = 0;
        transform.localEulerAngles = angles;

        if (Physics.Raycast(attackPoint.position, transform.forward, out rayHit, range))
        {
            if (rayHit.collider.CompareTag("Titan"))
            {
                shooting = true;
            }

            else
            {
                shooting = false;
            }

        }

        if (bulletsLeft == 0)
        {
            Reload();
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            readyToShoot = false;
            bulletsShot = bulletsPerTap;

            Invoke("Shoot", delay);

            //gunShot.Play();
            //uzzle.Play();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Raycast
        if (Physics.Raycast(attackPoint.position, transform.forward, out rayHit, range))
        {
            if (rayHit.transform.gameObject.tag == "Titan")
            {
                Healthbarscript hp = rayHit.transform.gameObject.GetComponent<Healthbarscript>();
                hp.TitanDamage(damageTurret);
            }
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }
    private void ReadyToShoot()
    {
        readyToShoot = false;
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Titan")
        {
            titan = other.gameObject.transform;
            MyInput();
        }
    }
}
