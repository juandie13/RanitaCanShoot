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
    public void DamagePalyer(int damage)
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
}
