using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

enum ProcState
{
    Loading,
    Processing,
    Destroying
}

public class Loader : MonoBehaviour {

    private static Loader instance;
    public static Loader GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(Loader)) as Loader;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    Scene sManager;

    Scene? curScene;

    ProcState cur_pState;

    public bool activateScenes = false;

    public void Awake()
    {
        cur_pState = ProcState.Destroying;

        curScene = null;

        //SceneManager.LoadScene("Chapter2-2", LoadSceneMode.Additive);
        //print(SceneManager.sceneCount);
        //print(SceneManager.sceneCountInBuildSettings);
        //curScene = SceneManager.GetSceneByName("Chapter2-2");
        //sManager = SceneManager.GetActiveScene();
        //print(SceneManager.GetActiveScene().name);
        //SceneManager.SetActiveScene(curScene);
        //print(SceneManager.GetActiveScene().name);

        StartCoroutine(LoadSceneAdd(Manager.GetInstance().getSceneName()));
    }
    void Start()
    {
        //SceneManager.SetActiveScene(curScene);
        //print(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string chapterName)
    {
        StartCoroutine(LoadSceneAdd(chapterName));
    }

    public int getProcState() { return (int)cur_pState; }

    public bool isProcessing() { return cur_pState == ProcState.Processing; }

    private IEnumerator UnloadScene()
    {
        print("Unload in!");
        if (SceneManager.UnloadScene(curScene.Value.name))
        {
            cur_pState = ProcState.Destroying;
            print("Scene Unload!");
            yield break;
        }
        print("Error : Scene Unload");
        yield break;
    }

    private IEnumerator LoadSceneAdd(string name)
    {

        if (curScene != null)
            StartCoroutine(UnloadScene());
        //SoundManager.getInstance.ESLoadMap();

        cur_pState = ProcState.Loading;
        AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while (async.progress < 0.999999f)
        {
            Debug.Log("Progress: " + async.progress + ", " + async.allowSceneActivation);
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        curScene = SceneManager.GetActiveScene();

        cur_pState = ProcState.Processing;
        Debug.Log("Scene activated!"); // The code never gets here.

        if (UIController.GetInstance().getCurChapter() == Manager.GetInstance().getSceneCount() + 1)
            TextManager.GetInstance().Init();

        FadeManager.GetInstance().SetFadeOut();
        //SoundManager.getInstance.StopLoopEvent();

        yield break;
    }
}
