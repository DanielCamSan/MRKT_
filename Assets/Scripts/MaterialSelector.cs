using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelector : MonoBehaviour
{
    public string materialName = "Bulbasaur";
    public string materialCode = "bulbasaur";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnMouseDown()
    {
        StartCoroutine(RestManager.instance.GetMaterial(materialCode));
    }
}
