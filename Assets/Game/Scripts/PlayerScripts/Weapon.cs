using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        TwoHandAxe,
        TwoHandBow,
        TwoHandClub,
        TwoHandCrossbow,
        TwoHandSpear,
        TwoHandSword,
        Dagger,
        FistWeapon,
        Mace,
        Pistol,
        Rifle,
        Shield,
        Spear,
        Spell,
        Staff,
        Sword
    };

    [Header("Weapon Set Up")]
    public WeaponType weaponType;

    public GameObject weaponModel;

    public int numberOfAttacks;
    public int numberOfKicks;
    public int numberOfBlockHits;
    public int numberOfHits;

    [Space, Header("Weapon Stats")]
    public float minDamage;
    public float maxDamage;
    public float globalCooldown;

    [Space, Header("For Projectile Weapons")]
    public LayerMask layerMask;
    public GameObject projectile;
    public GameObject spawnpoint;

    float damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy") && StateManager.isAttacking)
        {
            damage = Random.Range(minDamage, maxDamage);
            other.GetComponent<EnemyHealth>().TookDamage((int)damage);
        }
    }

    public void SpawnProjectile()
    {
        RaycastHit hit;
        Physics.BoxCast(transform.position + transform.localScale * .5f, Vector3.one * 2, transform.forward, out hit, Quaternion.identity, Mathf.Infinity, layerMask);

        if(hit.transform != null && hit.transform.tag.Equals("Enemy"))
        {
            damage = Random.Range(minDamage, maxDamage);
            GameObject myProjectile = Instantiate(projectile, spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
            myProjectile.GetComponent<Projectile>().SetVariables(damage, hit.transform.position);
        }
    }
}
