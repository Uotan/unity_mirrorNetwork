using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forGun : MonoBehaviour
{
    public bool isReload = false;
    public Animator _animatorController;

    void Start()
    {
        _animatorController = GetComponent<Animator>();
        
    }
    private void Update()
    {

    }
    public void Reload()
    {
        if (isReload==false&&this.gameObject.activeInHierarchy==true)
        {
            isReload = true;
            _animatorController.Play("reload");
            Invoke("Back", 1f);
        }
        
    }

    void Back()
    {
        isReload = false;
        _animatorController.Play("idle");
    }
}
