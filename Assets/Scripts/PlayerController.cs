using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp = 100;
    public int attack = 10;
    public PlayerMovement playerMovement;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver()
    {
        playerMovement.canMove = false;
    }
    
    public void ChangeHp(int hp)
    {
        currentHp += hp;
        if(currentHp <= 0)
        {
            GameOver();
        }
    }

    public void AttackEnemy(GameObject enemy)
    {
        //Debug.Log("Attack" + enemy.name);
        GameObject projectileObject = Instantiate(projectile, transform.position, projectile.transform.rotation);
        FollowTarget followTarget = projectileObject.GetComponent<FollowTarget>();
        followTarget.SetTarget(enemy);
        //Debug.Log(projectileObject.name);
    }
}
