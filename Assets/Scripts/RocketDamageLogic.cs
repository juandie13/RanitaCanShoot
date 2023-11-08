using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDamageLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBody"))   
        {
            Debug.Log(other.name);
            other.gameObject.GetComponent<BodyDamage>().DoDamage(35f);
        }
        else if (other.gameObject.CompareTag("HeadShoot"))
        {
            other.gameObject.GetComponent<BodyDamage>().DoDamage(60f);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerInteractions>().DamagePlayer(5f);
        }
    }
}
