using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPos {

    public int row,col;

}

public enum Choice {

    FISH,
    GOLD,
    SHARK

}

public class Board : MonoBehaviour
{

    int gAmount = 2;
    public int sAmount = 3;

    Choice[][] choices;
    List<GridPos> indexList;

    public Board()
    {

        choices = new Choice[5][];
        indexList = new List<GridPos>();

        for (int i = 0; i < 5; i++)
        {

            choices[i] = new Choice[5];

            for (int j = 0; j < choices[i].Length;j++)
            {

                choices[i][j] = Choice.FISH;
                indexList.Add(new GridPos { row = i, col = j });

            }

        }

        AddSharkAndGold();

    }

    GridPos GetRandomFromList()
    {

        GridPos temp;

        int index = Random.Range(0, indexList.Count);
        temp = new GridPos { row = indexList[index].row, col = indexList[index].col };

        indexList.RemoveAt(index);

        return temp;

    }

    void AddSharkAndGold()
    {

        GridPos temp;

        for (int i = 0; i <gAmount; i++)
        {

            temp = GetRandomFromList();

            choices[temp.row][temp.col] = Choice.GOLD;

        }

        for (int i = 0; i < sAmount; i++)
        {

            temp = GetRandomFromList();

            choices[temp.row][temp.col] = Choice.SHARK;

        }

    }

    public Choice GetChoice(int row, int col)
    {

        return choices[row][col];

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
