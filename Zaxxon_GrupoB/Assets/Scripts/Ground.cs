using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ground : MonoBehaviour
{
    //Variable para el MeshRenderer
    private MeshRenderer render;
    //Accedemos al código general
    private GeneralCode generalCode;
    void Start()
    {
        //Accedemos al código general
        generalCode = GameObject.Find("VarObject").GetComponent<GeneralCode>();
        //Accedemos al Mesh Renderer
        render = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        //Si está vivo hacemos que el suelo se mueva
        if (generalCode.isAlive)
        {
            float offset = generalCode.spaceSpeed * Time.time * 0.3f;
            render.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
    }
}
