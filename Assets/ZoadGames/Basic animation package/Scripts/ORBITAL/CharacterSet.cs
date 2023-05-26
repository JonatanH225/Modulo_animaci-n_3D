using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSet : MonoBehaviour
{
    public GameObject modelo;
    private GameObject instance;

    public void SetNewModel()
    {
        if (modelo)
        {
            Transform previousBody = transform.Find("ModeloBase");
            if (previousBody) DestroyImmediate(previousBody.gameObject);

            instance = Instantiate(modelo, transform);
            instance.name = "ModeloBase";

            CameraOrbit cm = GetComponent<CameraOrbit>();
            for (int i = 0; i < instance.transform.childCount; ++i)
            {
                if (!instance.transform.GetChild(i).GetComponent<MeshRenderer>())
                    cm.follow = instance.transform.GetChild(i);
            };
            Debug.Log("New model ok");
        }
        else Debug.LogError("You must load a new model");


    }
}