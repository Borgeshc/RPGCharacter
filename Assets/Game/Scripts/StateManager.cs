using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    public static bool isIdle;
    public static bool isMoving;
    public static bool isCrouching;
    public static bool isSprinting;
    public static bool isRunning;
    public static bool isAttacking;
    public static bool isKicking;
    public static bool isArmed;

    public static float leftStickInputX;
    public static float leftStickInputY;
    public static float rightStickInputX;
    public static float rightStickInputY;

    public static int previousWeapon;
    public static int currentWeapon;
}
