using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField] Transform CenterTilesTarget; 

    public Quaternion WantedRotation => CenterTilesTarget.rotation;
    public bool IsCorrectRotation = false;

    public Vector3 WantedPosition => CenterTilesTarget.position;
    public bool IsCorrectPosition = false;

    public bool IsEventCallable = true;
    public UnityEvent OnCorrectTransform;

    public Condition[] conditions;

    [Header("TileParameter")]
    public int TileID = -1;
    public int IncrementRotation = 90;


    public bool CheckConditions()
    {
        bool good = true;
        if (conditions.Length > 0)
        {
            Debug.Log("if");
            foreach (var c in conditions)
            {
                Debug.Log(c.ConditionIsGood);
                c.ConditionIsGood = !c.PreviousTile.IsEventCallable;
                Debug.Log(c.ConditionIsGood);
                if (!c.ConditionIsGood)
                {
                    good = false;
                    break;
                }
            }
        }
        return good;
    }

    public void CheckPosition()
    {
        if (CenterTilesTarget != null)
        {

            if (transform.position == WantedPosition)
            {
                IsCorrectPosition = true;
            }
            else
            {
                Debug.Log(transform.position + " != " + WantedPosition);
                IsCorrectPosition = false;
            }
                Debug.Log(transform.rotation);
            if (Mathf.Abs(Quaternion.Dot(transform.rotation, WantedRotation)) > 0.9999f)
            {
                IsCorrectRotation = true;
            }
            else
            {
                IsCorrectRotation = false;
            }

            if (IsCorrectPosition && IsCorrectRotation)
            {
                IsEventCallable = CheckConditions();
                Debug.Log(IsEventCallable);
                if (IsEventCallable)
                {
                    Debug.Log("Invoke");
                    OnCorrectTransform.Invoke();
                    IsEventCallable = false;
                }
                else
                {
                    StartCoroutine(WaitForConditions());
                }
            }
        }
    }
    
    IEnumerator WaitForConditions()
    {
        yield return new WaitUntil(() => CheckConditions());
        CheckPosition();
    }
}

    [Serializable]
    public class Condition
    {

        public Tile PreviousTile;
        public bool ConditionIsGood;
    }

