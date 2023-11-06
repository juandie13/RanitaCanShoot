using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamagePlayer(float damage)
    {
        if (GameManager.Instance.armorLifeCurrent > 0)
        {
            GameManager.Instance.armorLifeCurrent -= damage;
        }
        else
        {
            GameManager.Instance.playerLifeCurrent -= damage;
        }
    }

    private void onCollisionEnter(Collision collision){
        
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("CHOCANDO");
        //Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("EnemySmall") || collision.transform.CompareTag("EnemyBig"))
        {
           // Debug.Log("recibirDano");
            DamagePlayer(0.1f);
        }
    }
}