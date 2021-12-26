using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    public InventoryObject inventory;

    public InventoryUI inventoryUI;

    float rayLength = 2f;

    public Transform ray02Transform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (inventoryUI.GetSelectedItem().isPlaceable)
            {
                PlaceObject();
            }
        }
    }

    public void PlaceObject()
    {
        Debug.Log("PlaceObject called");

        Vector3 rayDirection01 = transform.rotation * Vector3.forward * rayLength;
        Debug.DrawRay(transform.position, rayDirection01);

        Ray ray01 = new Ray(transform.position, rayDirection01);
        RaycastHit hit01;

        if (!Physics.Raycast(ray01, out hit01, rayLength))
        {
            Debug.Log("No object detected in front of player");

            Vector3 rayDirection02 = ray02Transform.rotation * Vector3.down * rayLength;
            Debug.DrawRay(ray02Transform.position, rayDirection02);

            Ray ray02 = new Ray(ray02Transform.position, rayDirection02);
            RaycastHit hit02;
            if (Physics.Raycast(ray02, out hit02, rayLength))
            {
                Debug.Log("Found ground tile");

                //var hitObject = hit02.transform.GetComponent<Object>();

                Instantiate(inventoryUI.GetSelectedItem().objectPrefab, hit02.transform.position, hit02.transform.rotation);
            }
        }
    }
}
