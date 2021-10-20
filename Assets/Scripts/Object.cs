using UnityEngine;

public class Object : MonoBehaviour
{
    /*
    The Object class sits on the gameobject
    and holds that gameobject's ObjectObject
    */

    public ObjectObject objectObject;

    GameObject lastObject;

    public bool isBroken;

    [Header("States: (full, slightly damaged, very damaged)")]
    public GameObject[] states;

    // (Same as states array) 0 = full, 1 = slighty damaged, 2 = very damaged
    public int state;

    void Start()
    {
        state = 0;
    }

    public void BreakState()
    {
        if (state == 2)
        {
            isBroken = true;
        }

        if (state != 2)
        {
            state++;

            if (lastObject != null)
            {
                Destroy(lastObject);
            }

            lastObject = Instantiate(states[state], transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void Destroy()
    {
        Destroy(lastObject);
        Destroy(gameObject);
    }
}
