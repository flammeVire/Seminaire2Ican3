using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManagementPart : MonoBehaviour
{
    [SerializeField] Part[] parts;
    
    private void Start()
    {
        
        foreach(var t in parts)
        {
            foreach(var d in t.Datas)
            {
                d.TilesEventBoolean = d.TileScript.IsEventCallable;
                d.TilesPosition = d.Tiles.position;
                d.TilesRotation = d.Tiles.rotation;
            }

            t.CPAOrigine = (CenterPointAvailable[])t.PAC.CenterPointAvailables.Clone();
        }
    }

    public void ResetPart(int partToSpawn)
    {

        foreach (var d in parts[partToSpawn].Datas)
        {
            d.TileScript.IsEventCallable = d.TilesEventBoolean;
            d.Tiles.position = d.TilesPosition;
            d.Tiles.rotation = d.TilesRotation;
            if (parts[partToSpawn].PAC.CurrentTileSelected != null)
            {
                parts[partToSpawn].PAC.TryToReleaseTile();
            }
            
                
            
        }
        parts[partToSpawn].PAC.CenterPointAvailables = parts[partToSpawn].CPAOrigine;

        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("index : " + (this.GetComponent<LevelManager>().CurrentPart - 1).ToString());
            ResetPart(this.GetComponent<LevelManager>().CurrentPart - 1);
        }
    }
}

[Serializable]
public class Part
{
    public TileData[] Datas;
    public PointAndClick PAC;
    public CenterPointAvailable[] CPAOrigine;
}
[Serializable]
public class TileData
{
    public Transform Tiles;
    [HideInInspector] public Vector3 TilesPosition;
    [HideInInspector] public Quaternion TilesRotation;
    [HideInInspector] public bool TilesEventBoolean;
    public Tile TileScript;
}
