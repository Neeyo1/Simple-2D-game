using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private GameObject gameManagerObject;
    private GameManager gameManager;
    private GameObject playerDataObject;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        playerDataObject = GameObject.Find("Player");
        playerController = playerDataObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(gameObject.CompareTag("Collectable"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                gameManager.SendMessageLog("Collectable collected. +1 point\n");
                gameManager.AddPoint();
                gameManager.SpawnCollectable();
            }
        }
        else if(gameObject.CompareTag("Drop"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                float storageLeft = playerController.maxStorage - playerController.currentStorage;
                if(storageLeft >= 250.0f)
                {
                    Destroy(gameObject);
                    gameManager.SendMessageLog("Drop collected. +250 storage\n");
                    playerController.ChangeStorage(250.0f);
                }
                else
                {
                    gameManager.SendMessageLog("Could not pick up drop, no enough empty space\n");
                }
            }
        }
        else if(gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                playerController.ChangeHp(-10.0f);
            }
        }
        else if(gameObject.CompareTag("Projectile"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                if(other.gameObject != gameObject.GetComponent<FollowTarget>().source)
                {
                    Destroy(gameObject);
                    playerController.ChangeHp(-2.0f);
                }
            }
            else if(other.gameObject.CompareTag("Enemy"))
            {
                if(other.gameObject.tag != gameObject.GetComponent<FollowTarget>().source.tag)
                {
                    if(other.gameObject == gameObject.GetComponent<FollowTarget>().target)
                    {
                        Destroy(gameObject);
                        other.gameObject.GetComponent<EnemyController>().ChangeHp(-10.0f);
                    }
                }
            }
        }
    }
}
