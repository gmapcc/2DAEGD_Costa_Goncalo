using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    [SerializeField]
    protected Camera[] cameras;

    public Camera activeCamera;
    

    private int CamID;
    private int PrevID;
    private bool canCycle;

    void Awake()
    {
        cameras[0].gameObject.SetActive(true);
        CamID = 0;

        for(int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    void OnCycleCams(InputValue value)
    {
        float axisVal  = value.Get<float>();
        Debug.Log("cameras: " + cameras.Length + "\n");

        
        PrevID = CamID;

        switch (axisVal)
        {
            case 0f:
                canCycle = true;
                break;
            case > 0f:
                CamID++;
                if(CamID > cameras.Length - 1)
                {
                    CamID = 0;
                }
                break;
            case < 0f:
                CamID--;
                if(CamID < 0)
                {
                    CamID = cameras.Length - 1;
                }
                break;
            
        }

        cameras[CamID].gameObject.SetActive(true);
        cameras[PrevID].gameObject.SetActive(false);
        activeCamera = cameras[CamID];

    }
}
