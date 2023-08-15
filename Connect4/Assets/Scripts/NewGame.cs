using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    private bool GOver;

    public GameObject board;
    public GameObject startScreen;
    public GameObject GOScreen;
    public Text GOText;
    public Text buttonText;
    public GameObject clickText;

    void Start()
    {
        GOver = true;
    }
    private bool ai;
    // Start is called before the first frame update
    public void StartGame(int v)
    {
        bool yFirst = true;

        if(v == 0)
        {
            GetComponent<GameTable>().SetMaxDepth(3);
            ai = true;
        }
        if(v == 1)
        {
            GetComponent<GameTable>().SetMaxDepth(5);
            yFirst = false;
            ai = true;
        }
        if(v == 2)
        {
            ai = false;
        }

        board.SetActive(true);
        startScreen.SetActive(false);

        GOver = false;
        GetComponent<ActiveToken>().SetActiveColor(!yFirst);
        GetComponent<ActiveToken>().SpawnNewToken(yFirst);

    }

    public bool IsAiPlaying()
    {
        return ai;
    }

    public void PlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void GameEnd(int who)
    {
        string text;
        buttonText.color = new Color32(239, 47, 46, 255);

        Debug.Log("End Game");
        if (who == 1)
        {
            buttonText.color = new Color32(255, 151, 54, 255);
            if (IsAiPlaying())
            {
                text = "Player wins";
            }
            else
            {
                text = "Yellow wins";
            }
        }
        else if (who == 2)
        {
            if (IsAiPlaying())
            {
                text = "AI wins";
            }
            else
            {
                text = "Red wins";

            }
        }
        else text = "Tie";
        GOver = true;

        GOText.text = text;
        clickText.SetActive(true);

        StartCoroutine(ScreenDelay());

    }

    private IEnumerator ScreenDelay()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        GOScreen.SetActive(true);

    }

    public bool GetGOver()
    {
        return GOver;
    }

    public void Quit()
    {
        Application.Quit();
    }


}
