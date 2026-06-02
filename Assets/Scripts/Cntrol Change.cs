using UnityEngine;

public class BusToggleSwitch : MonoBehaviour
{
    public GameObject bus1;
    public GameObject bus2;

    private bool isBus1Active = true;

    void Start()
    {
        // Start state
        bus1.SetActive(true);
        bus2.SetActive(false);
    }

    // Call this function on button click
    public void ToggleBuses()
    {
        if (isBus1Active)
        {
            bus1.SetActive(false);
            bus2.SetActive(true);
        }
        else
        {
            bus1.SetActive(true);
            bus2.SetActive(false);
        }

        isBus1Active = !isBus1Active;
    }
}