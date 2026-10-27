using UnityEngine;

public class TestMovingNPC : MonoBehaviour
{

    [SerializeField]
    protected float speed = 2f;


    public Vector3 moveDirection;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDirection = Vector3.right;
        moveDirection.Normalize();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision == null) return;
        Vector3 newDirection = -moveDirection;
        if(collision.gameObject.CompareTag("wall"))
        {
           moveDirection = newDirection;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(moveDirection.x, 0, moveDirection.z) * speed;
    }

    
}
