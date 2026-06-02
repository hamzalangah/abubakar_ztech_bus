using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelection : MonoBehaviour
{
    public static int LevelNo;
    public GameObject LoadingPanel;
    public GameObject[] AllPanels;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void SelectLevel(int level)
    {
        LevelNo = level;
        StartCoroutine(GamePlay());
    }
    IEnumerator GamePlay()
    {
        LoadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GamePlay");
    }
    public void ModeSelection()
    {
        foreach(GameObject panels in AllPanels)
        {
            panels.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
