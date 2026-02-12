using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMesh : MonoBehaviour
{

    [SerializeField] MeshRenderer render;
    public void ChangeMat(Material newMat)
    {
        render.material = newMat;
    }
}
