using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;




public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls _controls;
    public Vector2 InputVector {get; private set;}

    private void Awake()=> _controls = new PlayerControls();
    private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();

    public void Update()
    {
        InputVector = _controls.Movement.Move.ReadValue<Vector2>();
    }


}
