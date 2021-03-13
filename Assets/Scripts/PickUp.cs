using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    float throwForce = 1000;
    Vector3 objectPos; 
    float range = 5; 

    public bool canHold = true;
    public bool isHolding = false; 
    public GameObject item; 
    public GameObject tempParent; 
    public Transform guide; 

    void Start()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Grabbing Objects and Letting go of Objects 
        if (isHolding == false)
        {
            if (Input.GetKeyDown(KeyCode.C) && (guide.transform.position - transform.position).sqrMagnitude < range * range)
            {
                pickup();
                isHolding = true;
            }
        }
        else if (isHolding == true)
        {
            // Throwing 
            if (Input.GetKeyDown(KeyCode.V)) 
            {
                throwObject();
                isHolding = false; 
            }               
            else if (Input.GetKeyDown(KeyCode.C))
            {
                drop();
                isHolding = false;
            }             
        }
    }

    void pickup()
    {
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.position = guide.transform.position;
        item.transform.rotation = guide.transform.rotation;
        item.transform.parent = tempParent.transform;    
    }
    void drop()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        item.transform.position = guide.transform.position;
    }    

    void throwObject()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        item.transform.position = guide.transform.position;
        item.GetComponent<Rigidbody>().AddForce(guide.forward * throwForce);
    }        

}
