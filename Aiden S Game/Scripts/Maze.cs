using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Maze : NetworkBehaviour
{

    public int myInt = 7;

    public MazeGrid maze;

    public Image character;

    public Image mySpace;

    public Image Win;

    public GameObject MazeServer;

    public GameObject GameManager;

    public int xVal = 0;

    public int yVal = 0;

    public int maxX;

    public int maxY;

    public void OnPressLeft() {
        if (xVal - 1 >= 0) {
            MazeServer.GetComponent<MazeServer>().decreaseX();
        }
    }
    public void OnPressRight()
    {
        if (xVal + 1  < maxX)
        {
            MazeServer.GetComponent<MazeServer>().increaseX();
        }
    }

    public void OnPressDown()
    {
        if (yVal + 1 < maxY)
        {
            MazeServer.GetComponent<MazeServer>().increaseY();
        }
    }
    public void OnPressUp()
    {
        if (yVal - 1 >= 0)
        {
            MazeServer.GetComponent<MazeServer>().decreaseY();
        }
    }

    public void updateCharacterPos() {
        mySpace = maze.GetComponent<MazeGrid>().get(yVal).get(xVal);

        Vector3 center = mySpace.GetComponent<RectTransform>().position;

        character.transform.position = center;

        if (mySpace == Win)
        {
            GameManager.GetComponent<GameStorage>().TaskComplete(true, myInt);

        }

        else {
            GameManager.GetComponent<GameStorage>().TaskComplete(false, myInt);
        }
    }

    public void Update()
    {
        updateCharacterPos();
    }







}
