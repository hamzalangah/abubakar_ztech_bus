using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using SWS;

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

    public GameObject[] FirstScene, MidScene, LastScene;

    [Header("CutScene Data")]
    public GameObject[] FirstScenePassangers;
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

        StartCoroutine(StartScene());

        if (LevelSelection.LevelNo > 4) return;
        passangers[LevelSelection.LevelNo].Bus_Passangers[VehicleSelection.vehicle_index].SetActive(true);
    }
    void ActiveLevels()
    {
        Levels[LevelSelection.LevelNo].SetActive(true);
    }

    IEnumerator StartScene()
    {
        StartCoroutine(PickingPassangers());
        FirstScene[LevelSelection.LevelNo].SetActive(true);
        yield return new WaitForSeconds(FirstSceneDuration[LevelSelection.LevelNo]);
        FirstScene[LevelSelection.LevelNo].SetActive(false);
        UIManager.instance.Controls.SetActive(true);
    }
    IEnumerator PickingPassangers()
    {
        if (LevelSelection.LevelNo == 0)
        {
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < FirstScenePassangers.Length; i++)
            {
                FirstScenePassangers[i].GetComponent<splineMove>().enabled = true;
                FirstScenePassangers[i].GetComponent<Animator>().SetBool("isWalking", true);
                yield return new WaitForSeconds(1f);

            }
        }
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
    [SerializeField] private float rotationSpeed = 1.0f;
}
