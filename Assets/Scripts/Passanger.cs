using UnityEngine;
using System.Collections;

public class Passanger : MonoBehaviour
{
    public Transform[] Seats;
    public bool isPicking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void HideSelf()
    {
        this.gameObject.SetActive(false);
    }
}
