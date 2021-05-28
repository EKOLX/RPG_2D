using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab = default;
    [SerializeField] private float repeatInterval;

    private void Start()
    {
        if (repeatInterval > 0)
        {
            InvokeRepeating(nameof(SpawnObject), 0.0f, repeatInterval);
        }
    }

    private void SpawnObject()
    {
        if (spawnPrefab != null)
        {
            Instantiate(spawnPrefab, transform.position, Quaternion.identity);
        }
    }

}
