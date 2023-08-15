using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInGame : MonoBehaviour
{
    public GameObject gameManager;
    private GameObject activeToken;
    public int column;
    // Start is called before the first frame update

    // Update is called once per frame
    void OnMouseEnter()
    {
        if(!gameManager.GetComponent<NewGame>().GetGOver())
        {
            if(gameManager.GetComponent<ActiveToken>().GetActiveColor() || !gameManager.GetComponent<NewGame>().IsAiPlaying())
            {
                activeToken = gameManager.GetComponent<ActiveToken>().GetActiveToken();
                if(activeToken.GetComponent<TokenManager>().GetPlaced() == false)
                {
                    activeToken.transform.position = new Vector3(transform.position.x,transform.position.y + 2.8f, 0);
                }
            }
        }


    }

    void OnMouseDown()
    {
        if (!gameManager.GetComponent<NewGame>().GetGOver())
        {
            if (gameManager.GetComponent<ActiveToken>().GetActiveColor() || !gameManager.GetComponent<NewGame>().IsAiPlaying())
            {
                if (activeToken.GetComponent<TokenManager>().GetPlaced() == false && gameManager.GetComponent<GameTable>().GetFreeRows(column))
                {
                    activeToken.GetComponent<Rigidbody2D>().simulated = true;
                    gameManager.GetComponent<GameTable>().AddToTable(column, gameManager.GetComponent<ActiveToken>().GetActiveColorInt());
                    activeToken.GetComponent<TokenManager>().SetPlaced(true);

                }
            }

        }



    }
}
