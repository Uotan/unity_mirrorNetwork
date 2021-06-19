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
    public bool isdead = false;
    public int healts = 3;

    public GameObject _camera;
    public GameObject _canvas;
    public GameObject _nickText;

    public CapsuleCollider2D _collider;

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
            _camera.SetActive(true);
            myNick.text = PlayerPrefs.GetString("net_name");
            CmdCanvas();
        }

    }

    void Update()
    {
        
        if (!isLocalPlayer) return;

        if (hasAuthority)
        {
            if (isdead!=true)
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
                    CmdenGun();
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    CmdreloadGun();
                }
                if (Input.GetKeyUp(KeyCode.F))
                {
                    healts--;
                    if (healts==0)
                    {
                        isdead = true;
                        
                    }
                }
            }
            else
            {
                CmdenGun();
                CmdDead();
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
        RpcenGun();
    }

    [ClientRpc]
    public void RpcenGun()
    {
        if (isdead==true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            gun.SetActive(false);
            gameObject.layer=9;
        }
        else
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
        
    }
    [ClientRpc]
    public void RpcreloadGun()
    {
        gunscript.Reload();
    }

    [Command]
    public void CmdreloadGun()
    {
        RpcreloadGun();
    }

    [ClientRpc]
    public void RpcDead()
    {
        _animatorController.Play("Dead");
    }

    [Command]
    public void CmdDead()
    {
        RpcDead();
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
        _camera.transform.localScale = new Vector3(-1f, 1f, 1f);
        _nickText.transform.localScale = new Vector3(-1f, 1f, 1f);

    }
    void Flip1()
    {
        flip = !flip;
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        _camera.transform.localScale = new Vector3(1f, 1f, 1f);
        _nickText.transform.localScale = new Vector3(1f, 1f, 1f);
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


    [Command]
    public void CmdCanvas()
    {
        RpcCanvas();
    }

    [ClientRpc]
    public void RpcCanvas()
    {
        _canvas.SetActive(true);
    }
}


