using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float moveSpeed;
    int DataIndex;
    [SerializeField]PlayerData[] playerDatas;

    public void StartMovement(int pointsIndexToStop)
    {
        StartCoroutine(MoveAlongPoints(pointsIndexToStop));
    }

    private IEnumerator MoveAlongPoints(int pointsIndexToStop)
    {
        int p = -1 ;
        if(pointsIndexToStop > 0)
        {
            p = pointsIndexToStop;
        }
        else
        {
            p = playerDatas[DataIndex].points.Length - 1;
        }


        for (int i = 0; i <= p && i < playerDatas[DataIndex].points.Length; i++)
        {
            Vector3 start = transform.position;
            Vector3 end = playerDatas[DataIndex].points[i].position;

            float duration = Vector3.Distance(start, end) / moveSpeed;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                t = Mathf.SmoothStep(0, 1, t);

                transform.position = Vector3.Lerp(start, end, t);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = end;
            if(i == playerDatas[DataIndex].points.Length - 1)
            {
                playerDatas[DataIndex].EndOfMovement.Invoke();
                DataIndex++;
            }
        }

        //movementCoroutine = null;
    }

    [Serializable]
    class PlayerData
    {
        public Transform[] points;
        public UnityEvent EndOfMovement;
    } 
}

