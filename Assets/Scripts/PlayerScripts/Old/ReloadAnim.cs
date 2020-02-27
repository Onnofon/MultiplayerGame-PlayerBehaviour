using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAnim : MonoBehaviour {


    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Attacking();
    }

    void Attacking()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.Play("Reload");
        }
    }
}
