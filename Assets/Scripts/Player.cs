using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using SWS;

[System.Serializable]
public class PassangersClass
{
    public GameObject[] PickPassangers, DropPassnagers;
}
public class Player : MonoBehaviour
{
    private UIManager ui;

    private GameManager manager;
    public PassangersClass[] passangers;
    private Rigidbody rb;

    public List<GameObject> Seats, OccupiedSeats;

    public GameObject gateCamera, busDoor;

    [Header("Pick & Drop Parkings")]
    public GameObject[] PickPoints;
    public GameObject[] DropPoints;

    [Header("Directional Arrows")]
    public GameObject[] PickArrows;
    public GameObject[] DropArrows;

    [Header("Pick & Drop Positions")]
    public Transform[] PickStopPos;
    public Transform[] DropStopPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (manager == null) manager = GameManager.instance;

        //ActiveSitPassanger();
        StartCoroutine(SetPassangersAnim());

        if (ui == null) ui = UIManager.instance;
    }
    void ActiveSitPassanger()
    {
        for (int i = 0; i < 10; i++)
        {
            int sit = Random.Range(0, Seats.Count);
            Seats[sit].SetActive(true);
            Seats.RemoveAt(sit);
        }
    }
    IEnumerator SetPassangersAnim()
    {
        yield return new WaitForSeconds(2);
        int totalPassangers = passangers[LevelSelection.LevelNo].PickPassangers.Length;

        for (int i = 0; i < totalPassangers; i++)
        {
            int randomAnim = Random.Range(0, 3);
            passangers[LevelSelection.LevelNo].PickPassangers[i].GetComponent<Animator>().SetInteger("animState", randomAnim);
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickPoint"))
        {
            rb.linearDamping = 10f;

            transform.position = PickStopPos[LevelSelection.LevelNo].transform.position;
            transform.rotation = PickStopPos[LevelSelection.LevelNo].transform.rotation;

            other.gameObject.SetActive(false);
            PickArrows[LevelSelection.LevelNo].SetActive(false);

            UIManager.instance.Controls.SetActive(false);
            StartCoroutine(PickPassangers());
        }
        else if (other.gameObject.CompareTag("DropPoint"))
        {
            rb.linearDamping = 10f;

            transform.position = DropStopPos[LevelSelection.LevelNo].transform.position;
            transform.rotation = DropStopPos[LevelSelection.LevelNo].transform.rotation;

            other.gameObject.SetActive(false);
            DropArrows[LevelSelection.LevelNo].SetActive(false);
            UIManager.instance.Controls.SetActive(false);

            StartCoroutine(DropPassangers());
        }
        else if (other.gameObject.CompareTag("ParkingArea"))
        {
            rb.linearDamping = 10f;

            transform.position = DropStopPos[LevelSelection.LevelNo].transform.position;
            transform.rotation = DropStopPos[LevelSelection.LevelNo].transform.rotation;

            other.gameObject.SetActive(false);
            UIManager.instance.Controls.SetActive(false);

            StartCoroutine(ParkedBus());
        }
    }

    IEnumerator PickPassangers()
    {
        int totalPassangers = passangers[LevelSelection.LevelNo].PickPassangers.Length;

        yield return new WaitForSeconds(2f);

        gateCamera.SetActive(true);

        yield return new WaitForSeconds(3f);
        busDoor.GetComponent<Animator>().SetBool("IsOpen", true);

        yield return new WaitForSeconds(2f);
        for (int pick = 0; pick < totalPassangers; pick++)
        {
            passangers[LevelSelection.LevelNo].PickPassangers[pick].GetComponent<splineMove>().enabled = true;
            passangers[LevelSelection.LevelNo].PickPassangers[pick].GetComponent<Animator>().SetBool("isWalking", true);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator DropPassangers()
    {
        int totalPassangers = OccupiedSeats.Count;

        //int dropTotal = Random.Range(2, DropPassnagers.Count);

        yield return new WaitForSeconds(2f);
        gateCamera.SetActive(true);

        yield return new WaitForSeconds(3f);
        busDoor.GetComponent<Animator>().SetBool("IsOpen", true);

        yield return new WaitForSeconds(2f);
        for (int drop = 0; drop < totalPassangers; drop++)
        {
            int sit = Random.Range(0, passangers[LevelSelection.LevelNo].DropPassnagers.Length);
            passangers[LevelSelection.LevelNo].DropPassnagers[sit].SetActive(true);

            OccupiedSeats[drop].SetActive(false);


            passangers[LevelSelection.LevelNo].DropPassnagers[sit].GetComponent<splineMove>().enabled = true;
            passangers[LevelSelection.LevelNo].DropPassnagers[sit].GetComponent<Animator>().SetBool("isWalking", true);

            //DropPassnagers.RemoveAt(sit);

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(10f);
        manager.LevelComplete();
    }
    IEnumerator ParkedBus()
    {
        yield return new WaitForSeconds(2f);
        manager.LevelComplete();
    }
    public void ActiveSitPassagner(int pass)
    {
        passangers[LevelSelection.LevelNo].PickPassangers[pass].SetActive(false);

        int sit = Random.Range(0, Seats.Count);
        Seats[sit].SetActive(true);
        OccupiedSeats.Add(Seats[sit]);
        Seats.RemoveAt(sit);

        if (pass == passangers[LevelSelection.LevelNo].PickPassangers.Length - 1)
        {
            StartCoroutine(AfterSitting());
        }
    }
    IEnumerator AfterSitting()
    {
        busDoor.GetComponent<Animator>().SetBool("IsOpen", false);

        yield return new WaitForSeconds(2f);
        UIManager.instance.Controls.SetActive(true);

        gateCamera.SetActive(false);

        rb.linearDamping = 0.01f;

        DropArrows[LevelSelection.LevelNo].SetActive(true);

        DropPoints[LevelSelection.LevelNo].SetActive(true);

        StartCoroutine(WatchScenePanel());

    }
    IEnumerator WatchScenePanel()
    {
        yield return new WaitForSeconds(10f);
        ui.watchScenePanel.SetActive(true);
        Time.timeScale = 0f;
    }
}