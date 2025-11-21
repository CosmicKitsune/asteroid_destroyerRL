using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Player Stats")]
    public static float moveSpeed = 10f; //max power applied
    public static float rotSpeed = 32f; //power applied
    public static float dashSpeed = 12f;
    public static float dashDuration = 1.0f;
    public static float dashCooldown = 1.0f;
    public static float shootCooldown = 0.8f;
}
