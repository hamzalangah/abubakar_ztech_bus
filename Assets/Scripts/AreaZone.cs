using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaZone : MonoBehaviour
{
    public List<GameObject> characters;
    public GameObject[] Vehicles, Trees;
    public PopulationCharacters PopulationCharacters;

    bool isPopulationActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(AssignPos());
    }
    IEnumerator AssignPos()
    {
        int totalPos = transform.childCount;
        for (int i = 0; i < totalPos; i++)
        {
            int randomPeople = Random.Range(0, PopulationCharacters.Characters.Length);

            GameObject charc = Instantiate(PopulationCharacters.Characters[randomPeople]);

            charc.transform.position = transform.GetChild(i).position;
            charc.transform.rotation = transform.GetChild(i).rotation;

            characters.Add(charc);

            yield return null;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Population") && !isPopulationActive)
    //    {
    //        isPopulationActive = true;
    //        StartCoroutine(ActivePeople());
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Population") && isPopulationActive)
    //    {
    //        isPopulationActive = false;
    //        StartCoroutine(DeactivePeople());
    //    }
    //}
    IEnumerator ActivePeople()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].SetActive(true);
            yield return null;
        }
        for (int j = 0; j < Vehicles.Length; j++)
        {
            Vehicles[j].SetActive(true);
            yield return null;
        }
    }
    IEnumerator DeactivePeople()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].SetActive(false);
            yield return null;
        }
        for (int j = 0; j < Vehicles.Length; j++)
        {
            Vehicles[j].SetActive(false);
            yield return null;
        }
    }
}
