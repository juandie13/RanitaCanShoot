using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Ammo related")]
    public int gunAmmoCurrent = 22;
    public int gunAmmoMax = 22;
    public int gunAmmoDamage = 10;
    public bool canReload = false;
    public bool canShoot = true;
    public bool isReloading = false;

    [Header("Life related")]
    public float playerLifeCurrent = 100;
    public float playerLifeMax = 100;
    public float armorLifeCurrent = 100;
    public float armorLifeMax = 100;
    public bool perdiste = false;
    public int ordaCount;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isReloading)
        {
            AmmoRelatedLogic();
        }
    }
    private void AmmoRelatedLogic()
    {

        // Shooting logic
        if (gunAmmoCurrent <= 0)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }

        // Reloading logic
        if (gunAmmoCurrent == gunAmmoMax)
        {
            canReload = false;
        }
        else
        {
            canReload = true;
        }
    }
}