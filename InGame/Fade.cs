using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour
{

    private static FadeManager instance;
    public static FadeManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(FadeManager)) as FadeManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

	// Use this for initialization
    Renderer rend;

    Transform Tr;

    bool isPlay;

    void Awake()
    {
        Tr = GameObject.Find("UI").transform.FindChild("Fade").transform;
    }

    public void SetFadeIn(string name)
    {
        _SetActive(true);
        StartCoroutine(FadeIn(name));
    }

    public void SetFadeOut()
    {
        _SetActive(true);
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn(string name)
    {
        while (isPlay)
        {
            yield return null;
        }
        CanvasGroup canvasGroup = Tr.GetComponent<CanvasGroup>();
        
        isPlay = true;

        while (canvasGroup.alpha < 1)
        {

            canvasGroup.alpha += Time.deltaTime / 2;
            yield return null;
        }
        isPlay = false;

        Manager.GetInstance().setGameState((int)GameState.Init);

        Loader.GetInstance().LoadScene(name);

        PlayerManager.GetInstance().getPlayerTransfrom().gameObject.SetActive(false);
        UIController.GetInstance().incCurChapter();
    }

    IEnumerator FadeOut()
    {
        while (isPlay)
        {
            yield return null;
        }
        CanvasGroup canvasGroup = Tr.GetComponent<CanvasGroup>();

        isPlay = true;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        isPlay = false;
        _SetActive(false);
    }

    public void _SetActive(bool _set)
    {
        Tr.gameObject.SetActive(_set);
    }
}
