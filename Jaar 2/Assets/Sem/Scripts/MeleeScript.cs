using EZCameraShake;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MeleeScript : MonoBehaviour
{
    public float damage;
    public float finalDamage;
    public float chargedDamage;
    public float maxChargedDamage;
    public float minChargedDamage;
    public float range;
    public bool canAttack;
    public float cooldown;
    public float minCooldown;
    public float maxCooldown;
    RaycastHit hit;
    public ParticleSystem swordhitParticle;
    public CameraShaker camShake;
    public GameObject fpsCam;
    public LayerMask enemy;
    public int attackstate;
    
    public float attackTransition;
    public float attackTransitionMax;
    public float attackTransitionMin;
    public bool isShaking;
    public float attackStateReset;
    public float attackStateResetResetMax;
    public float attackStateResetResetMin;
    public bool isReloading;
    public GameObject blade,blade2;
    public WeaponSway weaponSway;
    public int swordBlades;
    public bool disappeared;
    public LayerMask swordClink;
    public ParticleSystem swordGroundhitParticle;
    public bool reloadingWithSwords;
    public float duration, intensity;
    public Quaternion SpawnRot;
    public Economy economy;
    public GameObject sellUi;
    public GameObject[] cannons;
    public GameObject place;
    public GameObject turret, buyUi;
    
    // Start is called before the first frame update
    void Start()
    {
        isShaking = false;
        swordBlades = 3;
        reloadingWithSwords = false;
        range = 4.5f;
        damage = 10;
        maxCooldown = 1;
        minCooldown = 0;
        attackstate = 0;
        attackStateResetResetMax = 1;
        attackStateResetResetMin = 0;
        maxChargedDamage = 30f;
        minChargedDamage = 0f;
        attackTransitionMax = 2;
        attackTransitionMin = 0f;
        weaponSway = GameObject.Find("RightSwordHolder").GetComponent<WeaponSway>();
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShaker>();
        economy = GameObject.Find("Player").GetComponent<Economy>();

    }


    // Update is called once per frame
    void Update()
    {
        Animator anim = GameObject.Find("RightSwordHolder").GetComponent<Animator>();
        Animator anim2 = GameObject.Find("LeftSwordHolder").GetComponent<Animator>();



        anim.SetInteger("AttackState", attackstate);
        anim.SetBool("isReloading", isReloading);
        anim.SetBool("ReloadWithSwords", reloadingWithSwords);


        anim2.SetInteger("AttackState", attackstate);
        anim2.SetBool("isReloading", isReloading);
        anim2.SetBool("ReloadWithSwords", reloadingWithSwords);

        cannons = GameObject.FindGameObjectsWithTag("Cannon");










        


        if (swordBlades == 0 && disappeared == false)
        {
            StartCoroutine(BladeDisappear());
        }

        cooldown -= 1 * Time.deltaTime;

        if (cooldown < minCooldown)
        {
            cooldown = minCooldown;
        }

        if (cooldown > maxCooldown)
        {
            cooldown = maxCooldown;
        }
        if (cooldown == minCooldown)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        attackStateReset -= 1 * Time.deltaTime;
        if (attackStateReset > attackStateResetResetMax)
        {
            attackStateReset = attackStateResetResetMax;

        }
        if (attackStateReset < attackStateResetResetMin)
        {
            attackStateReset = attackStateResetResetMin;
        }


        if (chargedDamage > maxChargedDamage)
        {
            chargedDamage = maxChargedDamage;
        }




        if (Input.GetKey(KeyCode.F))
        {
            chargedDamage += 15 * Time.deltaTime;
            attackstate = 1;

        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            attackstate = 2;
        }





        if (cooldown == minCooldown)
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, enemy) && attackTransition == attackTransitionMin)
            {
                if (Input.GetKeyUp(KeyCode.F) && canAttack == true && swordBlades > 0)
                {
                    attackstate = 2;
                    finalDamage = damage + chargedDamage;
                    StartCoroutine(Slash());
                    chargedDamage = minChargedDamage;
                    attackTransition = attackTransitionMax;
                    cooldown = maxCooldown;


                    if(finalDamage > 20)
                    {
                        FindObjectOfType<AudioManager>().PlayAudio(2, 0.8f, 1.2f);
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().PlayAudio(1,0.8f,1.2f);
                    }
                }
            }
            else if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Economy eco = GameObject.Find("Player").GetComponent<Economy>();
                if (hit.transform.gameObject.tag == "Smith")
                {
                    
                    //ui setactive
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Cursor.lockState = CursorLockMode.None;
                        hit.transform.gameObject.GetComponent<BlackSmith>().isBuying = true;
                        buyUi.SetActive(true);
                    }
                }
                if(hit.transform.gameObject.tag == "Platform" && eco.turrets>=1)
                {
                    
                    place = hit.transform.gameObject;
                    Vector3 pos = hit.transform.gameObject.transform.position;
                    pos.y -= 1;                   
                    if (Input.GetKeyDown(KeyCode.E)&& place.GetComponent<PlaceTurret>().obstructed == false && economy.metal >=economy.turretCost)
                    {
                        Instantiate(turret,pos,Quaternion.identity);

                        eco.turrets--;
                        place.GetComponent<PlaceTurret>().cannon = turret;
                        
                    }
                }

                if(hit.transform.gameObject.tag == "Cannon")
                {
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        hit.transform.gameObject.GetComponentInParent<Turrets>().destroyed = true;
                        economy.metal += 10;
                        economy.turrets += 1;
                        
                    }
                }

                

                if (Input.GetKeyUp(KeyCode.F))
                {
                    attackstate = 2;
                    cooldown = maxCooldown;
                    attackTransition = attackTransitionMax;
                    StartCoroutine(SlashHit());
                    if(swordBlades > 0)
                    {
                        FindObjectOfType<AudioManager>().PlayAudio(6, 0.8f, 1.2f);
                    }
                    chargedDamage = minChargedDamage;
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                attackstate = 2;
                cooldown = maxCooldown;
                attackTransition = attackTransitionMax;
                chargedDamage = minChargedDamage;
            }
        }

        





        attackTransition -= 50 * Time.deltaTime;
        if (attackTransition < attackTransitionMin)
        {
            attackTransition = attackTransitionMin;
        }
        else if (attackTransition > attackTransitionMax)
        {
            attackTransition = attackTransitionMax;
        }

        if (attackstate == 2)
        {

            if (attackTransition == attackTransitionMin)
            {
                attackstate = 0;


            }

        }


        
        if(cooldown == minCooldown)
        {
            if (Input.GetKeyDown(KeyCode.R) && isReloading == false && swordBlades == 0)
            {
                StartCoroutine(Reload());
                isReloading = true;
            }
            else if (Input.GetKeyDown(KeyCode.R) && reloadingWithSwords == false && swordBlades > 0)
            {
                StartCoroutine(ReloadWithSwords());
                reloadingWithSwords = true;
            }
        }




        
    }

    public IEnumerator BladeDisappear()
    {
        yield return new WaitForSeconds(0.2f);
        disappeared = true;
        blade.SetActive(false);
        blade2.SetActive(false);

    }

    public IEnumerator Slash()
    {
        yield return new WaitForSeconds(0.2f);
        hit.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(finalDamage);
        swordBlades -= 1;
        Instantiate(swordhitParticle, hit.point, Quaternion.identity);

    }
    public IEnumerator SlashHit()
    {
        yield return new WaitForSeconds(0.2f);
        if (swordBlades > 0)
        {
            Instantiate(swordGroundhitParticle, hit.point, Quaternion.identity);
        }
    }

    public IEnumerator ReloadWithSwords()
    {
        weaponSway.canSway = false;
        canAttack = false;
        yield return new WaitForSeconds(0.8f);
        blade.SetActive(false);
        blade2.SetActive(false);
        yield return new WaitForSeconds(1f);
        blade.SetActive(true);
        blade2.SetActive(true);
        disappeared = false;
        yield return new WaitForSeconds(1.5f);
        swordBlades = 3;
        canAttack = true;
        reloadingWithSwords = false;
        weaponSway.canSway = true;
    }


    public IEnumerator Reload()
    {
        weaponSway.canSway = false;
        canAttack = false;
        blade.SetActive(false);
        blade2.SetActive(false);
        yield return new WaitForSeconds(1f);

        blade.SetActive(true);
        blade2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        swordBlades = 3;
        disappeared = false;
        canAttack = true;
        isReloading = false;
        weaponSway.canSway = true;
    }


}
