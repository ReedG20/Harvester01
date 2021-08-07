using UnityEngine;

public class CollectResource : MonoBehaviour
{
    public InventoryObject inventory;
    //public GameObject player;

    float rayLength = 2f;

    public GameObject inventoryUIObject;

    public ReloadBar reload;

    void Start()
    {
        inventoryUIObject.GetComponent<InventoryUI>().UpdateUI();

        //inventory.PrintContents();
    }

    void Update()
    {
        Vector3 rayDirection = transform.rotation * Vector3.forward * rayLength;
        Debug.DrawRay(transform.position, rayDirection);

        if (Input.GetKey(KeyCode.Space))
        {
            if (reload.isLoaded)
            {
                Ray ray = new Ray(transform.position, rayDirection);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayLength))
                {
                    var Object = hit.transform.GetComponent<Object>();
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
                        reload.startReload();
                    }
                }
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
            FindObjectOfType<AudioManager>().Play("Mining01");
        if (Input.GetKeyUp(KeyCode.Space))
            FindObjectOfType<AudioManager>().Stop("Mining01");
        */
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
    }
}
