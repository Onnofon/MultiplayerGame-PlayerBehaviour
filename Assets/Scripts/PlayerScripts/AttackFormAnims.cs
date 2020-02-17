using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFormAnims : PlayerShoot {

public Animator anim;

	void Update () {
        if (Input.GetKeyDown(KeyCode.R) && ammoUtility < maxAmmoUtility && bulletform == true)
        {
            anim.Play("MeleeAttack");
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        reload.Play();
        reloading = true;
        yield return new WaitForSeconds(2);
        ammoUtility = maxAmmoUtility;
        reloading = false;
    }
}
