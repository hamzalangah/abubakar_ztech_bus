using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VehicleSelection : MonoBehaviour
{
    public GameObject[] Vehicles, Loadings;
    public static int vehicle_index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void Next()
    {
        vehicle_index++;

        if (vehicle_index >= Vehicles.Length)
            vehicle_index = 0;

        UpdateVehicle();
    }
    public void Previous()
    {
        vehicle_index--;

        if (vehicle_index < 0)
            vehicle_index = Vehicles.Length - 1; 

        UpdateVehicle();
    }
    void UpdateVehicle()
    {
        for (int i = 0; i < Vehicles.Length; i++)
        {
            Vehicles[i].SetActive(false);
        }

        Vehicles[vehicle_index].SetActive(true);
    }
    public void ModeSelection() => StartCoroutine(ModeSelectionC());

    IEnumerator ModeSelectionC()
    {
        Loadings[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LevelSelection");
    }
    
}
