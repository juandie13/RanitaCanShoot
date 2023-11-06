using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static AudioClip playerShoot, playerReload, PlayerNoAmmo;
    public static AudioSource audioSrc;
    public enum SoundType
    {
        shoot,
        reload,
        noAmmo
    }
    void Start()
    {
        // ShortAudios
        playerShoot = Resources.Load<AudioClip>("Shoot");
        playerReload = Resources.Load<AudioClip>("Reload");
        PlayerNoAmmo = Resources.Load<AudioClip>("Noammo");
        // AudioSource
        audioSrc = GetComponent<AudioSource>();
    }

    // MonoBehaviour class method
    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(SoundType clip)
    {
        switch (clip)
        {
            case SoundType.shoot:
                audioSrc.PlayOneShot(playerShoot);
                break;
            case SoundType.reload:
                audioSrc.PlayOneShot(playerReload);
                break;
            case SoundType.noAmmo:
                audioSrc.PlayOneShot(PlayerNoAmmo);
                break;
        }

    }
}
