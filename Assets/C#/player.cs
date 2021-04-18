using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class player : NetworkBehaviour
{

    public Rigidbody2D rb;
    public Animator _animatorController;

    public Text myNick;

    public GameObject gun;
    public forGun gunscript;

    public int _speed;
    private float slidingH;
    private float slidingV;
    public float JumpForce;
    public bool flip = true;

    public GameObject eyes; 

    public ForGrondChecker GroundChecker1;
    public GameObject GroundCheckGO;
    public bool isGrounded;
    private void Start()
    {
        _animatorController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (!isLocalPlayer) return;

        if (hasAuthority)
        {
            eyes.SetActive(true);
            myNick.text = PlayerPrefs.GetString("net_name");
        }

    }

    void Update()
    {
        
        if (!isLocalPlayer) return;

        if (hasAuthority)
        {


            CheckGround();
            walk();
            if (isGrounded == false)
            {
                _animatorController.Play("Jump");
            }
            else if (rb.velocity.y == 0 && rb.velocity.x == 0 && isGrounded == true)
            {
                _animatorController.Play("Idle");
            }
            else if (isGrounded == true && rb.velocity.x != 0)
            {
                _animatorController.Play("Walk");
            }
            else
            {
                _animatorController.Play("Idle");
            }

            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                //if (gun.activeInHierarchy)
                //{
                //    gun.SetActive(false);
                //}
                //else
                //{
                //    gun.SetActive(true);
                //}


                //if (isServer)
                //{
                CmdenGun();
                //}
                //if (isClient)
                //{
                //    CmdenGun();
                //}
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                //gunscript.Reload();
                //if (isServer)
                //{
                    CmdreloadGun();
                //}
                //if (isClient)
                //{
                //    CmdreloadGun();
                //}
            }
        }
            
            
        
    }
    void Jump()
    {
        rb.velocity = (Vector2.up * JumpForce);
    }

    [Command]
    public void CmdenGun()
    {
        //if (gun.activeInHierarchy)
        //{
        //    gun.SetActive(false);
        //}
        //else
        //{
        //    gun.SetActive(true);
        //}
        RpcenGun();
    }

    [ClientRpc]
    public void RpcenGun()
    {
        if (gun.activeInHierarchy)
        {
            gun.SetActive(false);
        }
        else
        {
            gun.SetActive(true);
        }
    }



    
    [ClientRpc]
    public void RpcreloadGun()
    {
        gunscript.Reload();
    }

    [Command]
    public void CmdreloadGun()
    {
        //gunscript.Reload();
        RpcreloadGun();
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
        eyes.transform.localScale = new Vector3(-1f, 1f, 1f);

    }
    void Flip1()
    {
        flip = !flip;
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        eyes.transform.localScale = new Vector3(1f, 1f, 1f);
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


