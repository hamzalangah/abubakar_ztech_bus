using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class PopulationManager : MonoBehaviour
{
    public int rush = 20;
    public PopulationCharacters charactersPrefabs;
    public Queue<GameObject> characters = new Queue<GameObject>();
    public GameObject[] Zones;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AssignPos());
    }
    IEnumerator AssignPos()
    {

        for (int i = 0; i < Zones.Length; i++)
        {
            int totalChild = Zones[i].transform.childCount;

            for (int j = 0; j < totalChild; j++)
            {
                int randomAnim = Random.Range(0, 3);
                int randomPeople = Random.Range(0, charactersPrefabs.Characters.Length);

                GameObject charc = Instantiate(charactersPrefabs.Characters[randomPeople]);

                charc.transform.position = Zones[i].transform.GetChild(j).position;
                charc.transform.rotation = Zones[i].transform.GetChild(j).rotation;

                characters.Enqueue(charc);
                charc.GetComponent<Animator>().SetInteger("animState", randomAnim);
                yield return null;
            }
            int totalVehicles = Zones[i].GetComponent<AreaZone>().Vehicles.Length;
            for (int k = 0; k < totalVehicles; k++)
            {
                Zones[i].GetComponent<AreaZone>().Vehicles[k].SetActive(true);
                yield return null;
            }
        }
    }
}
