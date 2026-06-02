using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject WinPanel, LoadingPanel, LossPanel, watchScenePanel;

    void Awake()
    {
        if (instance == null) instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
