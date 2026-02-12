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

            t.CPAOrigine = new CenterPointAvailable[t.PAC.CenterPointAvailables.Length];

            for (int i = 0; i < t.PAC.CenterPointAvailables.Length; i++)
            {
                t.CPAOrigine[i] = t.PAC.CenterPointAvailables[i];
            }
        }
    }

    public void ResetPart(int partToSpawn)
    {

        foreach (var d in parts[partToSpawn].Datas)
        {
            d.TileScript.IsEventCallable = d.TilesEventBoolean;
            d.Tiles.position = d.TilesPosition;
            d.Tiles.rotation = d.TilesRotation;
            d.TileScript.GetComponent<BoxCollider>().enabled = true;
            if (parts[partToSpawn].PAC.CurrentTileSelected != null)
            {
                parts[partToSpawn].PAC.TryToReleaseTile();
            }
        }

        for (int i = 0; i < parts[partToSpawn].PAC.CenterPointAvailables.Length; i++)
        {
             parts[partToSpawn].PAC.CenterPointAvailables[i] = parts[partToSpawn].CPAOrigine[i];
        }
        parts[partToSpawn].OnResetEvent.Invoke();
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
    public UnityEvent OnResetEvent;
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
