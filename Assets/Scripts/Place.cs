using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    public InventoryObject inventory;

    public InventoryUI inventoryUI;

    float rayLength = 2f;

    public Transform ray02Transform;

    public WorldObject world;

    public GenerateTerrain terrainScript;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (inventoryUI.GetSelectedItem() != null)
            {
                if (inventoryUI.GetSelectedItem().isPlaceable)
                {
                    PlaceObject();
                }
            }
        }
    }

    public void PlaceObject()
    {
        Vector3 rayDirection01 = transform.rotation * Vector3.forward * rayLength;
        Debug.DrawRay(transform.position, rayDirection01);

        Ray ray01 = new Ray(transform.position, rayDirection01);

        if (!Physics.Raycast(ray01, out _, rayLength))
        {
            // No object detected in front of player

            Vector3 rayDirection02 = ray02Transform.rotation * Vector3.down * rayLength;
            Debug.DrawRay(ray02Transform.position, rayDirection02);

            Ray ray02 = new Ray(ray02Transform.position, rayDirection02);
            RaycastHit hit02;

            if (Physics.Raycast(ray02, out hit02, rayLength))
            {
                // Found ground tile

                if (hit02.transform.GetComponent<Object>().coordinates.x % 1 == 0 && hit02.transform.GetComponent<Object>().coordinates.y % 1 == 0)
                {
                    // Ready to place

                    if (world.ValueAtKeyObject(hit02.transform.GetComponent<Object>().coordinates) && !world.GetObject(hit02.transform.GetComponent<Object>().coordinates).objectType.collider)
                    {
                        // There is an object without a collider (grass)
                        // remove grass
                        world.RemoveObject(hit02.transform.GetComponent<Object>().coordinates);
                        // Need to figure out a way to destroy an object from the WorldObject
                        //Destroy(world.GetObject(hit02.transform.GetComponent<Object>().coordinates).objectType.collider);
                    }

                    // Then add object to world
                    world.AddObject(hit02.transform.GetComponent<Object>().coordinates, inventoryUI.GetSelectedItem().objectObject);
                    inventory.AddItemToHotbarAt(inventoryUI.selectedHotbarSlot, inventoryUI.GetSelectedItem(), -1);
                    inventoryUI.UpdateUI();
                    terrainScript.InstantiateObjectTile(new Vector2(hit02.transform.GetComponent<Object>().coordinates.x, hit02.transform.GetComponent<Object>().coordinates.y));
                }
            }
        }
    }
}
