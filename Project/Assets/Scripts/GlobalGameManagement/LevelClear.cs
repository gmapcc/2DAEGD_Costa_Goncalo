using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelClear : MonoBehaviour
{
    [SerializeField]protected string NextStageNumber;
    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Level"+NextStageNumber);
    }
}
