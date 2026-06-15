using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class DeliverySpawner : MonoBehaviour
{
    public Transform player;
    public WorldSpawner worldSpawner;
    public TargetArrow targetArrow;

    public GameObject deliveryPoint;

    public int currentDeliveryValue = 0;

    float minSpawnDistance = 10f;
    bool deliveryActive = false;

    void Start()
    {
        deliveryPoint.SetActive(false);
    }

    public void SpawnDeliveryPoint()
    {
        if (deliveryActive)
        {
            deliveryPoint.SetActive(false);
            deliveryActive = false;
        }

        List<Vector3> candidates = new List<Vector3>();

        foreach (var kvp in worldSpawner.GetActiveChunks())
        {
            GameObject chunkObject = kvp.Value;

            if (chunkObject == null)
                continue;

            if (Vector3.Distance(player.position, chunkObject.transform.position) < minSpawnDistance)
                continue;

            Tilemap road = chunkObject.transform.Find("Road")?.GetComponent<Tilemap>();

            if (road == null)
                continue;

            foreach (Vector3Int cellPosition in road.cellBounds.allPositionsWithin)
            {
                if (road.HasTile(cellPosition))
                {
                    Vector3 worldPosition = road.GetCellCenterWorld(cellPosition);
                    candidates.Add(worldPosition);
                }
            }
        }

        if (candidates.Count == 0)
        {
            Debug.LogWarning("No valid delivery point position found!");
            return;
        }

        Vector3 spawnPosition = candidates[Random.Range(0, candidates.Count)];

        currentDeliveryValue = Mathf.RoundToInt(Vector3.Distance(player.position, spawnPosition));

        deliveryPoint.transform.position = spawnPosition;
        deliveryPoint.SetActive(true);

        targetArrow.target = deliveryPoint.transform;

        deliveryActive = true;
    }

    public void CompleteDelivery()
    {
        if (!deliveryActive)
            return;

        deliveryPoint.SetActive(false);
        deliveryActive = false;
    }

    void Update()
    {
        if (!deliveryActive)
            return;

        Vector2Int deliveryCoord = worldSpawner.WorldToGrid(deliveryPoint.transform.position);

        if (!worldSpawner.IsChunkActive(deliveryCoord))
        {
            SpawnDeliveryPoint();
        }
    }
}