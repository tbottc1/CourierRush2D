using UnityEngine;

public class PackageTrigger : MonoBehaviour
{
    public DeliverySpawner deliverySpawner;
    public TargetSpawner targetSpawner;
    public AudioManager audioManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlayPackagePickup();

            targetSpawner.canSpawnTargets = false;
            targetSpawner.target.SetActive(false);

            deliverySpawner.SpawnDeliveryPoint();
        }
    }
}