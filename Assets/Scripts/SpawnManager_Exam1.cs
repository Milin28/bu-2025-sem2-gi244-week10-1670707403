using UnityEngine;

public class SpawnManager_Exam1 : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public Vector3 spawnPos = new(25, 0, 0);

    public float startDelay = 2;
    public float repeatRate = 2;

    private PlayerController playerController;

    
    void Start()
    {
       

        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);

        GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefab.Length);
        GameObject prefab = obstaclePrefab[index];
        Instantiate(prefab, spawnPos, prefab.transform.rotation);
    }
}
