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
    [SerializeField] CenterPointAvailable[] CenterPointAvailables;

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
        foreach (var CPA in CenterPointAvailables)
        {
            if (CPA.CenterPoint == CurrentTileSelected.transform.position)
            {
                    CPA.Available = true;

                break;
            }
        }
    }

    void TryToReleaseTile()
    {
        Vector3 point = GetClosestTile(Utility.GetMousePos());

        foreach(var CPA in CenterPointAvailables)
        {
            if(CPA.CenterPoint == point)
            {
                if (CPA.Available)
                {
                    CurrentTileSelected.transform.position = point;
                    CPA.Available = false;
                }
                break;
            }
        }

        StartCoroutine(ChangeScale(-AddScaleSelection));
        CurrentTileSelected.GetComponent<Tile>().CheckPosition();
        ResetCPA();
        CurrentTileSelected = null;
    }

    void ResetCPA()
    {
        Vector3 point = CurrentTileSelected.transform.position;
        foreach (var CPA in CenterPointAvailables)
        {
            if (CPA.CenterPoint == point)
            {
                if (CPA.Available)
                {
                    CPA.Available = false;
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
public class CenterPointAvailable
{
    [SerializeField] Transform PointEditor;

    public Vector3 CenterPoint => PointEditor.position;
    public bool Available;
}