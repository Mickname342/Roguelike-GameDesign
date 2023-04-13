using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    public Transform firepoint;
    public GameObject fireBullet;
    public GameObject waterBullet;
    public GameObject windBullet;
    public GameObject groundBullet;
    public GameObject bomb;
    GameObject bulletPrefab;
    GameObject bulletPrefab2;
    //public AudioSource shoot;
    public UnityEvent bulletsDown;
    public UnityEvent reload;

    public float bulletforce = 20f;
    float timeLastShot = 0f;
    float delayBetweenShots = 0.24f;
    float timeLastBullet = 0f;
    int bombLimit = 5;
    int bullets = 12;
    bool fire = true;
    bool water = false;

    // Update is called once per frame
    private void Start()
    {
        bulletPrefab = fireBullet;
        bulletPrefab2 = waterBullet;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > timeLastBullet + delayBetweenShots && bombLimit > 0)
        {
            ShootSpecial();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            fire = !fire;
            reload.Invoke();
        }

    }

    void Shoot()
    {
        if (fire)
        {
            //shoot.Play();
            timeLastBullet = Time.time;
            GameObject bullet2 = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(firepoint.right * bulletforce, ForceMode2D.Impulse);
        }
        else
        {
            //shoot.Play();
            timeLastBullet = Time.time;
            GameObject bullet = Instantiate(bulletPrefab2, firepoint.position, firepoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firepoint.right * bulletforce, ForceMode2D.Impulse);
        }
        //bullets--;
        //bulletsDown.Invoke();

    }

    void ShootSpecial()
    {
        //shoot.Play();
        timeLastBullet = Time.time;
        GameObject bullet = Instantiate(bomb, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bombLimit--;
        
    }

    public void reloadBullets()
    {
        bullets = bullets + 6;
        if (bullets >= 12)
        {
            bullets = 12;
        }
        reload.Invoke();
    }

    public void firePrimary()
    {
        bulletPrefab = fireBullet;
    }
    public void waterPrimary()
    {
        bulletPrefab = waterBullet;
    }
    public void windPrimary()
    {
        bulletPrefab = windBullet;
    }
    public void groundPrimary()
    {
        bulletPrefab = groundBullet;
    }
    public void fireSecondary()
    {
        bulletPrefab2 = fireBullet;
    }
    public void waterSecondary()
    {
        bulletPrefab2 = waterBullet;
    }
    public void windSecondary()
    {
        bulletPrefab2 = windBullet;
    }
    public void groundSecondary()
    {
        bulletPrefab2 = groundBullet;
    }

    public void ReloadBombs()
    {
        bombLimit = bombLimit + 2;
        if(bombLimit > 5)
        {
            bombLimit = 5;
        }
    }
}
