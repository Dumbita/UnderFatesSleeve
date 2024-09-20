using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public bool hasClicked;

    [SerializeField] int col, row;

    [SerializeField] Sprite gold, fish, shark, unrevealed;

    SpriteRenderer renderer;

    Animator animator;

    public Choice myChoice;

    void Start()
    {

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

    }

}
