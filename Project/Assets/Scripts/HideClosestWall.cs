using UnityEngine;

public class HideClosestWall : MonoBehaviour
{

    [SerializeField]
    protected GameObject[] closestWalls;

    private bool canHideWalls;

    void OnEnable()
    {
        canHideWalls = true;
    }

    void OnDisable()
    {
        canHideWalls = false;
        SetWallsVisibility(true);
    }

    void LateUpdate()
    {
        if (canHideWalls)
        {
            SetWallsVisibility(false);
        }
    }

    void SetWallsVisibility(bool state)
    {
        if(closestWalls == null) return;

        foreach(GameObject wall in closestWalls)
        {
            if(wall != null)
            {
                MeshRenderer wallRender = wall.GetComponent<MeshRenderer>();

                if(wallRender != null && wallRender.enabled !=state)
                {
                    wallRender.enabled = state;
                }
            }
        }
    }
}
