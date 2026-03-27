using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanicButton : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            FindAnyObjectByType<LevelManager>().LoadSceneByIndex(0);
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            FindAnyObjectByType<LevelManager>().LoadSceneByIndex(1);
        }
    }
}
