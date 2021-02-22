using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    //Velocidad del misil
    private float speed = 30;
    //Variable para el meshRenderer
    private MeshRenderer meshRenderer;
    //Accedemos a la explosión
    [SerializeField] GameObject explosion;
    //Accedemos al audio source
    private AudioSource audioSource;
    //Posición de la explosión
    private Vector3 explsPos;
    void Start()
    {
        //Accedemos al mesh renderer y al audio source
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        //Hacemos que el misil se mueva
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //Generamos la posición de la explosión
        explsPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    //Comprobamos si choca con un obstáculo
    private void OnTriggerEnter(Collider other)
    {
        //Si choca desaparecemos el misil, destruimos el obstáculo e instanciamos la explosión junto con el sonido
        if (other.gameObject.tag == "Obstacle")
        {
            meshRenderer.enabled = false;
            Destroy(other.gameObject);
            Instantiate(explosion, explsPos, Quaternion.identity);
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
