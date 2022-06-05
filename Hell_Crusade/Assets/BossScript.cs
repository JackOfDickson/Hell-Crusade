using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health = 2;
    public int goldDrop;

    private GameObject player;
    private PlayerGold playerGold;
    public AudioSource enemySounds;
    public AudioClip enemyScream;
    public GameObject door;
    private Animator animator;
    private int phase;
    

    void Start()
    {
        player = GameObject.Find("Player");
        playerGold = player.GetComponent<PlayerGold>();
        animator = GetComponent<Animator>();
        phase = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {

        if(col.gameObject.tag.Equals("Bullet"))
        {
            Destroy(col.gameObject);
            if(phase != 1){
                health -= 1;
                enemySounds.PlayOneShot(enemyScream);
                if(health == 0){
                    if(phase == 0){
                        phase += 1;
                        animator.SetInteger("phaseCounter", 1);
                        StartCoroutine(EndOfPhase1(5.0f));
                        }
                    if(phase == 2){
                        animator.SetInteger("phaseCounter", 3);
                        StartCoroutine(EndOfBossFight(5.0f));
                    }
                }
            }
        }
        
    }

IEnumerator EndOfPhase1(float wait)
    {
       

        yield return new WaitForSeconds(wait);
        
        phase += 1;
        animator.SetInteger("phaseCounter", 2);
        health = 2;
    
    }
IEnumerator EndOfBossFight(float wait){
        
        
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    
    }


    private void OnDestroy() 
    {
        if(door){
            door.GetComponent<DoorBehaviour>().removeEnemy(gameObject);
        }
    }
}
