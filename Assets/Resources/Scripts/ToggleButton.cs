using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : Button
{
    bool isOn;

    public bool IsOn
    {
        get 
        {
            return isOn;
        }
        set 
        {
            isOn = value;
            transform.GetChild(0).GetComponent<Image>().enabled = isOn;
        }
    } 
}