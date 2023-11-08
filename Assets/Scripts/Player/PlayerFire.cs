using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public SoundManager SM;
    private bool reloadSound = true;

    public float speed = 10f;
    private float distance = 20f;

    [SerializeField] public GameObject player;
    [SerializeField] GameObject rocketLauncher;
    private void Update()
    {
        if (player.GetComponent<PlayerMovement>().weaponSelect == 0)
        {
            speed = 10f;
            distance = 20f;
        }
        else
        {
            speed = 100;
            distance = 60f;
        }

        Debug.DrawRay(
            transform.position,
            transform.forward * speed,
            Color.red
        );
    }

    public void Fire()
    {
        if (GameManager.Instance.canShoot)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, distance) && (GameManager.Instance.gunAmmoCurrent > 0 || rocketLauncher.GetComponent<GunfireController>().ammo > 0))
            {
                // Choco con algo
                // Debug.Log(hit.collider.transform.name);
                if (hit.collider.gameObject.CompareTag("HeadShoot"))
                {
                    if (player.GetComponent<PlayerMovement>().weaponSelect == 0)
                    {
                        hit.collider.gameObject.GetComponent<HeadShootDamage>().DoDamage(15f);
                    }
                }
                else if (hit.collider.gameObject.CompareTag("EnemyBody"))
                {
                    if (player.GetComponent<PlayerMovement>().weaponSelect == 0)
                    {
                        hit.collider.gameObject.GetComponent<BodyDamage>().DoDamage(5f);
                    }
                }
                Quaternion lookAt = Quaternion.LookRotation(hit.normal);
                if (player.GetComponent<PlayerMovement>().weaponSelect == 0)
                {
                    GameObject temp = Resources.Load<GameObject>("BulletCollision");
                    var obj = Instantiate(
                        temp,
                        hit.point,
                        lookAt);
                    SM.PlaySound(SoundManager.SoundType.shoot);
                    GameManager.Instance.gunAmmoCurrent--;
                    Destroy(obj, 2f);
                }
                else
                {

                }


            }
        }
        else if (GameManager.Instance.canShoot == false && GameManager.Instance.isReloading == false)
        {
            SM.PlaySound(SoundManager.SoundType.noAmmo);
        }
    }

    public void Reload()
    {
        if((GameManager.Instance.gunAmmoCurrent!=GameManager.Instance.gunAmmoMax && player.GetComponent<PlayerMovement>().weaponSelect==0) || (rocketLauncher.GetComponent<GunfireController>().ammo!=1 && player.GetComponent<PlayerMovement>().weaponSelect==1)){
            if(!GameManager.Instance.isReloading){
                if (reloadSound == true)
                {
                    reloadSound = false;
                    SM.PlaySound(SoundManager.SoundType.reload);
                }
                StartCoroutine(ReloadWeapon());
            }
        }
    }

    private IEnumerator ReloadWeapon()
    {
        GameManager.Instance.isReloading = true;
        GameManager.Instance.canShoot = false;
        yield return new WaitForSeconds(2f);
        if (player.GetComponent<PlayerMovement>().weaponSelect == 0)
        {
            GameManager.Instance.gunAmmoCurrent = GameManager.Instance.gunAmmoMax;
        }
        else
        {
            rocketLauncher.GetComponent<GunfireController>().ammo = 1;
            rocketLauncher.GetComponent<GunfireController>().ReEnableDisabledProjectile();
        }
        GameManager.Instance.canShoot = true;
        GameManager.Instance.isReloading = false;
        reloadSound = true;


    }

}
