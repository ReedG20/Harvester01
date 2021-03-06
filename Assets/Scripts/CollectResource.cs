using UnityEngine;
using System.Collections;

public class CollectResource : MonoBehaviour
{
    public InventoryObject inventory;

    public WorldObject world;

    public InventoryUI inventoryUI;

    float rayLength = 2f;

    public GameObject inventoryUIObject;

    public ReloadBar reload;

    bool isLoaded = true;

    bool runningCoroutine = false;

    float tick = 1;

    // Debugging
    public ItemObject[] debugItems;
    public int[] debugAmounts;

    void Start()
    {
        inventoryUIObject.GetComponent<InventoryUI>().UpdateUI();
    }

    void Update()
    {
        Vector3 rayDirection = transform.rotation * Vector3.forward * rayLength;
        Debug.DrawRay(transform.position, rayDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray ray = new Ray(transform.position, rayDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength))
            {
                var hitObject = hit.transform.GetComponent<Object>();

                if (hitObject)
                {
                    if (hitObject.objectObject.objectType.dropItem.Length > 0)
                    {
                        for (int i = 0; i < hitObject.objectObject.objectType.dropItem.Length; i++)
                        {
                            inventory.AddItemToInventory(hitObject.objectObject.objectType.dropItem[i], hitObject.objectObject.objectType.dropAmount[i]);
                        }
                    }

                    world.RemoveObject(hitObject.coordinates);
                    // change later to be removed as a result of WorldObject changing
                    Destroy(hitObject.gameObject);
                    inventoryUIObject.GetComponent<InventoryUI>().UpdateUI();
                }
            }

            // Old loading code
            /*
            if (isLoaded)
            {
                isLoaded = false;

                Ray ray = new Ray(transform.position, rayDirection);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayLength))
                {
                    var hitObject = hit.transform.GetComponent<Object>();

                    // Efficiency
                    
                    if (inventoryUI.GetSelectedItem() != null)
                    {
                        for (int i = 0; i < inventoryUI.GetSelectedItem().efficiencyTargetObjects.Length; i++)
                        {
                            tick = 1f;
                            if (hitObject.objectObject == inventoryUI.GetSelectedItem().efficiencyTargetObjects[i])
                            {
                                tick *= inventoryUI.GetSelectedItem().toolEfficiencyMultiplier;
                                break;
                            }
                            else
                            {
                                tick = 1f;
                            }
                        }
                        
                    }
                    
                    
                    if (hitObject)
                    {
                        hitObject.BreakState();
                        
                        if (hitObject.isBroken == true)
                        {
                            for (int i = 0; i < hitObject.objectObject.type.dropItem.Length; i++) 
                            {
                                inventory.AddItemToInventory(hitObject.objectObject.type.dropItem[i], hitObject.objectObject.type.dropAmount[i]);
                            }
                            inventoryUIObject.GetComponent<InventoryUI>().UpdateUI();

                            hitObject.Destroy();
                        }
                        
                    }
                }
            }
            
            
            else if (!runningCoroutine)
            {
                StartCoroutine(Tick());
            }
            //*/
        }

        //SFX
        /*
        if (Input.GetKeyDown(KeyCode.Space))
            FindObjectOfType<AudioManager>().Play("Mining01");
        if (Input.GetKeyUp(KeyCode.Space))
            FindObjectOfType<AudioManager>().Stop("Mining01");
        */

        // Debugging
        /*
        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < debugItems.Length; i++)
            {
                inventory.AddItemToInventory(debugItems[i], debugAmounts[i]);
            }
        }
        */
    }

    /*
    IEnumerator Tick()
    {
        runningCoroutine = true;

        yield return new WaitForSeconds(tick);

        isLoaded = true;
        runningCoroutine = false;
    }
    */

    private void OnApplicationQuit()
    {
        inventory.Clear();
    }
}
