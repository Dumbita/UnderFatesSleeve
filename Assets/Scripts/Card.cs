using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{

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

        Sprite current = myChoice == Choice.FISH ? fish : myChoice == Choice.GOLD ? gold : shark;

        renderer.sprite = current;

        if (current != (myChoice == Choice.SHARK))
        {

            if (current == (myChoice == Choice.GOLD))
            {

                gain = 50;

            }
            else
            {

                gain = 10;

            }

        }
        else
        {

            gain = 0;

        }

        reference.MessageOut(gain);

    }

}
