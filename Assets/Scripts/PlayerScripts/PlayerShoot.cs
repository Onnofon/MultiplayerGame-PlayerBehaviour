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

    public string form = "AR";
    public int ammoAR;
    public int maxAmmoAR = 14;
    public int ammoTank;
    public int maxAmmoTank = 23;
    public int ammoUtility;
    public int maxAmmoUtility = 1;
    public PlayerWeapon weapon;
    public ParticleSystem flareattack;
    public ParticleSystem flareattack2;
    public ParticleSystem flareheavy;
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
    public float timeStamp2;
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
    public Animator animHeavy;
    public Animator animMelee;
    public AudioSource hitsound;

    void Start()
    {
        bulletform = true;
        FireLight.SetActive(false);
        ammoAR = maxAmmoAR;
        ammoTank = maxAmmoTank;
        ammoUtility = maxAmmoUtility;
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
        //if (Input.GetKeyDown("1") && Time.time >= timeStamp2) // Change stats when transforming to attack form
        //{
        //    form = "AR";
        //    weapon.range = 100f;
        //    fireRate = 3;
        //    weapon.damage = 25;
        //    timeStamp2 = Time.time + formDelay;
        //    bulletform = true;
        //}
        //if (Input.GetKeyDown("2") && Time.time >= timeStamp2) // Change stats when transforming to heavy form
        //{
        //    form = "Tank";
        //    weapon.range = 100f;
        //    fireRate = 11;
        //    weapon.damage = 12;
        //    timeStamp2 = Time.time + formDelay;
        //    bulletform = true;
        //}

        //if (Input.GetKeyDown("3") && Time.time >= timeStamp2) // Change stats when transforming to melee form
        //{
        //    form = "Utility";
        //    weapon.range = 2f;
        //    fireRate = 1;
        //    weapon.damage = 50;
        //    timeStamp2 = Time.time + formDelay;
        //    bulletform = false;
        //}

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && ammoTank > 0 && reloading == false && form == "Tank") // Ready to shoot check for heavy form
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            PlayerLight();
            if (bulletform == true)
            {
                ammoTank--;
                shoot.Play();
            }
        }
        else if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && ammoAR > 0 && reloading == false && form == "AR") // Ready to shoot check for attack form
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            PlayerLight();
            if (bulletform == true)
            {
                ammoAR -= 2;
                shoot.Play();
            }
        }
        else if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && ammoUtility > 0 && reloading == false && form == "Utility") // Ready to shoot check for melee form
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            PlayerLight();
        }

        else
        {
            PlayerLightFalse();
        }

        if (GetComponent<Player>().getHealth < health)
        {
            UI.GetComponent<OnHitEffectUI>().FlashIn();
        }

        if (Input.GetKeyDown(KeyCode.R) && ammoAR < maxAmmoAR && bulletform == true && form == "AR")
        {
            StartCoroutine(ReloadingAR());
        }
        if (Input.GetKeyDown(KeyCode.R) && ammoTank < maxAmmoTank && bulletform == true && form == "Tank")
        {
            StartCoroutine(ReloadingTank());
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
                playerScript.pickUpGrenade = false;
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
        if (ammoTank <= 0 && bulletform == true && reloading == false)
        {
            StartCoroutine(ReloadingTank());
            ammoTank = 0;
        }
        if (ammoTank > maxAmmoTank)
        {
            ammoTank = maxAmmoTank;
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
        animHeavy.Play("Shoot");
        animMelee.Play("MeleeAttack");
        flareattack.Play();
        flareattack2.Play();
        flareheavy.Play();
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
    IEnumerator ReloadingTank()
    {
        reload.Play(); // Play sound
        yield return new WaitForSeconds(.1f);
        animHeavy.Play("Reload");  // Play animation
        reloading = true;
        yield return new WaitForSeconds(1.9f);
        ammoTank = maxAmmoTank;
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