using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManagementPart : MonoBehaviour
{
    [SerializeField] Part[] parts;
    Part[] originePart;
    private void Start()
    {
        
    }

    public void ResetPart(int partToSpawn)
    {
        foreach(var tile in parts)
        {

        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ResetPart(this.GetComponent<LevelManager>().CurrentPart - 1);
        }
    }
}

[Serializable]
public class Part
{
    public TileData[] Datas;
}
[Serializable]
public class TileData
{
    public Transform Tiles;
    Vector3 TilesPosition => Tiles.position;
    Quaternion TilesRotation => Tiles.rotation;
    public Tile TileScript;
}
