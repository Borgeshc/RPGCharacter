using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    public static bool isIdle;
    public static bool isMoving;
    public static bool isBlocking;
    public static bool isBlockHit;
    public static bool isBlockBreak;
    public static bool isHit;
    public static bool isDead;
    public static bool isSprinting;
    public static bool isRunning;
    public static bool isAttacking;
    public static bool isKicking;
    public static bool isArmed;
    public static bool timeToRevive;

    public static float leftStickInputX;
    public static float leftStickInputY;
    public static float rightStickInputX;
    public static float rightStickInputY;

    public static int previousWeapon;
    public static int currentWeapon;
}
