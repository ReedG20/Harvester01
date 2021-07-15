using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    public Slider reloadBar;

    int maxValue = 100;
    int currentValue;

    public Image background;
    public Image fill;

    WaitForSecondsRealtime reloadTick = new WaitForSecondsRealtime(0.1f);

    public bool isLoaded;

    // Start is called before the first frame update
    void Start()
    {
        currentValue = maxValue;
        reloadBar.maxValue = maxValue;
        reloadBar.value = maxValue;

        isLoaded = true;
    }

    void Update()
    {
        //Debug.Log(isLoaded);

        if (currentValue >= maxValue)
        {
            isLoaded = true;

            background.enabled = false;
            fill.enabled = false;
        }
        else
        {
            isLoaded = false;

            background.enabled = true;
            fill.enabled = true;
        }
    }

    public void startReload()
    {
        currentValue = 0;
        reloadBar.value = 0;
        isLoaded = false;

        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        while (currentValue < maxValue)
        {
            currentValue += maxValue / 8;
            reloadBar.value = currentValue;
            yield return reloadTick;
        }
    }
}
