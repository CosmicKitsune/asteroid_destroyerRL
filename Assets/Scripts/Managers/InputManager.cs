using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private PlayerInput playerInput;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();
        //attackAction = playerInput.actions["Attack"];
    }

    private void Update()
    {
        //AttackInput = attackAction.WasPressedThisFrame();
    }
}
