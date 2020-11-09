using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
     //* 100/SpaceShip.distance + 1
    [SerializeField] GameObject Columna;
    [SerializeField] Transform InitPos;
    private float randomNumber;
    private Vector3 RandomPos;
    private int a;
    private float limit = 1;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("CrearColumna", 2.0f, 0.5f);
        StartCoroutine("CrearObstaculo");
        for(a=1; a<=27; a++)
        {
            randomNumber = Random.Range(-10.0f, 10.0f);
            RandomPos = new Vector3(randomNumber, 0, -a*10);
            Vector3 FinalPos = InitPos.position + RandomPos;
            Instantiate(Columna, FinalPos, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
       if (SpaceShip.distance >= 2000)
        {
            limit = 0.75f;
        }
       else if (SpaceShip.distance >= 5000)
        {
            limit = 0.5f;
        }
    }
    // Create obstacles increasing in number of obstacles created depending on the distance travelled.
    IEnumerator CrearObstaculo()
    {
        for (int n = 0; ; n++)
        {
            randomNumber = Random.Range(-10.0f, 10.0f);
            RandomPos = new Vector3(randomNumber, 0, 0);
            Vector3 FinalPos = InitPos.position + RandomPos;
            Instantiate(Columna, FinalPos, Quaternion.identity);
            yield return new WaitForSeconds(1f / (0.1f * ObstacleMove.spaceSpeed * limit));
        }

    }

    // Creates obstacles depending on speed and time
    /*void CrearColumna()
    {
        randomNumber = Random.Range(-10.0f, 10.0f);
        RandomPos = new Vector3(randomNumber, 0, 0);
        Vector3 FinalPos = InitPos.position + RandomPos;
        Instantiate(Columna, FinalPos, Quaternion.identity);
    }*/

}
