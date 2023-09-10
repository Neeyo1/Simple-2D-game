using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHp = 100.0f;
    public float currentHp = 100.0f;
    public float attack = 10.0f;
    public PlayerMovement playerMovement;
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
    
    public void ChangeHp(float hp)
    {
        currentHp += hp;
        if(currentHp <= 0)
        {
            GameOver();
        }
    }
}
