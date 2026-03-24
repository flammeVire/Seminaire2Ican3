using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunction : MonoBehaviour
{
    public Camera MainCam;
    public void Zoom(float SizeTarget)
    {
        float Size = MainCam.orthographicSize;
        Debug.Log("Zoom from " + Size + " to " +  SizeTarget);
        if(SizeTarget > Size)
        {
            StartCoroutine(ZoomIn(SizeTarget));
        }
        else if(SizeTarget < Size)
        {
            StartCoroutine(ZoomOut(SizeTarget));
        }
    }

    IEnumerator ZoomIn(float SizeTarget)
    {
        float Size = MainCam.orthographicSize;

        while (SizeTarget < Size)
        {
            MainCam.orthographicSize += 0.1f;
            yield return null;
        }
    }

    IEnumerator ZoomOut(float SizeTarget) 
    {
        float Size = MainCam.orthographicSize;
        while (SizeTarget > Size)
        {
            MainCam.orthographicSize -= 0.1f;
            yield return null;
        }
    }
}
