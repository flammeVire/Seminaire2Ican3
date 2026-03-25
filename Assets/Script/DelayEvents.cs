using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvents : MonoBehaviour
{
    [SerializeField] float Delay;
    [SerializeField] UnityEvent EndOfDelayEvents;

    public void LaunchDelay()
    {
        if (EndOfDelayEvents != null)
        {
            StartCoroutine(WaitingRoutine());
        }
    }
    IEnumerator WaitingRoutine()
    {
        yield return new WaitForSeconds(Delay);
        EndOfDelayEvents.Invoke();
    }
}
