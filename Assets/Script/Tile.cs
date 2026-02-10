using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public Vector3 WantedRotation;
    public bool IsCorrectRotation = false;

    public Vector3 WantedPosition;
    public bool IsCorrectPosition = false;

    public bool IsEventCallable = true;
    public UnityEvent OnCorrectTransform;

    [Header("TileParameter")]
    public int TileID = -1;
    public int IncrementRotation = 90;


    public void CheckPosition()
    {
        if(transform.position == WantedPosition)
        {
            IsCorrectPosition = true;
        }
        else
        {
            Debug.Log(transform.position + " != " + WantedPosition);
            IsCorrectPosition = false;
        }

        if(transform.rotation.eulerAngles == WantedRotation)
        {
            Debug.Log(transform.rotation.eulerAngles + " != " + WantedRotation);
            IsCorrectRotation = true;
        }
        else
        {
            IsCorrectRotation = false;
        }

        if(IsCorrectPosition && IsCorrectRotation)
        {
            Debug.Log("Invoke");
            OnCorrectTransform.Invoke();
            IsEventCallable = false;
        }
    }
}