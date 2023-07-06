using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    private int groundCollisions = 0;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Tree")
        {
            Destroy(gameObject);
        }

        if (collision.collider.tag == "Ground")
        {
            groundCollisions++;
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Rigidbody>().AddForce(1000 * Physics.gravity);
    }

    // Update is called once per frame
    void Update()
    {
        if(groundCollisions>3)   Destroy(gameObject);
    }
}
