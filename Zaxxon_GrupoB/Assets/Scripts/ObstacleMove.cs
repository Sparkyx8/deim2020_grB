using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] GameObject Columna;
    public static float spaceSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 180, 0);
        StartCoroutine("Limite");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * spaceSpeed);
        print(spaceSpeed);
        if (spaceSpeed >= 50)
        {
            spaceSpeed = 50;
        }
    }
    IEnumerator Limite()
    {
        for (int n = 0; ; n++)
        {
            if(transform.position.z <= -8)
            {
                Destroy(Columna);
            }
            yield return new WaitForSeconds(2f);
        }

    }
}
