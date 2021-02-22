using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    //Accedemos al código general
    private GeneralCode generalCode;
    void Start()
    {
        //Accedemos al código general
        generalCode = GameObject.Find("VarObject").GetComponent<GeneralCode>();
    }
    void Update()
    {
        //Movimiento de la caja.
        transform.Translate(Vector3.forward * Time.deltaTime * -generalCode.spaceSpeed);
    }
}
