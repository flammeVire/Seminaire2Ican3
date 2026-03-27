using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTileMat : MonoBehaviour
{
   [SerializeField] Tile tile;
    [SerializeField] Material newMat;
    private void Start()
    {
        StartCoroutine(WaitToChangeMat());
    }

    IEnumerator WaitToChangeMat()
    {
        yield return new WaitUntil(() => tile.IsCorrectPosition);
        yield return new WaitUntil(() => tile.IsCorrectRotation);
        tile.GetComponent<TileMesh>().ChangeMat(newMat);
        tile.GetComponent<Collider>().enabled = false;
    }
}
