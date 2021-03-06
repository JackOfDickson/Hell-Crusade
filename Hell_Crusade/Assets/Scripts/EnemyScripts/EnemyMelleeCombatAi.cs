using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelleeCombatAi : MonoBehaviour
{
   public bool isInCombat {get; set;}
    UnityEngine.AI.NavMeshAgent agent;
    private GameObject player;
    private float activeCooldown;
    private float distance;
    private Vector3 rayDir;
    public float idleSpeed;
    public float combatSpeed;
    private float differenceInX;

    // Start is called before the first frame update
    void Start()
    {
        isInCombat = false;
        player = GameObject.Find("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if(isInCombat == false){
            agent.speed = idleSpeed;
            return;
        }
        else{
            agent.speed = combatSpeed;
            move();
           
            
        }
        
    }

    private void FixedUpdate() {
        differenceInX = player.transform.position.x - gameObject.transform.position.x;
        if(differenceInX > 1 && isInCombat){
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(differenceInX < -1 && isInCombat){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player")){
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            health.ReceiveDamage(1);
        }
    }


    private void move(){
        if(distance > 0)
        {
            agent.isStopped = false;
            agent.destination = player.transform.position;
           
        }
        else{
            agent.isStopped = true;
        }
       

    }
}
