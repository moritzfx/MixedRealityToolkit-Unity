using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    public Transform spawnpoint;
    GameObject curObj;
    GameObject curParent;

    // Start is called before the first frame update
    void Start()
    {
        print("Script started");
    }

    // Update is called once per frame
    void Update()
    {
        if (curObj != null)
        {
            // Synchronize the rotation of curObj with curParent
            curObj.transform.rotation = curParent.transform.rotation;

            // Remove Rigidbody component to disable physics
            Destroy(curObj.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (curObj != null)
        {
            // An object is already in the trigger, so do nothing
            return;
        }

        print("On Trigger enter");
        curParent = other.gameObject;

        // Instantiate the parent object at the spawnpoint
        curObj = Instantiate(other.gameObject.transform.parent.gameObject, spawnpoint.position, Quaternion.identity);

        // Scale the object by a factor of 3
        curObj.transform.localScale *= 5;
    }

    private void OnTriggerExit(Collider other)
    {
        if (curObj == null || curParent == null)
        {
            return;
        }

        if (other.gameObject == curParent)
        {
            // Destroy curObj only if the object leaving the trigger is the current parent
            Destroy(curObj);
            curObj = null;
            curParent = null;
        }
    }
}
