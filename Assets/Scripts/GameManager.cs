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
    [SerializeField] TMP_Text saved;//vault
    [SerializeField] TMP_Text timesTable;//multiplier

    public Board myBoard;

    public static GameManager instance;

    bool hasGameFinished;

    int points;
    [SerializeField]int temporary;
    [SerializeField]int vault;
    [SerializeField]int multiplier;

    List<float> autoRoundUp = new List<float>();
    bool rounding;

    public void GameRestart()
    {

        PlayerPrefs.SetInt("money", vault);
        PlayerPrefs.SetInt("multi",multiplier);
        PlayerPrefs.SetInt("temp", temporary);

        SceneManager.LoadScene(0);

    }
    public void CleanScore()
    {

        temporary = 0;
        points = 0;
        multiplier = 0;
        vault = 0;

        PlayerPrefs.SetInt("money", vault);
        PlayerPrefs.SetInt("multi", multiplier);
        PlayerPrefs.SetInt("temp", temporary);

    }

    public void GameQuit()
    {

        PlayerPrefs.SetInt("money",vault);
        PlayerPrefs.SetInt("multi", multiplier);
        PlayerPrefs.SetInt("temp", temporary);

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
        temporary = PlayerPrefs.GetInt("temp");

        autoRoundUp.Add(Time.time);

    }

    private void Update()
    {

        if (rounding == false)
        {

            StartCoroutine(Looping(0.5f));
            
        }

        timesTable.text = "X " + multiplier.ToString();
        balance.text = points.ToString();

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

        if (gain != 1)
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

            temporary = points;
            vault += (points * multiplier);
            PlayerPrefs.SetInt("temp", temporary);
            PlayerPrefs.SetInt("money", vault);
            PlayerPrefs.SetInt("multi", multiplier);

            saved.text = vault.ToString();

        }

    }

    IEnumerator Looping(float time)
    {

        rounding = true;

        if (Time.time - autoRoundUp[0] <= 1.0f)
        {

            autoRoundUp[0] = Time.time;

            vault += (temporary * multiplier);

            saved.text = vault.ToString();

        }

        yield return new WaitForSeconds(time);

        rounding = false;

        StartCoroutine(Looping(time));

    }

}
