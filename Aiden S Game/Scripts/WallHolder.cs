using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class WallHolder : NetworkBehaviour
{
    public List<Image> _Walls = new List<Image>();

    public Image get(int i)
    {
        return _Walls[i];
    }
}
