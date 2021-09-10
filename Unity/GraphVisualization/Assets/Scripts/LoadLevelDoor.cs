using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoadLevelDoor : MonoBehaviour
{
    [FormerlySerializedAs("scenename")] [SerializeField] private PortalDoor door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Door Entered");
        SceneManager.LoadScene(door.Scenename);
    }
}
