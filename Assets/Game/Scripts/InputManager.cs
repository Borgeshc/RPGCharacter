using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class InputManager : MonoBehaviour
{
    InputDevice inputDevice;
    WeaponManager weaponManager;
    
    int currentWeapon; 

    private void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        inputDevice = InControl.InputManager.ActiveDevice;
        SetMovementValues();

        if (inputDevice.LeftStick != Vector3.zero && !StateManager.isBlocking && !StateManager.isSprinting && !StateManager.isAttacking && !StateManager.isKicking)
            StateManager.isMoving = true;
        else
            StateManager.isMoving = false;

        if (inputDevice.LeftStick == Vector3.zero && !StateManager.isAttacking && !StateManager.isKicking && !StateManager.isSprinting)
            StateManager.isIdle = true;
        else
            StateManager.isIdle = false;

        if (inputDevice.LeftStick != Vector3.zero && inputDevice.LeftStickButton.IsPressed && !StateManager.isBlocking && !StateManager.isAttacking && !StateManager.isKicking)
            StateManager.isSprinting = true;
        else
            StateManager.isSprinting = false;

        if (inputDevice.RightBumper.WasPressed && !StateManager.isAttacking && !StateManager.isBlocking)
        {
            StateManager.previousWeapon = currentWeapon;

            if (currentWeapon + 1 < weaponManager.amountOfWeapons + 1)
                currentWeapon++;
            else
                currentWeapon = 0;

            StateManager.currentWeapon = currentWeapon;
            weaponManager.ChangeWeapon();
        }

        if (inputDevice.LeftBumper.WasPressed && !StateManager.isAttacking && !StateManager.isBlocking)
        {
            StateManager.previousWeapon = currentWeapon;

            if (currentWeapon - 1 >= 0)
                currentWeapon--;
            else
                currentWeapon = weaponManager.amountOfWeapons;

            StateManager.currentWeapon = currentWeapon;
            weaponManager.ChangeWeapon();
        }

        if (currentWeapon == 0)
            StateManager.isArmed = false;
        else
            StateManager.isArmed = true;

        if(inputDevice.Action3.IsPressed && !StateManager.isAttacking && !StateManager.isKicking && !StateManager.isBlocking)
        {
            StateManager.isAttacking = true;
            StartCoroutine(GlobalCooldown());
        }

        if (inputDevice.Action4.IsPressed && !StateManager.isAttacking && !StateManager.isKicking && !StateManager.isBlocking)
        {
            StateManager.isKicking = true;
            StartCoroutine(GlobalCooldown());
        }

        if (inputDevice.Action2.IsPressed && StateManager.isIdle && !StateManager.isAttacking && !StateManager.isKicking)
            StateManager.isBlocking = true;
        else
            StateManager.isBlocking = false;
    }

    void SetMovementValues()
    {
        StateManager.leftStickInputX = inputDevice.LeftStickX;
        StateManager.leftStickInputY = inputDevice.LeftStickY;
        StateManager.rightStickInputX = inputDevice.RightStickX;
        StateManager.rightStickInputY = inputDevice.RightStickY;
    }

    IEnumerator GlobalCooldown()
    {
        if(StateManager.currentWeapon != 0)
        yield return new WaitForSeconds(weaponManager.CurrentWeapon().globalCooldown);

        StateManager.isAttacking = false;
        StateManager.isKicking = false;
    }
}
