using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    WeaponManager weaponManager;
    Animator anim;

    bool attacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        if (StateManager.isIdle && !StateManager.isAttacking && !StateManager.isKicking)
            Idle();

        if (StateManager.isMoving && !StateManager.isAttacking && !StateManager.isKicking)
            Moving();

        if (StateManager.isSprinting && !StateManager.isAttacking && !StateManager.isKicking)
            Sprinting();

        ChangeWeapon();
        
        if(StateManager.isAttacking && !attacking && StateManager.isArmed && !StateManager.isSprinting)
        {
            attacking = true;
            StartCoroutine(Attack());
        }

        if (StateManager.isKicking && !attacking && StateManager.isArmed && !StateManager.isSprinting)
        {
            attacking = true;
            StartCoroutine(Kick());
        }
    }

    void Idle()
    {
        anim.SetBool("Sprinting", false);
        anim.SetBool("Moving", false);
        anim.SetBool("Idle", true);
    }

    void Moving()
    {
        anim.SetBool("Sprinting", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Moving", true);
    }

    void Sprinting()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Moving", false);
        anim.SetBool("Sprinting", true);
    }

    void ChangeWeapon()
    {
        if(StateManager.previousWeapon == 0)
            anim.SetLayerWeight(StateManager.previousWeapon, .5f);
        else
            anim.SetLayerWeight(StateManager.previousWeapon, 0);

        anim.SetLayerWeight(StateManager.currentWeapon, 1);
    }

    IEnumerator Attack()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Moving", false);
        anim.SetBool("Sprinting", false);

        int attack = Random.Range(0, weaponManager.CurrentWeapon().numberOfAttacks);
        anim.SetFloat("Attack", attack);
        yield return new WaitForSeconds(weaponManager.CurrentWeapon().globalCooldown);
        anim.SetFloat("Attack", 0);
        attacking = false;
    }


    IEnumerator Kick()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Moving", false);

        int kick = Random.Range(0, weaponManager.CurrentWeapon().numberOfKicks);
        anim.SetFloat("Kick", kick);
        yield return new WaitForSeconds(weaponManager.CurrentWeapon().globalCooldown);
        anim.SetFloat("Kick", 0);
        attacking = false;
    }
}
