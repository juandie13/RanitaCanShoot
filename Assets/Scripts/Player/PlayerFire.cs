using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public SoundManager SM;
    private bool reloadSound = true;
    private void Update()
    {
        Debug.DrawRay(
            transform.position,
            transform.forward * 10f,
            Color.red
        );
    }

    public void Fire()
    {
        if (GameManager.Instance.canShoot)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 20f))
            {
                // Choco con algo
               // Debug.Log(hit.collider.transform.name);
                if (hit.collider.gameObject.CompareTag("HeadShoot"))
                {
                    hit.collider.gameObject.GetComponent<HeadShootDamage>().DoDamage(15f);
                }
                else if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<BodyDamage>().DoDamage(5f);
                }
                Quaternion lookAt = Quaternion.LookRotation(hit.normal);
                GameObject temp = Resources.Load<GameObject>("BulletCollision");
                var obj = Instantiate(
                    temp,
                    hit.point,
                    lookAt);
                SM.PlaySound(SoundManager.SoundType.shoot);
                GameManager.Instance.gunAmmoCurrent--;
                Destroy(obj, 2f);

            }
        }
        else if (GameManager.Instance.canShoot == false && GameManager.Instance.isReloading == false)
        {
            SM.PlaySound(SoundManager.SoundType.noAmmo);
        }
    }

    public void Reload()
    {
        if (GameManager.Instance.canReload)
        {
            if (reloadSound == true)
            {
                reloadSound = false;
                SM.PlaySound(SoundManager.SoundType.reload);
            }
            StartCoroutine(ReloadWeapon());
        }
    }

    private IEnumerator ReloadWeapon()
    {
        GameManager.Instance.isReloading = true;
        GameManager.Instance.canShoot = false;
        yield return new WaitForSeconds(2f);
        GameManager.Instance.gunAmmoCurrent = GameManager.Instance.gunAmmoMax;
        GameManager.Instance.canShoot = true;
        GameManager.Instance.isReloading = false;
        reloadSound = true;
    }

}
