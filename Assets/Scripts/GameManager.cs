using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject collectable;
    public GameObject enemy;
    public PlayerMovement playerMovement;
    private PlayerController playerController;
    public int points;
    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI messageUI;
    public TextMeshProUGUI hpUI;
    public TextMeshProUGUI storageUI;
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnCollectable();
        StartCoroutine(SpawnEnemyCoroutine());
        points = 0;
        UpdateHpUI();
        UpdateStorageUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            map.SetActive(!(map.activeInHierarchy));
        }
    }

    public void SpawnCollectable()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-playerMovement.boundX, playerMovement.boundX), Random.Range(-playerMovement.boundY, playerMovement.boundY));
        Instantiate(collectable, spawnPosition, collectable.transform.rotation);
        //Debug.Log("Collectable spawned");
    }

    public void AddPoint(int pointsToAdd = 1)
    {
        points += pointsToAdd;
        pointsUI.text = "Points: " + points;
    }

    public void UpdateHpUI()
    {
        hpUI.text = "HP: " + playerController.currentHp + "/" + playerController.maxHp;
    }

    public void UpdateStorageUI()
    {
        storageUI.text = "Storage: " + playerController.currentStorage + "/" + playerController.maxStorage;
    }

    IEnumerator SendMessageLogCoroutine(string message)
    {
        messageUI.text += message;
        yield return new WaitForSeconds(3);
        if(messageUI.text.Contains(message))
        {
            int startPosition = messageUI.text.IndexOf(message);
            int messageLength = message.Length;
            if(startPosition > 0)
            {
                string stringTemp = "";
                stringTemp += messageUI.text.Substring(0, startPosition);
                stringTemp += messageUI.text.Substring(startPosition + messageLength);
                //Debug.Log(stringTemp);
                messageUI.text = stringTemp;
            }
            else
            {
                string stringTemp = "";
                stringTemp += messageUI.text.Substring(startPosition + messageLength);
                //Debug.Log(stringTemp);
                messageUI.text = stringTemp;
            }
            //Debug.Log(messageUI.text.IndexOf(message));
        }
    }

    public void SendMessageLog(string message)
    {
        StartCoroutine(SendMessageLogCoroutine(message));
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            Vector2 spawnPosition = new Vector2(Random.Range(-playerMovement.boundX, playerMovement.boundX), Random.Range(-playerMovement.boundY, playerMovement.boundY));
            float distance = Vector2.Distance(player.transform.position,spawnPosition);
            while(distance <= 10.0f)
            {
                spawnPosition = new Vector2(Random.Range(-playerMovement.boundX, playerMovement.boundX), Random.Range(-playerMovement.boundY, playerMovement.boundY));
                distance = Vector2.Distance(player.transform.position,spawnPosition);
            }
            Instantiate(enemy, spawnPosition, enemy.transform.rotation);
            //Debug.Log("Enemy spawned");
            //Debug.Log(distance);
        }
    }
}
