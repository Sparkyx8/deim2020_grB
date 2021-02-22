using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MineMove : MonoBehaviour
{
    //Variable para que afecte al obstáculo.
    [SerializeField] GameObject Mine;
    //Variable para acceder a las variables generales
    private GeneralCode generalCode;
    void Start()
    {
        //Accedemos al código general
        generalCode = GameObject.Find("VarObject").GetComponent<GeneralCode>();
        //Giramos el obstáculo para que forward sea hacia la nave y no al revés.
        transform.Rotate(0, 180, 0);
        //Empezamos la corrutina que destruirá los obstáculos cuando salgan del campo de visión.
        StartCoroutine("Limite");
    }
    void Update()
    {
        //Movimiento de los obstáculos.
        transform.Translate(Vector3.forward * Time.deltaTime * generalCode.spaceSpeed);
    }
    //Corrutina para destruir los obstáculos cuando salgan del campo de visión.
    IEnumerator Limite()
    {
        for (int n = 0; ; n++)
        {
            if (transform.position.z <= -8)
            {
                Destroy(Mine);
            }
            yield return new WaitForSeconds(2f);
        }

    }
}
