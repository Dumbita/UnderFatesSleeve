using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text message;

    [SerializeField] TMP_Text balance;
    [SerializeField] TMP_Text saved;

    public Board myBoard;

    public static GameManager instance;

    bool hasGameFinished;

    int points;
    int vault;

    public void GameRestart()
    {

        SceneManager.LoadScene(0);

    } 

    public void GameQuit()
    {

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#endif

        Application.Quit();

    }

    private void Awake()
    {

        points = 0;

        if (instance == null)
        {

            instance = this;

        }
        else
        {

            Destroy(gameObject);

        }

        message.text = "Play Next Turn";
        hasGameFinished = false;

        myBoard =  new Board();

    }

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {

            if (hasGameFinished)
            {

                return;

            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x,mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (!hit.collider)
            {

                return;

            }

            if (hit.collider.CompareTag("Card"))
            {

                Card card = hit.collider.GetComponent<Card>();

                if (card.hasClicked)
                {

                    return;

                }

                card.PlayTurn();

                if (card.myChoice != Choice.FISH)
                {

                    if (card.myChoice == Choice.GOLD)
                    {

                        message.text = "You Win";

                    }
                    else if (card.myChoice == Choice.SHARK)
                    {

                        points = 0;

                        message.text = "You Lose";

                    }

                    
                    hasGameFinished = true;
                    

                }

            }

        }
        
    }

    public void MessageOut(int gain)
    {

        points += gain;

        balance.text = points.ToString();

        if (gain != 10)
        {

            if (gain == 0)
            {

                points = 0;

            }

            vault += points;
            saved.text = vault.ToString();

        }

    }

}
