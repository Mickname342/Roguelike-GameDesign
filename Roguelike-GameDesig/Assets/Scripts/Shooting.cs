using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Transform firepoint;
    public GameObject fireBullet;
    public GameObject bigBullet;
    public ParticleSystem shootingParticles;
    /*public GameObject waterBullet;
    public GameObject windBullet;
    public GameObject groundBullet;*/
    public GameObject bomb;
    public GameObject upgradeMenu;
    GameObject bulletPrefab;
    GameObject bulletPrefab2;
    
    public AudioSource shoot;
    public AudioSource crossShootAudio;
    public GameObject weaponChanger;
    public Text bombNumber;
    public Text bombTotalNumber;
    public Text bulletNumber;
    public Text bulletTotalNumber;
    public Image gun1;
    public Image gun2;

    public Transform rechargeBar;
    public SpriteRenderer rechargeSprite;
    public Animator rechargeAnim;


    public float bulletforce = 40f;
    float timeLastShot = 0f;
    float delayBetweenShots = 0.07f;
    float timeLastBullet = 0f;
    int currentBombs = 5;
    int bombLimit = 5;
    int currentBullets = 1;
    float initialAngle = 0f;
    bool fire = true;
    bool water = false;
    
    int bullets = 60;
    int totalBullets = 60;
    int bulletsForUpgrade = 0;
    int bulletsForCrossUpgrade = 0;
    public bool thirdBulletUpgrade = false;
    float rechargeTime = 2f;
    float rechargeStart = 0f;
    bool recharging = false;
    public Transform triangle;

    public bool crossShooting = false;
    float crossLastShot = 0f;
    float delayBetweenCross = 1.5f;

    // Update is called once per frame
    private void Start()
    {
        bulletPrefab = fireBullet;
        //bulletPrefab2 = waterBullet;
    }
    void Update()
    {
        bombNumber.text = currentBombs.ToString();
        bombTotalNumber.text = bombLimit.ToString();
        bulletNumber.text = bullets.ToString();
        bulletTotalNumber.text = totalBullets.ToString();
        if (Input.GetKeyDown(KeyCode.Mouse0) && bullets > 0)
        {
            Shoot();
            shootingParticles.Play();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > timeLastBullet + delayBetweenShots && currentBombs > 0)
        {
            ShootSpecial();
        }
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            MoreBombs();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            MoreBullets();
        }*/

        if (fire == true)
        {
            weaponChanger.SetActive(true);
        }
        if (fire == false)
        {
            weaponChanger.SetActive(false);
        }

        if (bullets <= 0)
        {
            rechargeAnim.SetBool("recharging", true);
            rechargeSprite.enabled = true;
        }

        if (crossShooting == true && Time.time > delayBetweenCross + crossLastShot)
        {
            crossLastShot = Time.time;
            crossShootAudio.Play();
            if(bulletsForCrossUpgrade < 2)
            {
                for (int i = 0; i < 8; i++)
                {
                    GameObject bullet2 = Instantiate(bulletPrefab, triangle.position, firepoint.rotation * Quaternion.Euler(0, 0, 270 + 45 * i));
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    float horizontalForce = Mathf.Cos(0.75f * (i - 2));
                    float verticalForce = Mathf.Sin(0.75f * (i - 2));
                    rb2.AddForce(firepoint.right * bulletforce * horizontalForce, ForceMode2D.Impulse);
                    rb2.AddForce(firepoint.up * bulletforce * verticalForce, ForceMode2D.Impulse);
                }
            }

            if (thirdBulletUpgrade)
            {
                bulletsForCrossUpgrade++;
                if(bulletsForCrossUpgrade >= 3)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        GameObject bullet2 = Instantiate(bigBullet, triangle.position, firepoint.rotation * Quaternion.Euler(0, 0, 270 + 45 * i));
                        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                        float horizontalForce = Mathf.Cos(0.75f * (i - 2));
                        float verticalForce = Mathf.Sin(0.75f * (i - 2));
                        rb2.AddForce(firepoint.right * bulletforce * horizontalForce, ForceMode2D.Impulse);
                        rb2.AddForce(firepoint.up * bulletforce * verticalForce, ForceMode2D.Impulse);
                        bulletsForCrossUpgrade = 0;
                    }
                }
            }
            
        }
    }

    void Shoot()
    {
        bullets--;
        shoot.Play();
        if (fire && bulletsForUpgrade<2)
        {
            timeLastShot = Time.time;
            for(int i = 0; i < currentBullets; i++)
            {
                GameObject bullet2 = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                float horizontalForce = Mathf.Cos(initialAngle * (i-2));
                float verticalForce = Mathf.Sin(initialAngle * (i - 2));
                rb2.AddForce(firepoint.right * bulletforce * horizontalForce, ForceMode2D.Impulse);
                rb2.AddForce(firepoint.up * bulletforce * verticalForce, ForceMode2D.Impulse);
            }
            
        }
        if (thirdBulletUpgrade)
        {
            bulletsForUpgrade++;
            if (bulletsForUpgrade >= 3)
            {
                timeLastShot = Time.time;
                for (int i = 0; i < currentBullets; i++)
                {
                    GameObject bullet2 = Instantiate(bigBullet, firepoint.position, firepoint.rotation);
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    float horizontalForce = Mathf.Cos(initialAngle * (i - 2));
                    float verticalForce = Mathf.Sin(initialAngle * (i - 2));
                    rb2.AddForce(firepoint.right * bulletforce * horizontalForce, ForceMode2D.Impulse);
                    rb2.AddForce(firepoint.up * bulletforce * verticalForce, ForceMode2D.Impulse);
                    bulletsForUpgrade = 0;
                }
            }
        }
        /*else
        {
            //shoot.Play();
            timeLastShot = Time.time;
            for (int i = 0; i < currentBullets; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab2, firepoint.position, firepoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                float horizontalForce = Mathf.Cos(initialAngle * (i - 2));
                float verticalForce = Mathf.Sin(initialAngle * (i - 2));
                if (bulletPrefab2 == windBullet)
                {
                    rb.AddForce(firepoint.right * bulletforce * horizontalForce * 999, ForceMode2D.Impulse);
                    rb.AddForce(firepoint.up * bulletforce * verticalForce * 999, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(firepoint.right * bulletforce * horizontalForce, ForceMode2D.Impulse);
                    rb.AddForce(firepoint.up * bulletforce * verticalForce, ForceMode2D.Impulse);
                }
            }
        }*/
        //bullets--;
        //bulletsDown.Invoke();

    }

    void ShootSpecial()
    {
        //shoot.Play();
        timeLastBullet = Time.time;
        GameObject bullet = Instantiate(bomb, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        currentBombs--;
        print("bombs Left: " + currentBombs);
        
    }

    public void Recharge()
    {
        bullets = totalBullets;
        rechargeAnim.SetBool("recharging", false);
        rechargeSprite.enabled = false;
    }

    public void ReloadBombs()
    {
        currentBombs = currentBombs + 2;
        if(currentBombs > bombLimit)
        {
            currentBombs = bombLimit;
        }
    }

    public void MoreBombs()
    {
        bombLimit = bombLimit +2;
    }

    public void MoreTotalBullets()
    {
        totalBullets = totalBullets + 20;
    }

    public void MoreBullets()
    {
        upgradeMenu.SetActive(false);
        currentBullets = currentBullets + 1;
        initialAngle = initialAngle + 0.1f;
    }

    public void MoreSpeed()
    {
        bulletforce = bulletforce + 10f;
    }

    public void IsWind()
    {
        bulletforce *= 999;
    }

    public void CrossShooting()
    {
        crossShooting = true;
    }

    public void BigBulletActive()
    {
        thirdBulletUpgrade = true;
    }
}
