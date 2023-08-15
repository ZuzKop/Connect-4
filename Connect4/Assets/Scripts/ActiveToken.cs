using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToken : MonoBehaviour
{
    public Transform Yprefab;
    public Transform Rprefab;

    private GameObject activeToken;
    private bool activeColor;

    public GameObject[] columns;


    public void SetAsActive(GameObject token)
    {
        activeToken = token;
        activeColor = !activeColor;
    }

    public void SetActiveColor(bool c)
    {
        activeColor = c;
    }

    public GameObject GetActiveToken()
    {
        return activeToken;
    }
    
    public bool GetActiveColor()
    {
        return activeColor;
    }

    public int GetActiveColorInt()
    {
        if (activeColor) return 1;
        else
        return 2;
    }

    public void SpawnNewToken(bool color)
    {
        if(color)
        {
            Instantiate(Yprefab);
        }
        else
        {
            Instantiate(Rprefab);

        }
    }

    public IEnumerator AiTurn()
    {
        yield return new WaitForSeconds(0.5f);
        int n = GetComponent<GameTable>().BestPossibleMove();

        activeToken.transform.position = new Vector3(columns[n].transform.position.x, columns[n].transform.position.y + 2.8f, 0);
        
        activeToken.GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<GameTable>().AddToTable(n, GetActiveColorInt());
        yield return new WaitForSeconds(0.5f);
        activeToken.GetComponent<TokenManager>().SetPlaced(true);

        
    }
}
