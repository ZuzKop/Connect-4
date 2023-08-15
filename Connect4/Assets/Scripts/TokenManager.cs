using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    public GameObject gameManager;
    private bool placed = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("manager");
        gameManager.GetComponent<ActiveToken>().SetAsActive(gameObject);
        if (!gameManager.GetComponent<ActiveToken>().GetActiveColor() && gameManager.GetComponent<NewGame>().IsAiPlaying())
        {
            Debug.Log("ai turn");
            StartCoroutine(gameManager.GetComponent<ActiveToken>().AiTurn());

        }
    }

    
    public void SetPlaced(bool plcd)
    {
        placed = plcd;

        if(!gameManager.GetComponent<GameTable>().IsGameOver())
        {
            gameManager.GetComponent<ActiveToken>().SpawnNewToken(!gameManager.GetComponent<ActiveToken>().GetActiveColor());
        }
    }

    public bool GetPlaced()
    {
        return placed;

    }
}
