using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelControlManager : MonoBehaviour
{
    public GameObject[] Mode1levelButtons, Mode2levelButtons, Mode1LockBtns, Mode2LockBtns;
    public bool m1, m2 = false;
    public GameObject loading;
    public Image FillImage;
    public static LevelControlManager Instance;
    void Start()
    {
        Instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        LoadUnlockedLevels();
        LoadUnlockedLevels2();
    }
    private void LoadUnlockedLevels()
    {
        for (int i = 0; i < Mode1LockBtns.Length; i++)
        {
            if (PlayerPrefs.GetInt("Levels1" + i, 0) == 1)
            {
                Mode1LockBtns[i].SetActive(false);
                Mode1levelButtons[i].SetActive(true);
            }
        }
    }
    private void LoadUnlockedLevels2()
    {
        for (int i = 0; i < Mode2LockBtns.Length; i++)
        {
            if (PlayerPrefs.GetInt("Levels2" + i, 0) == 1)
            {
                Mode2LockBtns[i].SetActive(false);
                Mode2levelButtons[i].SetActive(true);
            }
        }
    }
    public void unlock(int levelIndex1)
    {
        if (levelIndex1 >= 0 && levelIndex1 < Mode1LockBtns.Length)
        {
            Mode1LockBtns[levelIndex1].SetActive(false);
            Mode1levelButtons[levelIndex1].SetActive(true);
        }
    }
    public void unlock2(int levelIndex2)
    {
        if (levelIndex2 >= 0 && levelIndex2 < Mode2LockBtns.Length)
        {
            Mode2LockBtns[levelIndex2].SetActive(false);
            Mode2levelButtons[levelIndex2].SetActive(true);
        }
    }
    public void SelectLevelMode1(int i)
    {
        PlayerPrefs.SetInt("Levels1", i);
        StartCoroutine(LoadingProces());
    }
    public void SelectLevelMode2(int i)
    {
        PlayerPrefs.SetInt("Levels2", i);
        StartCoroutine(LoadingProces2());
    }
    public void LockBtnFunction(int Temp)
    {
        NetworkReachability reachability = Application.internetReachability;

        if (reachability != NetworkReachability.NotReachable)
        {
            Debug.Log("Internet Connection Available");

            //if (AdsController.Instance)
            {
                if (m1)
                {
                    //AdsController.Instance.ShowRewardedInterstitialAd_Admob(() =>
                    {
                        UnlockLevel(Temp);
                    }/*)*/
                    ;
                }
                else
                {
                    //AdsController.Instance.ShowRewardedInterstitialAd_Admob(() =>
                    {
                        UnlockLevel2(Temp);
                    }/*)*/
                    ;
                }
            }
        }
    }

    private void UnlockLevel(int levelIndex1)
    {
        if (levelIndex1 >= 0 && levelIndex1 < Mode1LockBtns.Length)
        {
            unlock(levelIndex1);
            PlayerPrefs.SetInt("Levels1" + levelIndex1, 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("Unknown index value for Mode1LockBtns: " + levelIndex1);
        }
    }
    private void UnlockLevel2(int levelIndex2)
    {
        if (levelIndex2 >= 0 && levelIndex2 < Mode2LockBtns.Length)
        {
            unlock2(levelIndex2);
            PlayerPrefs.SetInt("Levels2" + levelIndex2, 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("Unknown index value for Mode2LockBtns: " + levelIndex2);
        }
    }
    public void mode1()
    {
        m1 = true;
        m2 = false;
    }
    public void mode2()
    {
        m1 = false;
        m2 = true;
    }
    public IEnumerator LoadingProces()
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(3f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public IEnumerator LoadingProces2()
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(3);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}