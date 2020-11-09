using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipRotation : MonoBehaviour
{ 
    [SerializeField] GameObject Spaceship;
    float speed = 10f;
    public static float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveSpaceship();
        //distance and speed.
        distance = distance + 1 * Time.deltaTime * ObstacleMove.spaceSpeed;
        ObstacleMove.spaceSpeed = 10f + 1 * distance / 50;
        speed = 10 * distance / 1000;
        if (speed < 10)
        {
            speed = 10;
        }
        else if (speed > 30)
        {
            speed = 30;
        }
    }
    void moveSpaceship()
    {
        //Makes the spaceship move vertically.
        /*transform.Translate(Vector3.forward * Time.deltaTime * speed * Input.GetAxis("Vertical"));
        if (transform.position.y >= 10)
        {
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        }
        else if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }*/
        //Makes the spaceship move horizontally.
        transform.Translate(Vector3.right * Time.deltaTime * speed * Input.GetAxis("Horizontal"));
        if (transform.position.x >= 10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -10)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }
        //Makes the spaceship rotate.
        /*float rotation = 30f * Time.deltaTime * speed * Input.GetAxis("Horizontal");
        transform.Rotate(0f, 0f, rotation);
        print(rotation);
        if (transform.rotation.z >= 30)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 30f, transform.rotation.w);
        }
        else if (transform.rotation.z <= -30)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -30f, transform.rotation.w);
        }*/
    }
}
