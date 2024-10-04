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
    [SerializeField] TMP_Text timesTable;

    public Board myBoard;

    public static GameManager instance;

    bool hasGameFinished;

    int points;
    [SerializeField]int vault;
    [SerializeField]int multiplier;

    public void GameRestart()
    {

        PlayerPrefs.SetInt("money", vault);
        PlayerPrefs.SetInt("multi",multiplier);

        SceneManager.LoadScene(0);

    } 

    public void GameQuit()
    {

        PlayerPrefs.SetInt("money",vault);
        PlayerPrefs.SetInt("multi", multiplier);

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

    private void Start()
    {

        vault = PlayerPrefs.GetInt("money");
        multiplier = PlayerPrefs.GetInt("multi");

    }

    private void Update()
    {

        timesTable.text = "X " + multiplier.ToString();

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

                multiplier = 0;

            }
            else
            {

                if (multiplier != 0)
                {

                    multiplier = multiplier * 2;

                }
                else
                {

                    multiplier = 1;

                }

            }

            vault += (points * multiplier);
            PlayerPrefs.SetInt("money", vault);
            PlayerPrefs.SetInt("multi", multiplier);
            saved.text = vault.ToString();

        }

    }

}
