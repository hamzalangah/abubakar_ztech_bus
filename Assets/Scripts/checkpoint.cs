using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour
{
    public GameObject[] ReleaseObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(Hidethis());
        }
    }
    IEnumerator Hidethis()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject things in ReleaseObjects)
        {
            if (ReleaseObjects != null)
            {
                things.SetActive(true);
            }
        }
        yield return new WaitForSeconds(10f);
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {


    }
}
