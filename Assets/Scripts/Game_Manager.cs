using DG.Tweening;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    [Header("Players_Data")]
    public GameObject player;
    public Transform MiniBus;
    [Header("Level_Data")]
    public LevelData[] levelsPickDrop;
    public LevelData[] levelsHardMode;
    public LevelData[] StuntMode;
    public int current_Level;
    [Header("GameThings")]
    public GameObject ControlBtns, Rcccam, CompletePanel, StartBtnPickDrop, StartHardMode, TrafficSystem;
    public GameObject NextButton, levelFailed, MobileDrag, Loading, Pause, BlackScreen;
    public Text levelno, totalcoins;
    public Camera RccCam;
    int i;
    [Header("sounds")]
    public AudioSource[] BGMusic;
    public AudioSource SeatSound;
    private FieldInfo orbitXField;
    private FieldInfo orbitYField;
    private Component rccCameraComponent;

    public void Awake()
    {
        Instance = this;
        InitializeRCCCameraReflection();
    }
    private void Start()
    {
        //MinimapRoutes.Instance.destinationPoint = levelsPickDrop[current_Level].Trigers.gameObject.transform.GetChild(0).gameObject.transform;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        TrafficSystem.SetActive(false);
        RCC.SetMobileController(RCC_Settings.MobileController.SteeringWheel);
        current_Level = PlayerPrefs.GetInt("Levels1", 0);
        StartCoroutine(TimeStartScene());
        AudioListener.volume = 1f;
        Time.timeScale = 1f;
        MiniBus = player.transform;
        levelsno();
        RcSoundOff();
    }
    void levelsno()
    {
        i = current_Level + 1;
        levelno.text = "LEVEL NO: " + i.ToString();
    }
    public IEnumerator TimeStartScene()
    {
        levelsPickDrop[current_Level].gameObject.SetActive(true);
        levelsPickDrop[current_Level].start_Scene.SetActive(true);
        yield return new WaitForSeconds(levelsPickDrop[current_Level].startSceneTime);
        MiniBus.SetPositionAndRotation(levelsPickDrop[current_Level].start_Position.position, levelsPickDrop[current_Level].start_Position.rotation);
        MiniBus.gameObject.SetActive(true);
        Time.timeScale = 1f;
        BlackScreen.SetActive(false);
        levelsPickDrop[current_Level].start_Scene.SetActive(false);
        StartBtnPickDrop.SetActive(true);
    }
    public IEnumerator PickPassangers()
    {
        RCC_Camera cam = Rcccam.GetComponent<RCC_Camera>();
        if (cam != null)
        {
            cam.cameraMode = RCC_Camera.CameraMode.TPS;
        }

        LevelData.Instance.Arrows.transform.GetChild(0).gameObject.SetActive(false);
        LevelData.Instance.Trigers.transform.GetChild(0).gameObject.SetActive(false);
        MiniBus.GetComponent<Rigidbody>().linearDamping = 5f;
        yield return new WaitForSeconds(1);
        float originalX = GetOrbitX();
        float originalY = GetOrbitY();
        DOTween.To(() => GetOrbitX(), x => SetOrbitX(x), 162f, 4f);
        DOTween.To(() => GetOrbitY(), y => SetOrbitY(y), -5f, 5f);
        yield return new WaitForSeconds(3);
        Player_Components.Instance.DorOpen.Play("dooropenv3");
        Player_Components.Instance.DorSound.Play();
        yield return new WaitForSeconds(1.5f);
        LevelData.Instance.EnterPer.SetActive(true);
        LevelData.Instance.idlePer.SetActive(false);
        yield return new WaitForSeconds(7.5f);
        LevelData.Instance.SittingPer.SetActive(true);
        LevelData.Instance.EnterPer.SetActive(false);
        Player_Components.Instance.DorOpen.Play("doorclose");
        Player_Components.Instance.DorSound.Play();
        yield return new WaitForSeconds(2.5f);
        MiniBus.GetComponent<Rigidbody>().linearDamping = 0.01f;
        DOTween.To(() => GetOrbitX(), x => SetOrbitX(x), originalX, 3f);
        DOTween.To(() => GetOrbitY(), y => SetOrbitY(y), originalY, 3f);
        MiniBus.GetComponent<Rigidbody>().isKinematic = false;
        LevelData.Instance.Arrows.transform.GetChild(1).gameObject.SetActive(true);
        LevelData.Instance.Trigers.transform.GetChild(1).gameObject.SetActive(true);
        ControlBtns.SetActive(true);
        Time.timeScale = 1f;
        BlackScreen.SetActive(false);
        //MinimapRoutes.Instance.destinationPoint = levelsPickDrop[current_Level].Trigers.gameObject.transform.GetChild(1).gameObject.transform;
    }

    public IEnumerator DropPassangers()
    {
        RCC_Camera cam = Rcccam.GetComponent<RCC_Camera>();
        if (cam != null)
        {
            cam.cameraMode = RCC_Camera.CameraMode.TPS;
        }
        TrafficSystem.SetActive(false);
        MobileDrag.SetActive(false);
        MiniBus.GetComponent<Rigidbody>().linearDamping = 5f;
        yield return new WaitForSeconds(1.5f);
        float originalX = GetOrbitX();
        float originalY = GetOrbitY();
        DOTween.To(() => GetOrbitX(), x => SetOrbitX(x), 165f, 4f);
        DOTween.To(() => GetOrbitY(), y => SetOrbitY(y), -5f, 5f);
        Player_Components.Instance.DorOpen.Play("dooropenv3");
        Player_Components.Instance.DorSound.Play();
        yield return new WaitForSeconds(4.5f);
        LevelData.Instance.ExitPer.SetActive(true);
        LevelData.Instance.SittingPer.SetActive(false);
        yield return new WaitForSeconds(8);
        Player_Components.Instance.DorOpen.Play("doorclose");
        Player_Components.Instance.DorSound.Play();
        var carController = MiniBus.GetComponent<RCC_CarControllerV3>();
        carController.maxEngineSoundVolume = 0;
        carController.minEngineSoundVolume = 0;
        carController.maxEngineSoundPitch = 0;
        carController.minEngineSoundPitch = 0;
        carController.idleEngineSoundVolume = 0;

        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") + 1000);
        totalcoins.text = PlayerPrefs.GetInt("TotalCash").ToString() + "$";
        Time.timeScale = 1f;
        BlackScreen.SetActive(false);

        if (current_Level >= 4)
        {
            NextButton.gameObject.SetActive(false);
        }
        else
        {
            NextButton.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);

        CompletePanel.SetActive(true);
        int nextLevel = current_Level + 1;
        if (nextLevel < 10 && PlayerPrefs.GetInt("Levels1" + nextLevel, 0) == 0)
        {
            PlayerPrefs.SetInt("Levels1" + nextLevel, 1);
        }
        PlayerPrefs.SetInt("Levels1", current_Level);
        PlayerPrefs.Save();
    }
    public void RcSoundOff()
    {
        var c = MiniBus.GetComponent<RCC_CarControllerV3>();
        c.maxEngineSoundVolume = c.minEngineSoundVolume = c.idleEngineSoundVolume = 0f;
    }
    public void RcSoundOn()
    {
        var c = MiniBus.GetComponent<RCC_CarControllerV3>();
        c.maxEngineSoundVolume = 1f;
        c.minEngineSoundVolume = 0.3f;
        c.idleEngineSoundVolume = 0.5f;
    }
    void VibratePhone()
    {
        Handheld.Vibrate();
    }
    public void startEnginPlay()
    {
        StartCoroutine(StartEngineFunction());
    }
    IEnumerator StartEngineFunction()
    {
        VibratePhone();
        SeatSound.Play();
        StartBtnPickDrop.SetActive(false);
        LevelData.Instance.Arrows.transform.GetChild(0).gameObject.SetActive(true);
        LevelData.Instance.idlePer.SetActive(true);
        MiniBus.transform.GetComponent<RCC_CarControllerV3>().StartEngine();
        ControlBtns.SetActive(true);
        ShakeCamera(0.6f, 0.2f);
        yield return new WaitForSeconds(1.5f);
        RcSoundOn();
        BGMusic[0].Play();
        TrafficSystem.SetActive(true);
        MobileDrag.SetActive(true);
        MiniBus.GetComponent<Rigidbody>().isKinematic = false;
        LevelData.Instance.Trigers.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Faild()
    {
        StartCoroutine(LvlFaild());
    }
    IEnumerator LvlFaild()
    {
        var c = MiniBus.GetComponent<RCC_CarControllerV3>();
        c.maxEngineSoundVolume = c.minEngineSoundVolume = c.idleEngineSoundVolume = 0f;
        ControlBtns.SetActive(false);
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        BlackScreen.SetActive(false);
        Loading.SetActive(true);
        yield return new WaitForSecondsRealtime(3.5f);
        Loading.SetActive(false);
        levelFailed.SetActive(true);
    }
    public void RestartBtn()
    {
        StartCoroutine(Restart());
    }
    IEnumerator Restart()
    {
        Time.timeScale = 1f;
        Loading.SetActive(true);
        yield return new WaitForSecondsRealtime(3.5f);
        SceneManager.LoadScene(2);
    }
    public void Nextbtn()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        Loading.SetActive(true);
        yield return new WaitForSecondsRealtime(3.5f);

        int nextLevel = current_Level + 1;
        current_Level = nextLevel;
        if (nextLevel < 10 && PlayerPrefs.GetInt("Levels1" + nextLevel, 0) == 0)
        {
            PlayerPrefs.SetInt("Levels1" + nextLevel, 1);
        }
        PlayerPrefs.SetInt("Levels1", current_Level);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void HomeBtn()
    {
        StartCoroutine(home());
    }
    IEnumerator home()
    {
        Time.timeScale = 1f;
        Loading.SetActive(true);
        yield return new WaitForSecondsRealtime(3.5f);
        SceneManager.LoadScene(1);
    }
    public void pauseBtn()
    {
        StartCoroutine(pause());
    }
    IEnumerator pause()
    {
        AudioListener.volume = 0f;
        ControlBtns.SetActive(false);
        Pause.SetActive(true);
        yield return new WaitForSeconds(0);
        Time.timeScale = 0f;
    }
    public void pauseoff()
    {
        Time.timeScale = 1f;
        ControlBtns.SetActive(true);
        Pause.SetActive(false);
        AudioListener.volume = 1f;
    }
    public void ShakeCamera(float duration, float strength)
    {
        if (RccCam != null)
        {
            Vector3 originalPos = RccCam.transform.localPosition;
            RccCam.transform.DOShakePosition(duration, strength).OnComplete(() =>
            {
                RccCam.transform.localPosition = originalPos;
            });
        }
    }
    private void InitializeRCCCameraReflection()
    {
        if (Rcccam != null)
        {
            rccCameraComponent = Rcccam.GetComponentInParent<RCC_Camera>() ?? Rcccam.GetComponent("RCC_Camera");
            if (rccCameraComponent != null)
            {
                System.Type cameraType = rccCameraComponent.GetType();
                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                orbitXField = cameraType.GetField("orbitX", flags) ?? cameraType.GetField("OrbitX", flags) ?? cameraType.GetField("tpsRotation", flags);
                orbitYField = cameraType.GetField("orbitY", flags) ?? cameraType.GetField("OrbitY", flags) ?? cameraType.GetField("tpsTilt", flags);
            }
        }
    }
    private float GetOrbitX()
    {
        if (orbitXField != null && rccCameraComponent != null)
        {
            return (float)orbitXField.GetValue(rccCameraComponent);
        }
        return 0f;
    }
    private float GetOrbitY()
    {
        if (orbitYField != null && rccCameraComponent != null)
        {
            return (float)orbitYField.GetValue(rccCameraComponent);
        }
        return 0f;
    }
    private void SetOrbitX(float value)
    {
        if (orbitXField != null && rccCameraComponent != null)
        {
            orbitXField.SetValue(rccCameraComponent, value);
        }
    }
    private void SetOrbitY(float value)
    {
        if (orbitYField != null && rccCameraComponent != null)
        {
            orbitYField.SetValue(rccCameraComponent, value);
        }
    }
}