using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] myWeapons;

    [HideInInspector]
    public int amountOfWeapons;

    private void Start()
    {
        amountOfWeapons = myWeapons.Length;
    }

    public void ChangeWeapon()
    {
        if(StateManager.previousWeapon != 0)
            myWeapons[StateManager.previousWeapon - 1].gameObject.SetActive(false);

        if(StateManager.currentWeapon != 0)
            myWeapons[StateManager.currentWeapon - 1].gameObject.SetActive(true);
    }

    public Weapon CurrentWeapon()
    {
        if (StateManager.currentWeapon != 0)
            return myWeapons[StateManager.currentWeapon - 1];
        else return null;
    }
}
