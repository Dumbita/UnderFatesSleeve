using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    public Image score;
    public Sprite[] earnings = new Sprite[3];

    public bool hasClicked;

    private int gain;

    [SerializeField] int col, row;

    [SerializeField] Sprite gold, fish, shark, unrevealed;

    SpriteRenderer renderer;

    Animator animator;

    public Choice myChoice;

    GameManager reference;

    void Start()
    {

        score.enabled = false;

        reference = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        hasClicked = false;

        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        myChoice = GameManager.instance.myBoard.GetChoice(row,col);

        renderer.sprite = unrevealed;
        
    }

    public void PlayTurn()
    {

        animator.Play("Reveal");

        hasClicked = true;

    }

    public void changeImage()
    {

        score.enabled = true;

        Sprite current = myChoice == Choice.FISH ? fish : myChoice == Choice.GOLD ? gold : shark;

        renderer.sprite = current;

        if (current != (myChoice == Choice.SHARK))
        {

            if (current == (myChoice == Choice.GOLD))
            {

                gain = 50;
                score.sprite = earnings[0];

            }
            else
            {

                gain = 10;
                score.sprite = earnings[1];

            }

        }
        else
        {

            gain = 0;
            score.sprite = earnings[2];

        }

        reference.MessageOut(gain);

    }

}
