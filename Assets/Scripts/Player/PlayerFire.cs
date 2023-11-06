using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
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
                Debug.Log(hit.collider.transform.name);
                Quaternion lookAt = Quaternion.LookRotation(hit.normal);
                GameObject temp = Resources.Load<GameObject>("BulletCollision");
                var obj = Instantiate(
                    temp,
                    hit.point,
                    lookAt);
                GameManager.Instance.gunAmmoCurrent--;
                Destroy(obj, 2f);

            }
        }
    }

    public void Reload()
    {
        if (GameManager.Instance.canReload)
        {
            StartCoroutine(ReloadWeapon());
        }
    }

    private IEnumerator ReloadWeapon()
    {
        GameManager.Instance.isReloading = true;
        GameManager.Instance.canShoot = false;
        yield return new WaitForSeconds(5f);
        GameManager.Instance.gunAmmoCurrent = GameManager.Instance.gunAmmoMax;
        GameManager.Instance.canShoot = true;
        GameManager.Instance.isReloading = false;
    }

}
