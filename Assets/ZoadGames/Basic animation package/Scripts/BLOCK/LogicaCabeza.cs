using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaCabeza : MonoBehaviour
{

    public int contadorcolision = 0;
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
        contadorcolision++;
    }

    private void OnTriggerExit(Collider other)
    {
        contadorcolision--;
    }
}
