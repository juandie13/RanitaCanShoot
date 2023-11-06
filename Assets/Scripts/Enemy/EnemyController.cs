using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    #region States
    public IdleState IdleState;
    public FollowState FollowState;
    private State currentState;
    #endregion

    #region Parameters
    //public Transform Player;
    public GameObject target;
    public float DistanceToFollow = 4f;
    public float DistanceToAttack = 3f;
    public float Speed = 1f;
    public Transform FirePoint;
    public float CoolDownTime = 1.0f;
    public float enemyHealth = 100f;
    #endregion

    #region Readonly Properties
    public Rigidbody rb {private set; get;}
    public Animator animator {private set; get;}
    public NavMeshAgent agent {private set; get;}
    #endregion

    

    private void Awake() 
    {
        IdleState = new IdleState(this);
        FollowState = new FollowState(this);

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Seteamos el estado inicial
        currentState = IdleState;    
    }

    private void Start() 
    {
        currentState.OnStart();
        target = GameObject.Find("Player");
    }

    private void Update() 
    {
        foreach (var transition in currentState.Transitions)
        {
            if (transition.IsValid())
            {
                // Ejecutar Transicion
                currentState.OnFinish();
                currentState = transition.GetNextState();
                currentState.OnStart();
                break;
            }
        }
        currentState.OnUpdate();    
    }
    public void DamageEnemy(float damage) {
        enemyHealth -= damage;
        Die();
    }
    public void Die()
    {
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
