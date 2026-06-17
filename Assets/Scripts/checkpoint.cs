using System.Collections;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public GameObject[] ReleaseObjects;
    public GameObject Sprites;
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Sprites.SetActive(false);
            StartCoroutine(Hidethis());
        }
    }
    IEnumerator Hidethis()
    {
        foreach (GameObject things in ReleaseObjects)
        {
            things.SetActive(true);
        }
        yield return new WaitForSeconds(4);

        foreach (GameObject things in ReleaseObjects)
        {
            things.SetActive(false);

        }
    }
}

