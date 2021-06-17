using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_pattern : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator _animatorController;


    public int _speed;
    private float slidingH;
    private float slidingV;
    public float JumpForce;
    public bool flip = true;
    public bool isdead = false;
    public int healts = 3;


    public ForGrondChecker GroundChecker1;
    public GameObject GroundCheckGO;
    public bool isGrounded;
    private void Start()
    {
        _animatorController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
            if (isdead!=true)
            {
                CheckGround();
                walk();
                if (isGrounded == false)
                {
                    _animatorController.Play("jump");
                }
                else if (rb.velocity.y == 0 && rb.velocity.x == 0 && isGrounded == true)
                {
                    _animatorController.Play("idle");
                }
                else if (isGrounded == true && rb.velocity.x != 0)
                {
                    _animatorController.Play("run");
                }
                else
                {
                    _animatorController.Play("idle");
                }

                if (Input.GetKeyDown(KeyCode.W) && isGrounded)
                {
                    Jump();
                }
                
            }
    }
    void Jump()
    {
        rb.velocity = (Vector2.up * JumpForce);
    }

    void walk()
    {
        float h = 0f;
        float v = 0f;
        Vector2 smoothedInput;
        if (Input.GetKey(KeyCode.A))
        {
            h = -1f;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            h = 1f;

        }
        smoothedInput = SmoothInput(h, v);


        float smoothedH = smoothedInput.x;
        float smoothedV = smoothedInput.y;

        rb.velocity = new Vector2(smoothedInput.x * _speed, rb.velocity.y);
        if (smoothedInput.x < 0 && !flip)
        {
            Flip1();
        }
        if (smoothedInput.x > 0 && flip)
        {
            Flip();
        }
    }
    void Flip()
    {
        flip = !flip;
        gameObject.transform.localScale = new Vector3(-1f,1f,1f);
        //eyes.transform.localScale = new Vector3(-1f, 1f, 1f);

    }
    void Flip1()
    {
        flip = !flip;
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        //eyes.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private void CheckGround()
    {
        if (GroundChecker1.isGrounded == false)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }
    private Vector2 SmoothInput(float targetH, float targetV)
    {
        float sensitivity = 10f;
        float deadZone = 0.001f;

        slidingH = Mathf.MoveTowards(slidingH,
                      targetH, sensitivity * Time.deltaTime);

        slidingV = Mathf.MoveTowards(slidingV,
                      targetV, sensitivity * Time.deltaTime);

        return new Vector2(
               (Mathf.Abs(slidingH) < deadZone) ? 0f : slidingH,
               (Mathf.Abs(slidingV) < deadZone) ? 0f : slidingV);
    }        
            
            
        
}
