using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimation : MonoBehaviour
{
    public void PlayAnimation(GameObject go)
    {
        go.GetComponent<Animation>().Play();
        
    } 
}
