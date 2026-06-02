using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScript : MonoBehaviour
{
    public GameObject panel1, panel2, loading;
    public void Start()
    {
        StartCoroutine(Fucnction());
    }
    IEnumerator Fucnction()
    {
        panel1.SetActive(true);
        yield return new WaitForSeconds(3);
        panel2.SetActive(true);
        panel1.SetActive(false);
        yield return new WaitForSeconds(3);
        loading.SetActive(true);
        panel2.SetActive(false);
        yield return new WaitForSeconds(3.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
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