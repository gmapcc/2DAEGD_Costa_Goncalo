using System.Buffers.Text;
using UnityEngine;

public class GrabLogic : MonoBehaviour
{
    
    public float grabRange = 2f;
    public LayerMask grabLayer;


    private PlayerInputHandler _input;
    private Transform grabPoint;
    private Rigidbody player;
    private Rigidbody heldObject;
    private HingeJoint heldJoint;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabPoint = GetComponentInChildren<Transform>();
        player = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_input.pushValue > 0.0f)
        {
            tryGrab();
        }
        else release();
    }



    void tryGrab()
    {

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, grabRange, grabLayer))
        {
            player.constraints = RigidbodyConstraints.FreezeRotation;
            Rigidbody rigBod = hit.collider.GetComponent<Rigidbody>();

            if(rigBod != null)
            {
                heldObject = rigBod;

                heldJoint = gameObject.AddComponent<HingeJoint>();
                heldJoint.connectedBody = heldObject;

                heldJoint.autoConfigureConnectedAnchor = false;

                heldJoint.anchor = transform.InverseTransformPoint(grabPoint.position);
                heldJoint.connectedAnchor = heldObject.transform.InverseTransformPoint(hit.point);
            }
        }
    }

    public void release()
    {
        heldObject.AddExplosionForce(0.05f, grabPoint.position, 2f,0.0f);
        player.AddExplosionForce(0.05f, grabPoint.position, 2f, 0.0f);
        if(heldJoint != null)
        {
            Destroy(heldJoint);
        }
        heldObject = null;
    }
}
