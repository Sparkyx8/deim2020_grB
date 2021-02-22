using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    //Variable para encontrar a la nave
    [SerializeField] GameObject spaceShip;
    //Variables para acceder al código general
    private GeneralCode generalCode;
    //Variable para acceder al audio source
    private AudioSource audioSource;
    void Start()
    {
        //Instanciamos la nave y la hacemos padre de este game object
        var mySpaceShip = Instantiate(spaceShip, transform.position, Quaternion.identity);
        gameObject.transform.parent = mySpaceShip.transform;
        //Accedemos al Game Object general.
        generalCode = GameObject.Find("VarObject").GetComponent<GeneralCode>();
        //Accedemos al audio source
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        //Paramos el audio si el jugador muere
        if (!generalCode.isAlive)
        {
            audioSource.Stop();
        }
    }
}
