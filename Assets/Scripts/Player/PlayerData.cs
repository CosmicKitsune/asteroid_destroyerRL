using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Movement Stats")]
    public static float moveSpeed = 10f; //max power applied
    public static float rotSpeed = 32f; //power applied

    [Header("Dash Stats")]
    public static float dashSpeed = 12f;
    public static float dashDuration = 1.0f;
    public static float dashCooldown = 1.0f;
    public static int dashCount;

    [Header("Shooting Stats")]
    public static float shootCooldown = 0.8f;
    public static float damage;
    public static float bulSize;
    public static float bulSpeed;

    [Header("Health Stats")]
    public static float shieldHp;
    public static float shipHp;
}
