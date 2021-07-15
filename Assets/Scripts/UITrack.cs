using UnityEngine;
using UnityEngine.UI;

public class UITrack : MonoBehaviour
{
    public Slider UIElement;

    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        UIElement.transform.position = pos;
    }
}
