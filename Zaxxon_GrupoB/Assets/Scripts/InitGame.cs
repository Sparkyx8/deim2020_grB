using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class InitGame : MonoBehaviour
{
    //Accedemos a los objetos del canvas
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject background;
    [SerializeField] GameObject particles;
    [SerializeField] GameObject skinsMenu;
    //Booleana para saber el menú en el que está
    private bool inSkins = false;
    //Variables para cambiar el material y la malla de la nave
    [SerializeField] Mesh[] meshes;
    public MeshFilter myMeshFilter;
    public MeshRenderer myMeshRenderer;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material[] materials;
    [SerializeField] GameObject spaceShipBase;
    [SerializeField] GameObject spaceShip;
    //Variables para saber en que material y malla estamos del array
    [HideInInspector] public int currentMesh;
    [HideInInspector] public int currentColor;
    //Variables para los sonidos de los botones
    [SerializeField] AudioClip bigButtons;
    [SerializeField] AudioClip smallButtons;
    //Variable para el audio source
    private AudioSource audioSource;
    void Start()
    {
        //Accedemos al audio source
        audioSource = GetComponent<AudioSource>();
        //Reseteamos la skin de la nave a 0 si no ha elegido todavia
        if (GeneralCode.hasSkin == false)
        {
            meshRenderer.material = materials[0];
            myMeshRenderer.sharedMaterial = materials[0];
            meshFilter.sharedMesh = meshes[0];
            myMeshFilter.sharedMesh = meshes[0];
            currentMesh = 0;
        }
        //Mantenemos la skin si ha elegido ya una
        else if (GeneralCode.hasSkin == true)
        {
            meshRenderer.material = materials[currentColor];
            myMeshRenderer.sharedMaterial = materials[currentColor];
            meshFilter.sharedMesh = meshes[currentMesh];
            myMeshFilter.sharedMesh = meshes[currentMesh];
        }
    }
    void Update()
    {
        //Desactivamos algunos GO si no está en el menú de skin
        if (inSkins == false)
        {
            skinsMenu.SetActive(false);
            spaceShip.SetActive(false);
        }
    }
    //Métodos para los botones
    public void StartGame()
    {
        audioSource.PlayOneShot(bigButtons);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void Skins()
    {
        audioSource.PlayOneShot(bigButtons);
        inSkins = true;
        buttons.SetActive(false);
        background.SetActive(false);
        particles.SetActive(false);
        skinsMenu.SetActive(true);
        spaceShip.SetActive(true);
    }
    public void Exit()
    {
        audioSource.PlayOneShot(bigButtons);
        Application.Quit();
    }
    public void Black()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[0];
        myMeshRenderer.sharedMaterial = materials[0];
        currentColor = 0;
    }
    public void White()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[1];
        myMeshRenderer.material = materials[1];
        currentColor = 1;
    }
    public void Blue()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[2];
        myMeshRenderer.material = materials[2];
        currentColor = 2;
    }
    public void Cyan()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[3];
        myMeshRenderer.material = materials[3];
        currentColor = 3;
    }
    public void Green()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[4];
        myMeshRenderer.material = materials[4];
        currentColor = 4;
    }
    public void Orange()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[5];
        myMeshRenderer.material = materials[5];
        currentColor = 5;
    }
    public void Purple()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[6];
        myMeshRenderer.material = materials[6];
        currentColor = 6;
    }
    public void Red()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[7];
        myMeshRenderer.material = materials[7];
        currentColor = 7;
    }
    public void Yellow()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[8];
        myMeshRenderer.material = materials[8];
        currentColor = 8;
    }
    public void Grey()
    {
        audioSource.PlayOneShot(smallButtons);
        meshRenderer.material = materials[9];
        myMeshRenderer.material = materials[9];
        currentColor = 9;
    }
    public void nextMesh()
    {
        audioSource.PlayOneShot(smallButtons);
        if (currentMesh < 12)
        {
            currentMesh += 1;
        }
        else if (currentMesh == 12)
        {
            currentMesh = 1;
        }
        meshFilter.sharedMesh = meshes[currentMesh];
        myMeshFilter.sharedMesh = meshes[currentMesh];
    }
    public void previousMesh()
    {
        audioSource.PlayOneShot(smallButtons);
        if (currentMesh > 0)
        {
            currentMesh += -1;
        }
        else if (currentMesh == 0)
        {
            currentMesh = 12;
        }
        print(currentMesh);
        meshFilter.sharedMesh = meshes[currentMesh];
        myMeshFilter.sharedMesh = meshes[currentMesh];
    }
    public void Return()
    {
        audioSource.PlayOneShot(smallButtons);
        inSkins = false;
        buttons.SetActive(true);
        background.SetActive(true);
        particles.SetActive(true);
    }
}
