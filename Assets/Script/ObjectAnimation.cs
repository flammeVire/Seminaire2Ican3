using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimation : MonoBehaviour
{
    public void PlayAnimation(GameObject go)
    {
        go.GetComponent<Animation>().Play();

    }

    public void InvokeAnimationEvent(GameObject go)
    {
        go.GetComponent<EventAnimation>().CallEventAnim();
    }


    public void PlayClipByIndex(string AnimName,Animation anim)
    {
        
        foreach (AnimationState animationState in anim)
        {
            if (animationState.name == "AnimName")
            {
                anim.Play(AnimName);
                return;
            }

        }
        
    }
}
