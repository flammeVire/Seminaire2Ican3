using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LevelManager : MonoBehaviour
{
    [SerializeField] UnityEvent StartEvents;
    public int CurrentPart;

    [Header("Camera")]
    [SerializeField] GameObject Camera;
    [SerializeField] EndOfPart[] parts;
    [SerializeField] float Speed;

    private void Start()
    {
        StartEvents.Invoke();
    }

    public void ChangeIndex(int newIndex)
    {
        CurrentPart = newIndex;
    }
    public void StartCameraMovement()
    {
        StartCoroutine(MoveAlongPoints(parts[CurrentPart].CameraPoint));
        parts[CurrentPart].EOPEvent.Invoke();
    }

    private IEnumerator MoveAlongPoints(Transform[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 start = transform.position;
            Vector3 end = points[i].position;

            float duration = Vector3.Distance(start, end) / Speed;
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
        }
    }
}
[Serializable]
public class EndOfPart
{
    public UnityEvent EOPEvent;
    public Transform[] CameraPoint;
}
