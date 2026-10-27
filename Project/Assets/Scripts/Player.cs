
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected float speed = 5f;
    [SerializeField]
    protected float rotationSpeed = 10f;
    private Rigidbody rb;
    private PlayerInputHandler _inputHandler;
    private CameraControls _cameraControls;
    private Vector3 finalDirectionInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        _cameraControls = GetComponent<CameraControls>();

        if(rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 realInput = _inputHandler.InputVector;

        Camera activeCam = _cameraControls.activeCamera;

        if(activeCam == null) return;

        Vector3 camForward = activeCam.transform.forward;
        Vector3 camSideways = activeCam.transform.right;

        camForward.y = 0;
        camSideways.y = 0;

        camForward.Normalize();
        camSideways.Normalize();

        finalDirectionInput = (camForward * (-realInput.x)) + (camSideways * (-realInput.y));

        finalDirectionInput.Normalize();

    }

    void FixedUpdate()
    {
        if(finalDirectionInput.magnitude > 0.01f)
        {
            Quaternion rotTarget = Quaternion.LookRotation(finalDirectionInput);
            Quaternion nextRot = Quaternion.Slerp(rb.rotation, rotTarget, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(nextRot);

            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            rb.AddForce(finalDirectionInput * speed, ForceMode.VelocityChange);
        }

        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}
