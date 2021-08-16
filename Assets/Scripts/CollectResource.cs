using UnityEngine;
using System.Collections;

public class CollectResource : MonoBehaviour
{
    public InventoryObject inventory;
    //public GameObject player;

    public InventoryUI inventoryUI;

    float rayLength = 2f;

    public GameObject inventoryUIObject;

    public ReloadBar reload;

    bool isLoaded = true;

    bool runningCoroutine = false;

    float tick = 1;

    void Start()
    {
        inventoryUIObject.GetComponent<InventoryUI>().UpdateUI();

        //inventory.PrintContents();
    }

    void Update()
    {
        ////tick = inventoryUI.GetSelectedItem().efficiency;

        Vector3 rayDirection = transform.rotation * Vector3.forward * rayLength;
        Debug.DrawRay(transform.position, rayDirection);

        if (Input.GetKey(KeyCode.Space))
        {
            //reload.isLoaded
            if (isLoaded)
            {
                isLoaded = false;

                Ray ray = new Ray(transform.position, rayDirection);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayLength))
                {
                    var Object = hit.transform.GetComponent<Object>();

                    //Efficiency
                    if (inventoryUI.GetSelectedItem() != null)
                    {
                        for (int i = 0; i < inventoryUI.GetSelectedItem().efficiencyTargetObjects.Length; i++)
                        {
                            if (Object.objectObject.name == inventoryUI.GetSelectedItem().efficiencyTargetObjects[i])
                            {
                                tick *= inventoryUI.GetSelectedItem().efficiency;
                                return;
                            }
                            else
                            {
                                tick = 1f;
                            }
                        }
                    }
                    
                    if (Object)
                    {
                        Object.BreakState();
                        if (Object.isBroken == true)
                        {
                            for (int i = 0; i < Object.objectObject.dropItem.Length; i++)
                            {
                                inventory.AddItemToInventory(Object.objectObject.dropItem[i], Object.objectObject.dropAmount[i]);
                            }
                            inventoryUIObject.GetComponent<InventoryUI>().UpdateUI();

                            //inventory.PrintContents();
                            Object.Destroy();
                        }
                        //reload.startReload();
                    }
                }
            }
            else if (!runningCoroutine)
            {
                StartCoroutine(Tick());
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
            FindObjectOfType<AudioManager>().Play("Mining01");
        if (Input.GetKeyUp(KeyCode.Space))
            FindObjectOfType<AudioManager>().Stop("Mining01");
        */
    }

    IEnumerator Tick()
    {
        runningCoroutine = true;

        yield return new WaitForSeconds(tick);

        isLoaded = true;
        runningCoroutine = false;
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
    }
}
