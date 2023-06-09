using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public int hp = 10;
    public int maxHp = 10;
    public Transform hpBar;
    //public AudioSource roar;
    public GameObject rechargePrefab;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    int random;
    public GameObject TheEnemy;
    public GameObject enemyBullet;
    EnemySpawner enemySpawner;
    EnemySpawner enemySpawner2;
    public GameObject Spawner;
    public GameObject Spawner2;

    [Header("Shooting to Player:")]
    public Transform Target;
    private Vector2 direction;
    public float rotationSpeed = 1;

    private void Start()
    {
        random = Random.Range(0, 10);
        hpBar.localScale = new Vector2(maxHp / 5, 1);
        enemySpawner = Spawner.GetComponent<EnemySpawner>();
        enemySpawner2 = Spawner2.GetComponent<EnemySpawner>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("DoubleDamage"))
            {
                hp = hp - 4;
                hpBar.localScale = new Vector2(hpBar.localScale.x - 1 / 5f * 4f, 1);
            }
            else if (collision.gameObject.CompareTag("Bomb"))
            {
                hp = hp - 0;
            }
            else
            {
                hp--;
                hpBar.localScale = new Vector2(hpBar.localScale.x - 1 / 5f, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //activate.Invoke();
        //anim.SetBool("Active", true);
        /*if (other.CompareTag("Player"))
        {
            float angle = Mathf.Atan2( playerTransform.position.y - transform.position.y, playerTransform.position.x - transform.position.x) * Mathf.Rad2Deg;
            print(angle);
            GameObject bulletShot = Instantiate(enemyBullet,transform.position, Quaternion.Euler(0,0,angle));
            Rigidbody2D rb = bulletShot.GetComponent<Rigidbody2D>();
            float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * 10;
            float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * 10;
            Vector3 forceapplied = new Vector3(xcomponent, ycomponent, 0);
            print(forceapplied);
            rb.AddForce(forceapplied, ForceMode2D.Impulse);
        }*/

    }

    private void Update()
    {
        if (hp <= 0)
        {
            enemySpawner.DefeatedEnemy();
            enemySpawner2.DefeatedEnemy();
            if (random <= 6)
            {
                Instantiate(rechargePrefab, transform.position, Quaternion.identity);
            }
            Destroy(TheEnemy);

        }
    }
}
