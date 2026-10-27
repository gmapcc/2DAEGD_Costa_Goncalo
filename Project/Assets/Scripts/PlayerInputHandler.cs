using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;




public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls _controls;
    public Vector2 InputVector {get; private set;}

    public float pushValue;
    public float scarecrowVal;
    public bool canTP;
    private bool wasTPing;

    public float Restart;
    public float FullReset;
    private bool isPushing = false;
    private bool wasPushing = false;
    public bool canPush = true;

    private void Awake()=> _controls = new PlayerControls();
    private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();

    public void Update()
    {
        InputVector = _controls.Movement.Move.ReadValue<Vector2>();
        pushValue = _controls.Movement.Push.ReadValue<float>();
        scarecrowVal = _controls.Movement.ScareCrow.ReadValue<float>();
        Restart = _controls.Movement.Restart.ReadValue<float>();
        FullReset = _controls.Movement.FullReset.ReadValue<float>();

        CheckForGameManagement();
        
        canPush = checkForPush(pushValue, ref wasPushing);
        canTP = checkForPush(scarecrowVal, ref wasTPing);
       
    }

    private void CheckForGameManagement()
    {
        if (Restart > 0.01f)
        {
            GameStates.instance.StateMachine(GameState.Restart);
        }
        else if(FullReset > 0.01f)
        {
            GameStates.instance.StateMachine(GameState.FullReset);
        }
    }

    bool checkForPush(float val, ref bool lastState)
    {
        bool currentState;
        bool justPressed;
        if(val > 0.0f) currentState = true;
        else currentState = false;
        
        if(currentState && !lastState)
        {
            justPressed = true;
        }
        else justPressed = false;
        lastState = currentState;
        return justPressed;
       
    }
}
