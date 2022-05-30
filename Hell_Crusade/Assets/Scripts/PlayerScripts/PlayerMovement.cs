using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Camera cam;
    Rigidbody2D body;
    Vector2 movement;
    Vector2 mouse;

    public float dashSpeed;
    public float dashCooldown = 5f; //timer length
    private float dashLength = 0.5f;
    private float baseSpeed = 5f;
    public Animator animator;
    private float dashCounter; //actual timer
    private float dashCoolCounter;
    



    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        speed = baseSpeed;
    
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        
        animator.SetFloat("Speed", speed);
    

    //This is the dash ability
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
            speed = dashSpeed;
            dashCounter = dashLength;
            }
        }
        //dash duration counter
        if (dashCounter > 0){
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    speed = baseSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }
        if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }

        //Sprint
        if(Input.GetKey(KeyCode.LeftShift)){
            speed = 7.0f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            speed = baseSpeed;
        }

        


    }
    private void FixedUpdate()
    {
        body.MovePosition(body.position + movement * speed * Time.deltaTime);

        Vector2 dir = mouse - body.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        body.rotation = angle;
    }



}