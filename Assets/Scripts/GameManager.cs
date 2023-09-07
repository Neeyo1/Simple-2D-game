using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject collectable;
    public PlayerMovement playerMovement;
    public int points;
    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI messageUI;
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        points = 0;
        pointsUI.text = "Points: " + points;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            map.SetActive(!(map.activeInHierarchy));
        }
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector2(Random.Range(-playerMovement.boundX, playerMovement.boundX), Random.Range(-playerMovement.boundY, playerMovement.boundY));
        Instantiate(collectable, spawnPosition, collectable.transform.rotation);
        Debug.Log("Spawned");
    }

    public void AddPoint()
    {
        points += 1;
        pointsUI.text = "Points: " + points;
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
}
