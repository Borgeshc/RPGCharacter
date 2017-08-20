using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public Image healthBar;
    public AudioClip[] hurtSounds;
    public GameObject damageBorder;

    WeaponManager weaponManager;
    AudioSource source;
    Animator anim;
    EnemyAI enemyAI;
    bool isBeingHit;
    bool hasRevived;

    float health;

    void Start()
    {
        health = maxHealth;
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        weaponManager = GetComponent<WeaponManager>();
        UpdateHealthBar();
    }

    public void TookDamage(float damage, int critChance)
    {
        if (StateManager.timeToRevive && !hasRevived)
            Revive();

        if (StateManager.isDead) return;

        if (!StateManager.isBlocking)
        {
            StateManager.isHit = true;

            if (CritChance(critChance))
            {
                health -= damage * 2;
            }
            else
            {
                health -= damage;
            }
        }
        else
            StateManager.isBlockHit = true;

        if(!isBeingHit)
        {
            isBeingHit = true;
            StartCoroutine(Hit());
        }

        UpdateHealthBar();

        if (health <= 0)
        {
            Died();
        }
    }

    bool CritChance(int critChance)
    {
        int critRoll = Random.Range(0, 100);
        if (critRoll <= critChance)
            return true;
        else
            return false;
    }

    IEnumerator Hit()
    {
      if(hurtSounds.Length > 0)
        source.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)]);
        damageBorder.SetActive(true);
        yield return new WaitForSeconds(.15f);
        damageBorder.SetActive(false);
        isBeingHit = false;
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    void Died()
    {
        StateManager.isDead = true;
        hasRevived = false;
    }

    void Revive()
    {
        StateManager.isDead = false;
        hasRevived = true;
    }

    public void DeathSound(AudioClip deathSound)
    {
        source.PlayOneShot(deathSound);
    }
}
