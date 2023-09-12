using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHp = 100.0f;
    public float currentHp = 100.0f;
    public float attack = 10.0f;
    public float maxStorage = 500.0f;
    public float currentStorage = 0.0f;
    public PlayerMovement playerMovement;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver()
    {
        playerMovement.canMove = false;
    }
    
    public void ChangeHp(float hp)
    {
        currentHp += hp;
        gameManager.UpdateHpUI();
        if(currentHp <= 0)
        {
            GameOver();
        }
    }

    public void ChangeStorage(float storage)
    {
        if(currentStorage + storage > maxStorage)
        {
            //TODO take all player can take with his current storage and subtrack it from drop
            gameManager.SendMessage("Could not pick up whole drop");
        }
        else
        {
            currentStorage += storage;
            gameManager.UpdateStorageUI();
        }
    }
}
