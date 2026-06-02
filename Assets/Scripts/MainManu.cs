using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public static int ModeNo;
    public GameObject Mainmenu, profilePanel, CompleteProfile, WelcomePrivacy, countrypael, settingpnl, ExitPanel;
    public Button Next;
    public GameObject[] ModeLevels, Loadings, CityLock, OffroadLock;
    [Header("DailyReward")]
    public Text totalcoins;
    public GameObject[] profiles, flags, genders;
    public Button continueButtonForFlags;
    public Image[] selectedProfileImages;
    public Image[] selectedFlagImages;
    public InputField nameInputField;
    public GameObject AgeInputField;
    public Button Nextprf;
    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        VehicleSelection.vehicle_index = 0;
        //PlayerPrefs.SetInt("Citydriving", 9);
        //PlayerPrefs.SetInt("offroaddriving", 9);
        //if (PlayerPrefs.GetInt("Once") == 0)
        //{
        //    WelcomePrivacy.SetActive(true);
        //    PlayerPrefs.SetInt("Once", 1);
        //    PlayerPrefs.Save();
        //}
        //else
        //{
        //    Mainmenu.SetActive(true);
        //    profilePanel.SetActive(false);
        //}
        //Time.timeScale = 1;
        //AudioListener.volume = 1;
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //totalcoins.text = PlayerPrefs.GetInt("TotalCash").ToString();
        ////Profile Flags
        //flags[PlayerPrefs.GetInt("SelectedFlagIndex")].transform.GetChild(0).gameObject.SetActive(true);
        //profiles[PlayerPrefs.GetInt("CurrentProfile")].transform.GetChild(0).gameObject.SetActive(true);

    }
    public void ModeSelection() => StartCoroutine(ModeSelectionC());

    IEnumerator ModeSelectionC()
    {
        Loadings[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Garage");
    }
}
  /*  public void ShowRandomLoadingPanel()
    {
        foreach (GameObject panel in Loadings)
        {
            panel.SetActive(false);
        }
        int randomIndex = Random.Range(0, Loadings.Length);

        Loadings[randomIndex].SetActive(true);
    }
    public void HideAllLoadingPanels()
    {
        foreach (GameObject panel in Loadings)
        {
            panel.SetActive(false);
        }
    }
    public void StartLoadingProcess()
    {
        ShowRandomLoadingPanel();
        StartCoroutine(LoadingProcess());
    }
    private IEnumerator LoadingProcess()
    {
        yield return new WaitForSeconds(3.5f);
        HideAllLoadingPanels();
    }
    public void StartLoadingProcessRect()
    {
        ShowRandomLoadingPanel();
        StartCoroutine(LoadingProcessRect());
    }
    private IEnumerator LoadingProcessRect()
    {
        yield return new WaitForSeconds(3.5f);
        HideAllLoadingPanels();
    }
    //Play button
    public void ModeOpenMainManuClose()
    {
        StartCoroutine(Play());
    }
    IEnumerator Play()
    {
        Loadings[1].SetActive(true);
        yield return new WaitForSeconds(3.5f);
        Loadings[1].SetActive(false);
        Mainmenu.SetActive(false);
        ModeSelection.SetActive(true);
    }
    //Mode Button & Selection
    public void Modeno(int modeno)
    {
        ModeNo = modeno;
    }
    public void ModeSelectionBtn()
    {
        StartCoroutine(modeselectionbtn());
    }
    IEnumerator modeselectionbtn()
    {
        if (ModeNo == 0)
        {
            yield return new WaitForSeconds(0);
            CityModeOpen();
        }
        if (ModeNo == 1)
        {
            OffroadModeOpen();
        }
    }
    public void welcomebtn()
    {
        StartCoroutine(Welcome());
    }
    IEnumerator Welcome()
    {
        yield return new WaitForSeconds(0);
    }
    public void profileOpenCntryClose()
    {
        StartCoroutine(profileb());
    }
    IEnumerator profileb()
    {
        StartLoadingProcess();
        yield return new WaitForSeconds(3.5f);
        countrypael.SetActive(false);
        profilePanel.SetActive(true);
    }
    public void CntryPnlOpenPrivacyClos()
    {
        WelcomePrivacy.SetActive(false);
        countrypael.SetActive(true);
    }
    public void Exitmodeselection()
    {
        StartCoroutine("exitmodeselection");
    }
    IEnumerator exitmodeselection()
    {
        ModeSelection.SetActive(false);
        yield return new WaitForSeconds(0);
        Mainmenu.SetActive(true);
    }
    public void ADMainMenu()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.rc.airplane.simulator.game.flight.simulator.airplane.games");
    }
    public void AdModeSelectio()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.rc.car.simulator.car.games");
    }
    public void Privacy()
    {
        Application.OpenURL("https://rexposutdio.blogspot.com/2025/09/privacy-policy.html");
    }
    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=7670823250668908548");
    }
    public void Rateus()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.rc.flying.bus.bus.simulator.game");
    }
    //Back Btns
    public void BackToModeSelection()
    {
        StartCoroutine(backtomodeselection());
    }
    IEnumerator backtomodeselection()
    {
        foreach (GameObject w in ModeLevels)
        {
            w.SetActive(false);
        }
        StartLoadingProcess();
        yield return new WaitForSeconds(0);
        ModeSelection.SetActive(true);
    }
    //Back to MainMenu
    public void BackToMainMenu()
    {
        StartCoroutine(backtomainmenu());
    }
    public void CityModeOpen()
    {
        StartCoroutine(citymode());
    }
    IEnumerator citymode()
    {
        StartLoadingProcess();
        yield return new WaitForSeconds(3.5f);
        ModeSelection.SetActive(false);
        ModeLevels[0].SetActive(true);
    }
    public void OffroadModeOpen()
    {
        StartCoroutine(OffroadMode());
    }
    IEnumerator OffroadMode()
    {
        StartLoadingProcess();
        yield return new WaitForSeconds(0);
        ModeSelection.SetActive(false);
        ModeLevels[1].SetActive(true);
    }
    IEnumerator backtomainmenu()
    {
        StartLoadingProcess();
        ModeSelection.SetActive(false);
        yield return new WaitForSeconds(0);
        Mainmenu.SetActive(true);
    }
    public void Steering()
    {
        RCC.SetMobileController(RCC_Settings.MobileController.SteeringWheel);
    }
    public void Buttens()
    {
        RCC.SetMobileController(RCC_Settings.MobileController.TouchScreen);
    }
    public void Tilt()
    {
        RCC.SetMobileController(RCC_Settings.MobileController.Gyro);
    }
    public void exit()
    {
        Application.Quit();
    }
    public void MainManuOpenProfileClos()
    {
        StartCoroutine(NextBtnFunction());
    }
    IEnumerator NextBtnFunction()
    {
        StartLoadingProcess();
        yield return new WaitForSeconds(3.5f);
        CompleteProfile.SetActive(false);
        Mainmenu.SetActive(true);
    }
    public void comPletOpenProfileClos()
    {
        StartCoroutine(NextBtnComplete());
    }
    IEnumerator NextBtnComplete()
    {
        yield return new WaitForSeconds(0f);
        profilePanel.SetActive(false);
        CompleteProfile.SetActive(true);
    }
    public void ExitPnlOpenManuClose()
    {
        StartCoroutine(Ext());
    }
    IEnumerator Ext()
    {
        Loadings[0].SetActive(true);
        yield return new WaitForSeconds(0);
        Loadings[0].SetActive(false);
        ExitPanel.SetActive(true);
        Mainmenu.SetActive(false);
    }
    public void setingPnlOpenManuClose()
    {
        Mainmenu.SetActive(false);
        settingpnl.SetActive(true);
    }
    public void ExitPanelOff()
    {
        ExitPanel.SetActive(false);
        Mainmenu.SetActive(true);
    }
    public void SettingPanelOff()
    {
        StartCoroutine(setingband());
    }
    IEnumerator setingband()
    {
        StartLoadingProcess();
        yield return new WaitForSeconds(3.5f);
        settingpnl.SetActive(false);
        Mainmenu.SetActive(true);
    }
    public void OnProfileClick(int profileIndex)
    {
        for (int i = 0; i < profiles.Length; i++)
        {
            profiles[i].transform.GetChild(0).gameObject.SetActive(i == profileIndex);
            profiles[i].GetComponent<Button>().enabled = (i != profileIndex);
        }

        foreach (Image img in selectedProfileImages)
        {
            img.sprite = profiles[profileIndex].GetComponent<Image>().sprite;
        }

        PlayerPrefs.SetInt("CurrentProfile", profileIndex);
        PlayerPrefs.Save();
    }
    public void SelectFlagButton(int Index)
    {
        foreach (Image img in selectedFlagImages)
        {
            img.sprite = flags[Index].GetComponent<Image>().sprite;
        }

        for (int i = 0; i < flags.Length; i++)
        {
            flags[i].transform.GetChild(0).gameObject.SetActive(i == Index);
            flags[i].GetComponent<Button>().enabled = (i != Index);
        }
        PlayerPrefs.SetInt("SelectedFlagIndex", Index);
        PlayerPrefs.Save();
    }
    public void ValidateNameInput(string value)
    {
        string filtered = "";
        foreach (char c in value)
        {
            if (char.IsLetter(c))
            {
                filtered += c;
            }
        }
        if (nameInputField.text != filtered)
            nameInputField.text = filtered;
    }
    public void ValidateAgeInput(string value)
    {
        string filtered = "";
        foreach (char c in value)
        {
            if (char.IsDigit(c))
            {
                filtered += c;
            }
        }
        if (filtered.Length > 2)
            filtered = filtered.Substring(0, 2);
        if (AgeInputField.GetComponent<InputField>().text != filtered)
            AgeInputField.GetComponent<InputField>().text = filtered;
    }
}*/