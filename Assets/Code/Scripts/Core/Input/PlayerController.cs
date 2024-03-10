using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour  {
    
    [Tooltip("The PlayerCharacter associated with the player GameObject.")]
    public PlayerCharacter Player { get; private set; }
    
    // ----------------- Input -------------------
    private PlayerInputActions _playerInputActions;
    
    // ------------- Unity Events ----------------
    public UnityEvent OnMovementInput;
    
    private void Awake() {
        this.Player = this.GetComponent<PlayerCharacter>();
            
        this._playerInputActions = new PlayerInputActions();
        this._playerInputActions.Enable();
    }
    
    private void Start() {
        this.EnablePlayerControls();
    }
    
    private void DisablePlayerControls() {
        this._playerInputActions.Player.Disable();

        this._playerInputActions.Player.Movement.performed -= this.MovePlayer;
    }
    
    private void EnablePlayerControls() {
        this._playerInputActions.Player.Enable();
            
        this._playerInputActions.Player.Movement.performed += this.MovePlayer;
    }
    
    private void MovePlayer(InputAction.CallbackContext obj) {
        this.OnMovementInput?.Invoke();
        this.Player.InvokeMovement(this._playerInputActions.Player.Movement.ReadValue<Vector2>());
    }
}
