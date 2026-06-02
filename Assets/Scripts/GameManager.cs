using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class BusPassangers
{
    public GameObject[] Bus_Passangers;
}
public class GameManager : MonoBehaviour
{
    public BusPassangers[] passangers;
    public static GameManager instance;
    private UIManager ui;

    public float[] FirstSceneDuration, MidSceneDuration, LastSceneDuration;
    public Transform[] Positions;
    public GameObject[] Players, Levels;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Players[VehicleSelection.vehicle_index].SetActive(true);
        Rigidbody rb = Players[VehicleSelection.vehicle_index].GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = Positions[LevelSelection.LevelNo].position;
        rb.rotation = Positions[LevelSelection.LevelNo].rotation;

        Players[VehicleSelection.vehicle_index].GetComponent<Rigidbody>().isKinematic = false;

        ActiveLevels();

        if (ui == null) ui = UIManager.instance;

        passangers[LevelSelection.LevelNo].Bus_Passangers[VehicleSelection.vehicle_index].SetActive(true);

        StartCoroutine(StartScene());
    }
    void ActiveLevels()
    {
        Levels[LevelSelection.LevelNo].SetActive(true);
    }

    IEnumerator StartScene()
    {
        ui.LoadingPanel.SetActive(true);
        yield return new WaitForSeconds(FirstSceneDuration[LevelSelection.LevelNo]);
        ui.LoadingPanel.SetActive(false);
    }
    

    public void WathSceneYesorNO()
    {
        Time.timeScale = 1f;
        ui.watchScenePanel.SetActive(false);
    }
    public void LevelComplete()
    {
        ui.WinPanel.SetActive(true);
    }

    public void NextLevel()
    {
        LevelSelection.LevelNo++;

        if (LevelSelection.LevelNo >= 10)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }

        if (LevelSelection.LevelNo == 5)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
