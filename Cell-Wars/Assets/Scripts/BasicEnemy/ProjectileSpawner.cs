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

    [SerializeField]
    float AngleShiftNextProjectile = 50f;

    private int _numSpawned = 0;

    private float _timeElapsedSinceLastSpawn = 0;

    private float _nextAngle = 0;

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
            Quaternion angle = Quaternion.AngleAxis(_nextAngle, Vector3.forward);

            // Send one in each of 4 cardinal directions
            for (int i = 0; i < 4; i++)
            {
                GameObject copy = Instantiate(Projectile, transform.position, angle);
                Destroy(copy, 5);
                angle *= Quaternion.AngleAxis(90f, Vector3.forward);
                _numSpawned++;
            }
            _nextAngle += AngleShiftNextProjectile % 360f;
        }
    }
}
