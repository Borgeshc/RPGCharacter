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

    public WeaponType weaponType;

    public GameObject weaponModel;
    public GameObject weaponLocation;

    public int numberOfAttacks;
    public int numberOfKicks;
    public int numberOfBlockHits;
    public int numberOfHits;

    public float globalCooldown;

    public virtual void UnSheathWeapon()
    {
        weaponModel.transform.position = weaponLocation.transform.position;
    }
}
