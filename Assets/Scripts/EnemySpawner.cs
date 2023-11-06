using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemyPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 60.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 30;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }
    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            UnityEngine.Vector3 spawnDirection = Random.insideUnitSphere.normalized * spawnDistance;
            UnityEngine.Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            UnityEngine.Quaternion rotation = UnityEngine.Quaternion.AngleAxis(variance, UnityEngine.Vector3.forward);

            EnemyController enemy = Instantiate(this.enemyPrefab, spawnPoint, rotation);
        }
    }
}
