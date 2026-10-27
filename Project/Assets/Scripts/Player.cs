
using UnityEngine;

public class Player : MonoBehaviour
{
    //stats
    [SerializeField]
    protected float speed = 5f;
    [SerializeField]
    protected float rotationSpeed = 10f;

    //sub-objects
    private Rigidbody rb;
    private PlayerInputHandler _inputHandler;
    private CameraControls _cameraControls;
    private Vector3 finalDirectionInput;
    private GameObject lockArea;

    void Start()
    {

        //gets all the components that are present in the player's object

        rb = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        _cameraControls = GetComponent<CameraControls>();
        

        //Stops the player's rigidbody from "bouncing" when moving fast by not allowing it to rotate in any of the axis

        if(rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }

   

    void Update()
    {   

        //Gets the player input from the input handler
        Vector2 realInput = _inputHandler.InputVector;

        //Gets the current active camera
        Camera activeCam = _cameraControls.activeCamera;

        if(activeCam == null) return;

        //Gets the direction axis based on the camera that is currently active
        Vector3 camForward = activeCam.transform.forward;
        Vector3 camSideways = activeCam.transform.right;

        camForward.y = 0;
        camSideways.y = 0;

        camForward.Normalize();
        camSideways.Normalize();

        //Gets the final Directionthat will be applied to player's movement
        finalDirectionInput = (camForward * (-realInput.x)) + (camSideways * (-realInput.y));

        finalDirectionInput.Normalize();

    }

    void FixedUpdate()
    {
        //Movement logic if the input is different than 0
        if(finalDirectionInput.magnitude > 0.01f)
        {

            //Rotates towards the direcction the player is facing
            Quaternion rotTarget = Quaternion.LookRotation(finalDirectionInput);
            Quaternion nextRot = Quaternion.Slerp(rb.rotation, rotTarget, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(nextRot);

            //Applies movement
            rb.linearVelocity = new Vector3(finalDirectionInput.x, 0, finalDirectionInput.z)* speed;

        }

        else
        {
            //Stops the character when player isn't pressing the keyboard
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}
