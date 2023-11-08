using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemyPrefab;
    public EnemyController enemyPrefab2;
    float cronometro = 0.0f;
    private float startSpawn = 1.0f;
    public float spawnRate = 60.0f;
    public float trajectoryVariance = 15.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 30;
    public AudioSource startRound;
    public int currentRound;
    public static AudioClip startSound;
    public int enemyList = GameManager.Instance.ordaCount;
    public TextMeshProUGUI rondaText;

    public static EnemySpawner Instance { get; private set; }
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.startSpawn, this.spawnRate);
        spawnAmount = GameManager.Instance.ordaCount;
        startSound = Resources.Load<AudioClip>("startRound");
    }
    private void Update()
    {
        cronometro += 1 * Time.deltaTime;
        if (cronometro >= spawnRate)
        {
            spawnRate -= 5.0f;
            cronometro = 0.0f;
        }
        else
        {
            if (spawnRate <= 10.0f)
            {
                spawnRate = 10.0f;
            }
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Spawn()
    {
        enemyList = 0;
        currentRound++;
        startRound.PlayOneShot(startSound);
        rondaText.text = "Ronda actual: "+ currentRound;
        for (int i = 0; i < this.spawnAmount; i++)
        {
            int randomNumber = Random.Range(0, this.spawnAmount);

            UnityEngine.Vector3 spawnDirection = Random.insideUnitSphere.normalized * spawnDistance;
            UnityEngine.Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            UnityEngine.Quaternion rotation = UnityEngine.Quaternion.AngleAxis(variance, UnityEngine.Vector3.forward);

            if (i < randomNumber)
            {
                EnemyController enemy = Instantiate(this.enemyPrefab, spawnPoint, rotation);
                enemyList++;
            }
            else
            {
                EnemyController enemy = Instantiate(this.enemyPrefab2, spawnPoint, rotation);
                enemyList++;
            }
        }
    }
}
