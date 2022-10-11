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
    public bool readyToShoot;

    //Reference
    public Transform attackPoint;
    public RaycastHit rayHit;
    public RaycastHit hit;

    //Graphics
    public GameObject platform;
    public AudioSource gunShot;
    public AudioSource gunReload;
    public ParticleSystem muzzle;
    public EnemyHealth enemyHp;
    public LayerMask titanMask;
    public Transform titan;
    public MeleeScript ml;
    public bool destroyed;

    // Start is called before the first frame update
    void Start()
    {
        //gunShot.volume = 0.75f; 
        ml = GameObject.Find("handheld ODM gear_fixed_ow 2").GetComponent<MeleeScript>();
        readyToShoot = true;
        platform = ml.place;
        range = 40;


    }
    public void Update()
    {
       if(destroyed == true)
       {
            platform.GetComponent<PlaceTurret>().cannon = null;
            Destroy(gameObject);
       }

        if(titan != null)
        {
            MyInput();

        }

        else if(titan == null)
        {
            gameObject.GetComponent<SphereCollider>().radius += 75 * Time.deltaTime;
        }
        if(gameObject.GetComponent<SphereCollider>().radius > range)
        {
            gameObject.GetComponent<SphereCollider>().radius = 0;
        }
        
        





    }
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void MyInput()
    {
        //Shoot
        if(titan != null)
        {
            transform.LookAt(titan);
            titan.GetComponent<EnemyHealth>().gettingShotBy = gameObject;
            Vector3 angles = transform.localEulerAngles;
            angles.x = 0;
            transform.localEulerAngles = angles;

            


            if (bulletsLeft == 0)
            {
                Reload();
            }
            if (readyToShoot && Vector3.Distance(gameObject.transform.position, titan.transform.position) < range)
            {                             
                readyToShoot=false;
                Invoke("Shoot", delay);       
                
            }
            
        }
    }
    public void LateUpdate()
    {
        if(titan != null)
        {
            if (Vector3.Distance(gameObject.transform.position, titan.transform.position) > range)
            {
                titan = null;
                
            }
        }
    }
    private void Shoot()
    {

        if (titan != null)
        {
            EnemyHealth hp = titan.GetComponent<EnemyHealth>();
            hp.TakeDamage(damageTurret);
            readyToShoot = true;
            
            
        }

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
        if (other.gameObject.tag == "Titan" && titan == null)
        {
            titan = other.gameObject.transform;
            titan.GetComponent<EnemyHealth>().gettingShotBy = gameObject;
            MyInput();
            readyToShoot = true;
        }
    }
}
