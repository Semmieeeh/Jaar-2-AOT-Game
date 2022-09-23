using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    public GameObject [] turrets;

    public float health;

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

    public bool invisible;
    private bool invisibleStart;

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

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        health = 1000f;
        turrets[1].SetActive(false);
        turrets[2].SetActive(false);
        damageTurret = 5f;
        magazineSize = 5;
        gunShot.volume = 0.75f;
        range = 15f;

        delay = 0.5f;

        invisibleStart = true;
}
    public void TakeDamageTurret(int dmg)
    {
        health -= dmg;

        if(health < 0)
        {
            health = 0;
        }

        if(health <= 500)
        {
            turrets[0].SetActive(false);
            turrets[1].SetActive(true);
            damageTurret = 3f;
        }

        if(health <= 100)
        {
            turrets[2].SetActive(true);
            turrets[1].SetActive(false);
            damageTurret = 1f;
        }

        if (health <= 0)
        {
            Destroy(turret);
            damageTurret = 0f;
        }
    }

    public void Update()
    {
        MyInput();
    }
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void MyInput()
    {
            transform.LookAt(player);
            Vector3 angles = transform.localEulerAngles;
            angles.x = 0;
            transform.localEulerAngles = angles;

        if (Physics.Raycast(attackPoint.position, transform.forward, out rayHit, range))
        {
            if (rayHit.collider.CompareTag("Player"))
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

            gunShot.Play();
            muzzle.Play();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Raycast
        PlayerHealth playerHealth = GameObject.Find("player").GetComponent<PlayerHealth>();

        if (Physics.Raycast(attackPoint.position, transform.forward, out rayHit, range))
        {
            if (rayHit.collider.CompareTag("Player"))
            {
                playerHealth.AddjustCurrentHealth(damageTurret);
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
}
