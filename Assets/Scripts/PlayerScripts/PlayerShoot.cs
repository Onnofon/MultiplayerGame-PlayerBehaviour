using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerShoot : NetworkBehaviour
{
    // Use this for initialization
    private const string PLAYER_TAG = "Player";
    public int ammoAR;
    public int maxAmmoAR = 14;
    public PlayerWeapon weapon;
    public ParticleSystem flareattack;
    public ParticleSystem flareattack2;
    public float fireRate = 2;
    public float nextTimeToFire = 0f;
    private GameObject UI;
    public GameObject impactEffect;
    private int health;
    public bool reloading = false;
    public AudioSource shoot;
    public AudioSource reload;
    public GameObject Grenade;
    public Transform throwposition;
    public float grenadeDelay = 8f;
    public float timeStamp;
    public float formDelay = 5f;
    public GameObject FireLight;
    public bool bulletform;
    Player playerScript;
    characterController cScript;
    public int grenades = 1;
    public int grenadesCount = 1;
    public int destroyGrenade = 0;
    public Camera cam;
    public LayerMask mask;
    public Animator animM9;
    public AudioSource hitsound;

    void Start()
    {
        bulletform = true;
        FireLight.SetActive(false);
        ammoAR = maxAmmoAR;
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera reference");
            this.enabled = true;
        }
        UI = GameObject.Find("OnHitEffect");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && ammoAR > 0 && !reloading && bulletform ) // Ready to shoot check for heavy form
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            PlayerLight();
            ammoAR--;
            shoot.Play();
        }


        else
        {
            PlayerLightFalse();
        }

        if (GetComponent<Player>().getHealth < health)
        {
            UI.GetComponent<OnHitEffectUI>().FlashIn();
        }

        if (Input.GetKeyDown(KeyCode.R) && ammoAR < maxAmmoAR && bulletform)
        {
            StartCoroutine(ReloadingAR());
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= timeStamp)
        {
            if (grenadesCount == 1)
            {
                CmdLightGrenade();
                timeStamp = Time.time + grenadeDelay;
                grenades -= 1;                
            }
            else
            if (grenadesCount >= 2)
            {
                CmdLightGrenade();
                timeStamp = Time.time;
                grenades -= 1;
                grenadesCount -= 1;
            }
        }

        // autoreload
        if (ammoAR <= 0 && bulletform == true && reloading == false)
        {
            StartCoroutine(ReloadingAR());
            ammoAR = 0;
        }
        if (ammoAR > maxAmmoAR)
        {
            ammoAR = maxAmmoAR;
        }

        health = GetComponent<Player>().getHealth;
        if (grenades > 2) grenades = 2;
        if (grenades < 0) grenades = 0;
    }

    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffect();
    }

    [ClientRpc]
    void RpcDoShootEffect()
    {
        animM9.Play("Shoot");
        flareattack.Play();
        flareattack2.Play();
    }

    [ClientRpc]
    void RpcLightGrenade()
    {
        var grenade = (GameObject)Instantiate(Grenade, throwposition.position, throwposition.rotation);
        grenade.GetComponent<Rigidbody>().velocity = grenade.transform.forward * 10;
        Destroy(grenade, 8f);
        StartCoroutine(grenadeUI());
    }

    [ClientRpc]
    void RpcPlayerLight()
    {
        FireLight.SetActive(true);
    }

    [ClientRpc]
    void RpcPlayerLightFalse()
    {
        FireLight.SetActive(false);
    }

    [Client]
    void PlayerLight()
    {
        CmdPlayerLight();
    }

    [Client]
    void PlayerLightFalse()
    {
        CmdPlayerLightFalse();
    }

    [Client]
    void Shoot()
    {
        
        CmdOnShoot();
        RaycastHit Hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out Hit, weapon.range, mask))
        {
            Debug.Log("Hit " + Hit.collider.name);
            if (Hit.collider.tag == PLAYER_TAG)
            {
                hitsound.Play();
                CmdPlayerShot(Hit.collider.name, weapon.damage);
            }
            if (bulletform == true)
            {
                GameObject impact = Instantiate(impactEffect, Hit.point, Quaternion.LookRotation(Hit.normal));
                Destroy(impact, 1f);
            }
        }
    }

    [Client]
    void LightGrenade()
    {
        CmdLightGrenade();
    }

    [Command]
    void CmdLightGrenade()
    {
        RpcLightGrenade();
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + " Has been shot.");

        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage, this.gameObject.name);
    }

    [Command]
    void CmdPlayerLight()
    {
        RpcPlayerLight();
    }

    [Command]
    void CmdPlayerLightFalse()
    {
        RpcPlayerLightFalse();
    }

    IEnumerator ReloadingAR()
    {
        reload.Play(); // Play sound 
        yield return new WaitForSeconds(0.1f);
        animM9.Play("Reload"); // Play animation
        reloading = true;
        yield return new WaitForSeconds(1.9f);
        ammoAR = maxAmmoAR;
        reloading = false;
    }
    IEnumerator grenadeUI()
    {
        yield return new WaitForSeconds(8f);
        if (grenades == 0)
        {
            grenades += 1;        
        }
    }
}