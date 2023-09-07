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
        if (gameObject.CompareTag("Collectable"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                gameManager.SendMessageLog("Collectable collected. +1 point\n");
                gameManager.AddPoint();
                gameManager.SpawnEnemy();
                playerController.hp = 200;
            }
        }
    }
}
