using UnityEngine;

public class DeliveryTrigger : MonoBehaviour
{
    public DeliverySpawner deliverySpawner;
    public TargetSpawner targetSpawner;
    public TargetArrow targetArrow;
    public GameManager gameManager;
    public AudioManager audioManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlayDeliveryComplete();

            int pointsEarned = targetSpawner.currentPackageValue + deliverySpawner.currentDeliveryValue;
            gameManager.AddScore(pointsEarned);

            deliverySpawner.CompleteDelivery();

            targetSpawner.canSpawnTargets = true;
            targetSpawner.SpawnTarget(false);

            targetArrow.target = targetSpawner.target.transform;
        }
    }
}