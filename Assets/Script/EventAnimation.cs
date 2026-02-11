using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventAnimation : MonoBehaviour
{
    [SerializeField] UnityEvent eventAnim;

    public void CallEventAnim()
    {
        eventAnim.Invoke();
    }
}
