using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class MazeGrid : NetworkBehaviour
{
    public List<WallHolder> mazeGrid = new List<WallHolder>();


    public WallHolder get(int i) {
        return mazeGrid[i];
    }
}
