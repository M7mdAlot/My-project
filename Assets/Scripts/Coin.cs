using JetBrains.Annotations;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource audioSource;
    public float rotationSpeed = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();

            if (GameManager.Instance != null)
                GameManager.Instance.CollectCoin();

            Destroy(gameObject);

        }
    }
    private void Update()
    {
        transform.Rotate(0,rotationSpeed * Time.deltaTime,0);
    }
}
