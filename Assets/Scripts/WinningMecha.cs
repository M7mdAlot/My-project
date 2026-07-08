using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningMecha : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
