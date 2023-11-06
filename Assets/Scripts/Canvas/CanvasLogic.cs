using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLogic : MonoBehaviour
{
    public GameObject reloadGif;
    public GameObject pointer;



    [Header("Face related")]
    public List<Sprite> playerFaces;
    public Image playerCurrentLifeFace;
    public TextMeshProUGUI playerCurrentLifeText;
    public Slider playerCurrentLifeSlider;



    [Header("Shield related")]
    public List<Sprite> shieldSprites;
    public Image shieldCurrentLifeSprite;
    public TextMeshProUGUI shieldCurrentLifeText;
    public Slider shieldCurrentLifeSlider;



    [Header("Ammo related")]
    public List<Sprite> ammoSprites;
    public Image ammoCurrentSprite;
    public TextMeshProUGUI ammoCurrentText;


    // Update is called once per frame
    void Update()
    {
        ReloadLogic();
        PlayerLife();
        ShieldLife();
        AmmoLeft();
    }

    private void ReloadLogic()
    {
        if (GameManager.Instance.isReloading)
        {
            reloadGif.SetActive(true);
            pointer.SetActive(false);
        }
        else
        {
            reloadGif.SetActive(false);
            pointer.SetActive(true);
        }
    }

    private void PlayerLife()
    {
        FaceLogic();
        int lifeValue = ((int)GameManager.Instance.playerLifeCurrent);
        int lifeMax = ((int)GameManager.Instance.playerLifeMax);

        if (lifeValue > lifeMax) { lifeValue = lifeMax; }
        else if (lifeValue < 0) { lifeValue = 0; }

        playerCurrentLifeText.SetText(lifeValue.ToString() + "%");
        playerCurrentLifeSlider.value = lifeValue;
    }
    private void FaceLogic()
    {
        if (GameManager.Instance.playerLifeCurrent >= GameManager.Instance.playerLifeMax / 5 * 4)
        {
            playerCurrentLifeFace.sprite = playerFaces[0];
        }
        else if (GameManager.Instance.playerLifeCurrent >= GameManager.Instance.playerLifeMax / 5 * 3)
        {
            playerCurrentLifeFace.sprite = playerFaces[1];
        }
        else if (GameManager.Instance.playerLifeCurrent >= GameManager.Instance.playerLifeMax / 5 * 2)
        {
            playerCurrentLifeFace.sprite = playerFaces[2];
        }
        else if (GameManager.Instance.playerLifeCurrent >= GameManager.Instance.playerLifeMax / 5 * 1)
        {
            playerCurrentLifeFace.sprite = playerFaces[3];
        }
        else
        {
            playerCurrentLifeFace.sprite = playerFaces[4];
        }
    }

    private void ShieldLife()
    {
        ShieldLogic();
        int shieldValue = ((int)GameManager.Instance.armorLifeCurrent);
        int shieldMax = ((int)GameManager.Instance.armorLifeMax);

        if (shieldValue > shieldMax) { shieldValue = shieldMax; }
        else if (shieldValue < 0) { shieldValue = 0; }

        shieldCurrentLifeText.SetText(shieldValue.ToString() + "%");
        shieldCurrentLifeSlider.value = shieldValue;
    }

    private void ShieldLogic()
    {
        if (GameManager.Instance.armorLifeCurrent >= GameManager.Instance.armorLifeMax / 3 * 2)
        {
            shieldCurrentLifeSprite.sprite = shieldSprites[0];
        }
        else if (GameManager.Instance.armorLifeCurrent >= GameManager.Instance.armorLifeMax / 3 * 1)
        {
            shieldCurrentLifeSprite.sprite = shieldSprites[1];
        }
        else
        {
            shieldCurrentLifeSprite.sprite = shieldSprites[2];
        }
    }

    private void AmmoLeft()
    {
        AmmoLogic();
        int ammoValue = GameManager.Instance.gunAmmoCurrent;
        int ammoMax = GameManager.Instance.gunAmmoMax;

        if (ammoValue > ammoMax) { ammoValue = ammoMax; }
        else if (ammoValue < 0) { ammoValue = 0; }

        ammoCurrentText.SetText(ammoValue.ToString() + " / " + ammoMax.ToString());
    }

    private void AmmoLogic()
    {
        if (GameManager.Instance.gunAmmoCurrent == GameManager.Instance.gunAmmoMax)
        {
            ammoCurrentSprite.sprite = ammoSprites[0];
        }
        else if (GameManager.Instance.gunAmmoCurrent >= GameManager.Instance.gunAmmoMax / 4 * 3)
        {
            ammoCurrentSprite.sprite = ammoSprites[1];
        }
        else if (GameManager.Instance.gunAmmoCurrent >= GameManager.Instance.gunAmmoMax / 4 * 2)
        {
            ammoCurrentSprite.sprite = ammoSprites[2];
        }
        else if (GameManager.Instance.gunAmmoCurrent >= GameManager.Instance.gunAmmoMax / 4 * 1)
        {
            ammoCurrentSprite.sprite = ammoSprites[3];
        }
        else if (GameManager.Instance.gunAmmoCurrent > 0)
        {
            ammoCurrentSprite.sprite = ammoSprites[4];
        }
        else
        {
            ammoCurrentSprite.sprite = ammoSprites[5];
        }
    }
}
