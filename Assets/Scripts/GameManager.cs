using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text message;

    public Board myBoard;

    public static GameManager instance;

    bool hasGameFinished;

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

                if(card.myChoice == Choice.GOLD)
                {

                    hasGameFinished = true;
                    message.text = "You Win";

                }
                else if (card.myChoice == Choice.SHARK)
                {

                    hasGameFinished = true;
                    message.text = "You Lose";

                }

            }

        }
        
    }

}
