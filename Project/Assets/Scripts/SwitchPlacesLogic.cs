using UnityEngine;

public class SwitchPlacesLogic : MonoBehaviour
{
   [SerializeField] GameObject ScareCrow;
   private PlayerInputHandler _inputs;
   private Vector3 playerPos;
   private Vector3 scarecrowPos;

    void Start()
    {
        _inputs = GetComponent<PlayerInputHandler>();
        if(ScareCrow == null)
        {
            ScareCrow = GameObject.Find("ScareCrow(Clone)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputs.canTP)
        {
            scarecrowPos = ScareCrow.transform.position;
            playerPos = transform.position;
            transform.position = scarecrowPos;
            ScareCrow.transform.position = playerPos;
        }
    }
}
