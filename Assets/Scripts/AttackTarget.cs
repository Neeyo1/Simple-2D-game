using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AttackCoroutine(GameObject target)
    {
        while(target != null)
        {
            if(target.CompareTag("Enemy"))
            {
                float distanceToTarget = Vector2.Distance(target.transform.position, new Vector2(transform.position.x, transform.position.y));
                Debug.Log("Pew");
                Debug.Log(distanceToTarget);

                if(distanceToTarget <= 10.0f)
                {
                    GameObject projectileObject = Instantiate(projectile, transform.position, projectile.transform.rotation);
                    FollowTarget followTarget = projectileObject.GetComponent<FollowTarget>();
                    followTarget.SetTargetAndSource(target, gameObject);
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    Debug.Log("Stop");
                    target.GetComponent<EnemyController>().isBeingAttacked = false;
                    break;
                }
            }
            else if(target.CompareTag("Player"))
            {
                float distanceToTarget = Vector2.Distance(target.transform.position, new Vector2(transform.position.x, transform.position.y));
                if(distanceToTarget <= 5.0f)
                {
                    GameObject projectileObject = Instantiate(projectile, transform.position, projectile.transform.rotation);
                    FollowTarget followTarget = projectileObject.GetComponent<FollowTarget>();
                    followTarget.SetTargetAndSource(target, gameObject);
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    gameObject.GetComponent<EnemyController>().isAttacking = false;
                    break;
                }
            }
        }
    }

    public void Attack(GameObject target)
    {
        if(target.CompareTag("Enemy"))
        {
            if(target.GetComponent<EnemyController>().isBeingAttacked == false)
            {
                target.GetComponent<EnemyController>().isBeingAttacked = true;
                StartCoroutine(AttackCoroutine(target));
            }
        }
        else if(target.CompareTag("Player"))
        {
            if(gameObject.GetComponent<EnemyController>().isAttacking == false)
            {
                gameObject.GetComponent<EnemyController>().isAttacking = true;
                StartCoroutine(AttackCoroutine(target));
            }
        }
    }
}
