using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManager : MonoBehaviour
{
    public Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = pos.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
