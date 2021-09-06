using System.Collections;
using System.Collections.Generic;
using GraphContent;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // public GameObject Matrix;
    private DataManagerSingleton _dataManager = DataManagerSingleton.Instance;

    public void ShowHide(GameObject Matrix)
    {
        if (!_dataManager.MenuState)
        {
            Matrix.SetActive(true);
            _dataManager.MenuState = true;
        }
        else
        {
            Matrix.SetActive(false);
            _dataManager.MenuState = false;
        }
    }
    
}
