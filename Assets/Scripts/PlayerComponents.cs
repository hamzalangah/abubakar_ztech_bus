using DG.Tweening;
using UnityEngine;
public class Player_Components : MonoBehaviour
{
    public static Player_Components Instance;
    public Transform player;
    public Animator DorOpen;
    public AudioSource DorSound;
    public void Awake()
    {
        Instance = this;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pick"))
        {
            Transform pickPoint = other.transform.Find("PickPoint");
            Game_Manager.Instance.MiniBus.GetComponent<Rigidbody>().isKinematic = true;
            Game_Manager.Instance.MiniBus.transform.DOMove(pickPoint.position, 1f);
            Game_Manager.Instance.MiniBus.transform.DORotateQuaternion(pickPoint.rotation, 1f);
            Game_Manager.Instance.ControlBtns.SetActive(false);
            StartCoroutine(Game_Manager.Instance.PickPassangers());
            other.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("falseStart"))
        {
            LevelData.Instance.Shows.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("ShowMid"))
        {
            LevelData.Instance.Shows.transform.GetChild(1).gameObject.SetActive(true);
            other.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("FalseMid"))
        {
            LevelData.Instance.Shows.transform.GetChild(1).gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("ShowEnd"))
        {
            LevelData.Instance.Shows.transform.GetChild(2).gameObject.SetActive(true);
            other.gameObject.SetActive(false);
        }

        else if (other.CompareTag("Samandar"))
        {
            StopCameraFollow();
            Rigidbody rb = Game_Manager.Instance.MiniBus.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(Vector3.down * 800f, ForceMode.Impulse);
            Game_Manager.Instance.Faild();
        }

        else if (other.gameObject.CompareTag("EndDrop"))
        {
            Transform pickPoint = other.transform.Find("PickPoint");
            Game_Manager.Instance.MiniBus.GetComponent<Rigidbody>().isKinematic = true;
            Game_Manager.Instance.MiniBus.transform.DOMove(pickPoint.position, 1f);
            Game_Manager.Instance.MiniBus.transform.DORotateQuaternion(pickPoint.rotation, 1f);
            Game_Manager.Instance.ControlBtns.SetActive(false);
            StartCoroutine(Game_Manager.Instance.DropPassangers());
            other.gameObject.SetActive(false);
        }
    }
    public void StopCameraFollow()
    {
        if (Game_Manager.Instance.Rcccam != null)
        {
            var camScript = Game_Manager.Instance.Rcccam.GetComponent("RCC_Camera");
            if (camScript != null)
            {
                ((MonoBehaviour)camScript).enabled = false;
            }
        }
    }
}