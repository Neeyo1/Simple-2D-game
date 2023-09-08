using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private float latestDirectionChangeTime;
    private float directionChangeTime = 3f;
    public float speed = 2.0f;
    public float maxXp = 100.0f;
    public float currentHp;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    public GameObject ui;
    public GameObject hpBar;
    public float hpBarOriginalSize;
    public float hpBarOffset = 0.0f;


    void Start()
    {
        currentHp = maxXp;
        hpBarOriginalSize = hpBar.transform.localScale.x;
        player = GameObject.Find("Player");
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }

    void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
        
        float distanceToPlayer = Vector2.Distance(player.transform.position, new Vector2(transform.position.x, transform.position.y));
        if(distanceToPlayer > 5f)
        {
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
                                             transform.position.y + (movementPerSecond.y * Time.deltaTime));
        }
        else
        {
            Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

        }
    }

    void calcuateNewMovementVector()
    {
    //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * speed;
    }

    public void ChangeHp(float hp)
    {
        currentHp += hp;
        ChangeHpBar();
        if(currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeHpBar()
    {
        hpBar.transform.localScale = new Vector2(currentHp / maxXp * hpBarOriginalSize, 0.25f);
        Debug.Log(hpBarOriginalSize - (currentHp / maxXp * hpBarOriginalSize));
        Debug.Log((hpBarOriginalSize - (currentHp / maxXp * hpBarOriginalSize))/2.0f);
        hpBar.transform.position -= new Vector3((hpBarOriginalSize - (currentHp / maxXp * hpBarOriginalSize))/2.0f - hpBarOffset, 0, 0);
        hpBarOffset = (hpBarOriginalSize - (currentHp / maxXp * hpBarOriginalSize))/2.0f;
    }
}
