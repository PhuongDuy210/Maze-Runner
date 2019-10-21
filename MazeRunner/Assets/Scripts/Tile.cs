using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 pos;
    public GameObject NWall, EWall, WWall, SWall, floor;
    public bool visited = false;
    public Material ogMat;
    public Tile previousTile;

    public void DestroyWall(GameObject wall)
    {
        if(wall != null)
        {
            GameObject.Destroy(wall);
        }
    }
}
