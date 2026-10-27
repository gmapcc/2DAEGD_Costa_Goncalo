using Unity.VisualScripting;
using UnityEngine;

public class PushLogic : MonoBehaviour
{

    //Push stats
    [SerializeField]protected float pushForce = 10f;
    [SerializeField]protected float pushRange = 2f;
    [SerializeField]protected Transform PushPoint;
    [SerializeField]protected Vector3 boxSize = new Vector3(0.5f,0.5f, 0.05f);
    private Rigidbody playerBod; 
    [SerializeField]protected LayerMask pushLayer;


    //Input Handler
    private PlayerInputHandler _inputs;

    void Start()
    {
        _inputs = GetComponent<PlayerInputHandler>();
        playerBod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputs.canPush)
        {
            TryPsh();
        }
    }

    void TryPsh()
    {
        RaycastHit hit;

        bool hitSomething =Physics.BoxCast(PushPoint.position,boxSize,PushPoint.forward,out hit, PushPoint.rotation,pushRange,pushLayer);
        
        if(hitSomething)
        {
            Rigidbody pushedRigBod = hit.collider.GetComponent<Rigidbody>();

            if(pushedRigBod != null)
            {
                pushedRigBod.AddForce(PushPoint.forward * pushForce, ForceMode.Impulse);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (PushPoint == null) return;

        Gizmos.color = Color.cyan;
        // Desenha a caixa na posição inicial
        Gizmos.matrix = Matrix4x4.TRS(PushPoint.position, PushPoint.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize * 2);

        // Desenha uma linha mostrando o alcance do empurrão
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.DrawLine(PushPoint.position, PushPoint.position + PushPoint.forward * pushRange);
    }
}
