using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject Projectile;

    /// <summary>
    /// X per second
    /// </summary>
    [SerializeField]
    float ProjectileSpawnRate = 3;

    [SerializeField]
    int ProjectileSpawnMax = -1;

    private int _numSpawned = 0;

    private float _timeElapsedSinceLastSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float spawnRateInFixedTime = 1 / ProjectileSpawnRate;
        if (_timeElapsedSinceLastSpawn > spawnRateInFixedTime) {
            SpawnProjectile();
            _timeElapsedSinceLastSpawn = 0;
        }
        _timeElapsedSinceLastSpawn += Time.fixedDeltaTime;
    }

    private void SpawnProjectile()
    {
        if (ProjectileSpawnMax < 0 || _numSpawned < ProjectileSpawnMax)
        {
            Quaternion angle = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward);
            Vector3 spawnRel = angle * transform.up;
            GameObject copy = Instantiate(Projectile, transform.position + spawnRel, angle);
            Destroy(copy, 5);
            _numSpawned++;
        }
    }
}
