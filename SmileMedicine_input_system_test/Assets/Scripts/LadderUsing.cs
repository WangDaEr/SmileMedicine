using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderUsing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject sor = other.gameObject;

        if (gameObject.tag == "ladderEntrance" && sor.tag == "Player")
        {
            sor.GetComponent<PlayerController>().X_restraint = true;
            sor.GetComponent<PlayerController>().Y_restraint = false;

            Debug.Log("change to ver");
        }
        else if (gameObject.tag == "ladderExit" && sor.tag == "Player")
        {
            sor.GetComponent<PlayerController>().X_restraint = false;
            sor.GetComponent<PlayerController>().Y_restraint = true;

            Debug.Log("change to hor");
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        GameObject sor = collision.gameObject;

        //change the allowed movement direction to upwards
        if (gameObject.tag == "ladderEntrance" && sor.tag == "Player")
        {
            sor.GetComponent<PlayerController>().X_restraint = true;
            sor.GetComponent<PlayerController>().Y_restraint = false;

            Debug.Log("change to ver");
        }
        else if (gameObject.tag == "ladderExit" && sor.tag == "Player")
        {
            sor.GetComponent<PlayerController>().X_restraint = false;
            sor.GetComponent<PlayerController>().Y_restraint = true;

            Debug.Log("change to hor");
        }
    }
    */
}
