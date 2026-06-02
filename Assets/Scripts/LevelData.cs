using UnityEngine;
public class LevelData : MonoBehaviour
{
    public static LevelData Instance;
    [Header("Levels")]
    public Transform start_Position;
    public GameObject start_Scene;
    public float startSceneTime;
    public GameObject startMid_Scene;
    public float startMidSceneTime;
    public GameObject Arrows;
    public GameObject VoiceText;
    public GameObject Trigers;
    public GameObject Shows;
    public GameObject idlePer, EnterPer, SittingPer, ExitPer;
    private void Awake()
    {
        Instance = this;
    }
}