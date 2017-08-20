using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    WeaponManager weaponManager;
    Animator anim;

    bool attacking;
    bool hasDied;
    bool hasRevived;
    bool beingHit;
    bool changingWeapon;

    private void Start()
    {
        anim = GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        if (StateManager.timeToRevive && !hasRevived)
            Revive();

        if (StateManager.isDead && !hasDied)
            Died();
        else if(StateManager.isDead) return;

        if(StateManager.isChangingWeapon && !changingWeapon)
        {
            changingWeapon = true;
            StartCoroutine(ChangeWeapon());
        }

        print("Sheath " + anim.GetBool("Sheath"));

        WeaponChangeState();
        SetMovementState();
        Block();

        if (StateManager.isBlockHit && !beingHit)
        {
            beingHit = true;
            StartCoroutine(BlockHit());
        }

        if (StateManager.isHit && !beingHit)
        {
            beingHit = true;
            StartCoroutine(Hit());
        }

        if (StateManager.isAttacking && !attacking && StateManager.isArmed && !StateManager.isSprinting)
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

    void SetMovementState()
    {
        anim.SetBool("Sprinting", StateManager.isSprinting);
        anim.SetBool("Moving", StateManager.isMoving);
        anim.SetBool("Idle", StateManager.isIdle);
    }

    void WeaponChangeState()
    {
        anim.SetBool("ChangingWeapon", StateManager.isChangingWeapon);
    }

    IEnumerator ChangeWeapon()
    {
        anim.SetBool("Sheath", true);

        yield return new WaitForSeconds(.25f);
        anim.SetLayerWeight(StateManager.previousWeapon + 1, 0);
        anim.SetLayerWeight(StateManager.currentWeapon + 1, 1);
        anim.SetBool("Sheath", false);

        yield return new WaitForSeconds(.25f);
        StateManager.isChangingWeapon = false;
        changingWeapon = false;
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

    void Block()
    {
        anim.SetBool("Block", StateManager.isBlocking);
    }

    IEnumerator BlockHit()
    {
        int randomHit = 0;
        if(StateManager.currentWeapon != 0)
            randomHit = Random.Range(0, weaponManager.CurrentWeapon().numberOfHits);
        else
            randomHit = Random.Range(0, 2);

        anim.SetFloat("BlockHit", randomHit);
        yield return new WaitForSeconds(.5f);
        anim.SetFloat("BlockHit", 0);
        StateManager.isBlockHit = false;
        beingHit = false;
    }

    IEnumerator Hit()
    {
        int randomHit = 0;
        if (StateManager.currentWeapon != 0)
            randomHit = Random.Range(0, weaponManager.CurrentWeapon().numberOfHits);
        else
            randomHit = Random.Range(0, 5);

        anim.SetFloat("Hit", randomHit);
        yield return new WaitForSeconds(.4f);
        anim.SetFloat("Hit", 0);
        StateManager.isHit = false;
        beingHit = false;
    }

    void Died()
    {
        hasDied = true;
        anim.SetBool("Died", true);
        hasRevived = false;
    }

    public void Revive()
    {
        anim.SetBool("Died", false);
        hasDied = false;
        hasRevived = true;
    }
}
