using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    private int groundCollisions = 0;
    [SerializeField] public GameObject brokenTree;
    
    void OnCollisionEnter(Collision collision)
    {
        // if (collision.collider.tag == "Destroyable")
        // {
        //     Destroy(gameObject);
        // }

        if (collision.collider.tag == "Ground")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;    
            //groundCollisions++;
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
