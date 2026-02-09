using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PointAndClick : MonoBehaviour
{
    public GameObject CurrentTileSelected;
    [SerializeField] LayerMask TileLayer;
    [SerializeField] LayerMask PlayerLayer;

    /*
     a faire:
            deplacement seulement sur des cases "vide"
            ajouter juice rota (voir la rota)
            ajout event quand case posé
            impossible de posé une case hors de la map
            
            clic droit pour faire déplacer le joueur
            ajout fin

     */
    void Update()
    {
        ManageInput();
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
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            RotateTile(90);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            RotateTile(-90);
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
        }
        else
        {
            Debug.Log("Not Hit");
        }
    }

    void TryToReleaseTile()
    {
        CurrentTileSelected = null;
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
