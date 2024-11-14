using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab; // Reference to the collectible prefab
    public int collectibleCount = 10; // Number of collectibles to spawn
    public Vector2 spawnRangeX = new Vector2(-10, 10); // Range for X-axis spawning
    public Vector2 spawnRangeZ = new Vector2(-10, 10); // Range for Z-axis spawning

    // Start is called before the first frame update
    void Start()
    {
        SpawnCollectibles();
    }

    // Method to spawn collectibles at random positions
    void SpawnCollectibles()
    {
        for (int i = 0; i < collectibleCount; i++)
        {
            // Randomize the position within the defined range
            float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randomZ = Random.Range(spawnRangeZ.x, spawnRangeZ.y);
            Vector3 randomPosition = new Vector3(randomX, 0.5f, randomZ);

            // Spawn the collectible at the random position
            Instantiate(collectiblePrefab, randomPosition, Quaternion.identity);
        }
    }

    // New method to spawn a single collectible at a random position
    public void SpawnCollectible()
    {
        // Randomize the position within the defined range
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomZ = Random.Range(spawnRangeZ.x, spawnRangeZ.y);
        Vector3 randomPosition = new Vector3(randomX, 0.5f, randomZ);

        // Spawn the collectible at the random position
        Instantiate(collectiblePrefab, randomPosition, Quaternion.identity);
    }
}
