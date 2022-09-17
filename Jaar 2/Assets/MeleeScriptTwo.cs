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
    public ParticleSystem swordhitParticle;

    public GameObject fpsCam;
    public LayerMask enemy;
    public int attackstate;
    public float attackTransition;
    public float attackTransitionMax;
    public float attackTransitionMin;

    public float attackStateReset;
    public float attackStateResetResetMax;
    public float attackStateResetResetMin;
    public bool isReloading;
    public GameObject blade;
    public WeaponSway weaponSway;
    public int swordBlades;
    public bool disappeared;
    public LayerMask swordClink;
    public ParticleSystem swordGroundhitParticle;
    public bool reloadingWithSwords;
    // Start is called before the first frame update
    void Start()
    {
        swordBlades = 3;
        reloadingWithSwords = false;
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


    }


    // Update is called once per frame
    void Update()
    {
        Animator anim = GameObject.Find("LeftSwordHolder").GetComponent<Animator>();
        anim.SetInteger("AttackState", attackstate);
        anim.SetBool("isReloading", isReloading);
        anim.SetBool("ReloadWithSwords", reloadingWithSwords);


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
                }
            }
            else if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    attackstate = 2;
                    cooldown = maxCooldown;
                    attackTransition = attackTransitionMax;
                    StartCoroutine(SlashHit());
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


        if (attackstate == 0)
        {

        }
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false && swordBlades == 0)
        {
            StartCoroutine(Reload());
            isReloading = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) &&reloadingWithSwords == false &&swordBlades >0)
        {
            StartCoroutine(ReloadWithSwords());
            reloadingWithSwords = true;
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
        yield return new WaitForSeconds(1f);
        blade.SetActive(false);
        yield return new WaitForSeconds(1f);
        blade.SetActive(true);
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
        yield return new WaitForSeconds(1f);

        blade.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        swordBlades = 3;
        disappeared = false;
        canAttack = true;
        isReloading = false;
        weaponSway.canSway = true;
    }


}
