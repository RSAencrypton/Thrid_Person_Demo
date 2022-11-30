using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "input handle")]
public class inputHandle : ScriptableObject, PlayerInput.IGameplayActions
{
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };
    PlayerInput _inputHandle;

    private void OnEnable()
    {
        _inputHandle = new PlayerInput();

        _inputHandle.Gameplay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        disableInput();
    }

    public void disableInput() {
        _inputHandle.Gameplay.Disable();
    }

    public void enableGameplayInput() {
        _inputHandle.Gameplay.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            onMove.Invoke(context.ReadValue<Vector2>());

        if (context.phase == InputActionPhase.Canceled)
            onStopMove.Invoke();
    }
}
