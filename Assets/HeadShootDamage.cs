using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShootDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyController EC;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoDamage(float damage)
    {
        EC.DamageEnemy(damage);
    }
}
