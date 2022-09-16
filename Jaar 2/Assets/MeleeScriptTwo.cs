using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeScriptTwo : MonoBehaviour
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
    public GameObject fpsCam;
    public LayerMask enemy;
    public int attackstate;
    public float attackTransition;
    public float attackTransitionMax;
    public float attackTransitionMin;
    public ParticleSystem swordhitParticle;
    public float attackStateReset;
    public float attackStateResetResetMax;
    public float attackStateResetResetMin;
    public bool isReloading;
    public GameObject blade;
    public WeaponSway weaponSway;
    public int swordBlades;
    public bool disappeared;
    // Start is called before the first frame update
    void Start()
    {

        range = 3f;
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
        weaponSway = GameObject.Find("LeftSwordHolder").GetComponent<WeaponSway>();
        swordBlades = 3;

    }

    // Update is called once per frame
    void Update()
    {
        Animator anim = GameObject.Find("RightSwordHolder").GetComponent<Animator>();
        anim.SetInteger("AttackState", attackstate);
        anim.SetBool("isReloading", isReloading);





        

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

        if (swordBlades == 0 && disappeared == false)
        {
            StartCoroutine(BladeDisappear());
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
                }
            }
            else if (Input.GetKeyUp(KeyCode.F) && canAttack == true && attackTransition == attackTransitionMin)
            {
                attackstate = 2;
                cooldown = maxCooldown;
                attackTransition = attackTransitionMax;
                chargedDamage = minChargedDamage;
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
        if (chargedDamage > 15)
        {
            CameraShaker.Instance.ShakeOnce(0.1f, 5f, 1f, 0.1f);
        }

        if (attackstate == 0)
        {

        }
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            StartCoroutine(Reload());
            isReloading = true;
        }

    }

    public IEnumerator BladeDisappear()
    {

        yield return new WaitForSeconds(0.2f);
        disappeared = true;
        blade.SetActive(false);
        
    }

    public IEnumerator Slash()
    {
        yield return new WaitForSeconds(0.2f);
        hit.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(finalDamage);
        swordBlades -= 1;
        swordhitParticle.GetComponent<ParticleSystem>().Play();

    }
    public void SlashHit()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == enemy)
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(10);
        }
    }


    public IEnumerator Reload()
    {
        weaponSway.canSway = false;
        
        blade.SetActive(false);
        yield return new WaitForSeconds(1f);
        swordBlades = 3;
        blade.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        disappeared = false;
        isReloading = false;
        weaponSway.canSway = true;
    }


}
