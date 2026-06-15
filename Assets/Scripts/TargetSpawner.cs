using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{

    public Transform player;
    public GameObject target;
    public WorldSpawner worldSpawner;

    float minSpawnDistance = 10f;

    public bool canSpawnTargets = true;

    public int currentPackageValue = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(SpawnTargetInitial), 0.1f);
    }

    void SpawnTargetInitial()
    {
        SpawnTarget(true);
    }

    public void SpawnTarget(bool ignoreDistance)
    {
        List<Vector3> candidates = new List<Vector3>();

        foreach (var kvp in worldSpawner.GetActiveChunks())
        {
            GameObject chunkObject = kvp.Value;

            if (chunkObject == null)
            {
                continue;
            }

            if (!ignoreDistance && Vector3.Distance(player.position, chunkObject.transform.position) < minSpawnDistance)
            {
                continue;
            }

            Tilemap road = chunkObject.transform.Find("Road")?.GetComponent<Tilemap>();

            if (road == null)
            {
                continue;
            }

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
            Debug.LogWarning("No valid target spawn position found!");
            return;
        }

        Vector3 spawnPosition = candidates[Random.Range(0, candidates.Count)];

        currentPackageValue = Mathf.RoundToInt(Vector3.Distance(player.position, spawnPosition));

        target.transform.position = spawnPosition;
        target.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

        if (!canSpawnTargets)
        {
            return;
        }

        if (target == null || player == null || worldSpawner == null)
        {
            return;
        }

        Vector2Int targetCoord = worldSpawner.WorldToGrid(target.transform.position);

        if (!worldSpawner.IsChunkActive(targetCoord))
        {
            SpawnTarget(false);
        }
    }
}
