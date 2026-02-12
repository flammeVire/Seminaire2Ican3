using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PointAndClick : MonoBehaviour
{
    public GameObject CurrentTileSelected;
    [SerializeField] LayerMask TileLayer;
    [SerializeField] LayerMask PlayerLayer;


    [SerializeField] GameObject[] CenterPoint;
    [SerializeField] public CenterPointAvailable[] CenterPointAvailables;

    [Header("FeedBack")]
    [SerializeField] float AddScaleSelection;
    GameObject CursoredTile;
    [SerializeField] Material[] mats;
    /*
     a faire:
            
            ajouter juice rota (voir la rota)
            clic droit pour faire déplacer le joueur
            ajout fin

     */
    void Update()
    {
        ManageInput();
       // ShowSelectable();
    }
    
    void ManageInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(CurrentTileSelected == null)
            {
                TryToGetTile();
            }
            else
            {
                TryToReleaseTile();
            }
        }
        if (CurrentTileSelected != null)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {

                RotateTile(CurrentTileSelected.GetComponent<Tile>().IncrementRotation);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                RotateTile(-CurrentTileSelected.GetComponent<Tile>().IncrementRotation);
            }
        }

    }





    void TryToGetTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 100f))
        {
            Debug.Log("hit something " + hit.transform.name);
            CurrentTileSelected = hit.transform.gameObject;
            StartCoroutine(ChangeScale(AddScaleSelection));
            ReleaseCPA();
        }
        else
        {
            Debug.Log("Not Hit");
        }
    }

    void ReleaseCPA()
    {
        for (int i = 0; i < CenterPointAvailables.Length; i++)
        {
            if (CenterPointAvailables[i].CenterPoint == CurrentTileSelected.transform.position)
            {
                CenterPointAvailables[i].Available = true;

                break;
            }
        }
    }

    public void TryToReleaseTile()
    {
        Vector3 point = GetClosestTile(Utility.GetMousePos());

        for (int i = 0; i < CenterPointAvailables.Length; i++)
        {
            if (CenterPointAvailables[i].CenterPoint == point)
            {
                if (CenterPointAvailables[i].Available)
                {
                    CurrentTileSelected.transform.position = point;
                    CenterPointAvailables[i].Available = false;
                }
                break;
            }
        }

        StartCoroutine(ChangeScale(-AddScaleSelection));
        CurrentTileSelected.GetComponent<Tile>().CheckPosition();
        ResetCPA(CurrentTileSelected.transform.position);
        CurrentTileSelected = null;
    }

    public void ResetCPA(Vector3 point)
    {
        for (int i = 0; i < CenterPointAvailables.Length; i++)
        {
            if (CenterPointAvailables[i].CenterPoint == point)
            {
                if (CenterPointAvailables[i].Available)
                {
                    CenterPointAvailables[i].Available = false;
                }
                break;
            }
        }
    }

    void RotateTile(int Angle)
    {
        if(CurrentTileSelected != null)
        {
            Vector3 rota = CurrentTileSelected.transform.rotation.eulerAngles;
            rota.z += Angle;
            CurrentTileSelected.transform.localEulerAngles = rota;
        }
    }

    public Vector3 GetClosestTile(Vector3 point)
    {
        float minDistance = Mathf.Infinity;
        Vector3 closestTilePosition = Vector3.zero;
        foreach(GameObject GO in CenterPoint)
        {
            float distance = Vector3.Distance(point, GO.transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                closestTilePosition = GO.transform.position;
            }
        }

        return closestTilePosition;
    }

    #region Feedback

    void ShowSelectable()
    {
        if (CurrentTileSelected == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Transform[] children = hit.collider.GetComponentsInChildren<Transform>();
                foreach (Transform child in children)
                {
                    if (child.gameObject.layer == LayerMask.NameToLayer("mat"))
                    {
                        child.GetComponent<MeshRenderer>().material = mats[0];
                        break;
                    }
                }
            }
        }
    }
    public IEnumerator ChangeScale(float addingScale)
    {
        GameObject currentTile = CurrentTileSelected;
        float targetScale = currentTile.transform.localScale.x + addingScale;
        if(addingScale >= 0)
        {

        while (currentTile.transform.localScale.x <= targetScale)
        {
            yield return null;
            if (currentTile != null)
            {
                currentTile.transform.localScale = new Vector3(currentTile.transform.localScale.x + addingScale/4, currentTile.transform.localScale.y + addingScale/4, currentTile.transform.localScale.z + addingScale/4);
            }
        } 
        }
        else if(addingScale < 0)
        {
            while (currentTile.transform.localScale.x >= targetScale)
            {
                yield return null;
                if (currentTile != null)
                {
                    currentTile.transform.localScale = new Vector3(currentTile.transform.localScale.x + addingScale / 4, currentTile.transform.localScale.y + addingScale / 4, currentTile.transform.localScale.z + addingScale / 4);
                }
            }
        }
        currentTile.transform.localScale = new Vector3(currentTile.transform.localScale.x + addingScale, currentTile.transform.localScale.y + addingScale, currentTile.transform.localScale.z + addingScale );
    }

    #endregion
}



public class Utility : MonoBehaviour
{
    public static Vector3 GetMousePos()
    {
        Vector3 mpos = Input.mousePosition;
        mpos.z = Camera.main.nearClipPlane+1;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mpos);
        return pos;
    }

}


[Serializable]
public struct CenterPointAvailable
{
    [SerializeField] Transform PointEditor;

    public Vector3 CenterPoint => PointEditor.position;
    public bool Available;
}