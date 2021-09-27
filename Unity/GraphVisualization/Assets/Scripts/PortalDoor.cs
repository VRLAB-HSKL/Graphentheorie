using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PortalDoor : MonoBehaviour
{
    [SerializeField] private string _Scenename;
    [field: SerializeField] public GameObject VRCamera { get; set; }

    public string Scenename
    {
        get
        {
            return _Scenename;
        }
    }
    
}
