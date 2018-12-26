using UnityEngine;
using System.Collections;
using System;

enum GameState
{
    Init,
    Loop,
    Destroy,
    Loading
}

public class Manager : MonoBehaviour {

    private static Manager instance;
    public static Manager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(Manager)) as Manager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    Modal ProcModal;

    void Awake()
    {
        
    }

    
    void Start()
    {
        gameState = GameState.Init;
    }

    public void setGameState(int _state)
    {
        gameState = (GameState)_state;
    }

    GameState gameState;


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Loader.GetInstance().isProcessing())
            {
                //PlayerManager.GetInstance().useGravity();
                switch (gameState)
                {
                    case GameState.Init:
                        Init();
                        
                        break;
                    case GameState.Loop:
                        PlayerManager.GetInstance().playerMove();
                        if (Input.touchCount > 0)
                        {
                            if (Input.GetTouch(0).phase == TouchPhase.Began)
                            {
                            }
                        }
                        ProcModal.Loop();

                        //PlayerManager.GetInstance().playerMove();
                        break;
                    case GameState.Destroy:
                        ProcModal.Destroy(chapter[UIController.GetInstance().getCurChapter()]);
                        gameState = GameState.Loading;
                        break;
                    default:
                        break;
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public int getSceneCount() {

        int cnt = 0;

        for (int i = 0 ; i < UIController.GetInstance().getCurChapter()-1 ; i++)
        {
            if (chapter[i].IndexOf("S") == 0)
                cnt++;
        }

        return cnt;
    }

    public string getSceneName() { return chapter[UIController.GetInstance().getCurChapter() - 1]; }

    string[] chapter = { "SMain", "Scene1_1", "Scene1_2", "Chapter1_1", "Chapter1_2", "Chapter2_1", "Chapter2_2", "Scene2", "Scene3", "Scene5", "Chapter3_1", "Chapter3_2", "Scene6" };

    bool Init()
    {
        Type elementType = Type.GetType(chapter[UIController.GetInstance().getCurChapter() - 1]);
        ProcModal = (Modal)Activator.CreateInstance(elementType);

        ProcModal.Init();
        PlayerManager.GetInstance().getPlayerTransfrom().gameObject.SetActive(true);
        UIController.GetInstance().Init();
        if (!(chapter[UIController.GetInstance().getCurChapter() - 1].IndexOf("S") == 0))
        {
            //SoundManager.getInstance.BGSIngame();
            ObjectController.GetInstance().CreateEffectTile();
            ProcModal.setBeginPos();
            ProcModal.setBeginAngle();
            CameraManager.GetInstance().setSmooth(true);
        }
        else
        {
            //SoundManager.getInstance.BGSStop();
            CameraManager.GetInstance().setSmooth(false);
        }

        gameState = GameState.Loop;
        return true;
    }

    public Vector3 getBeginPos() { return ProcModal.getBeginPos(); }
    public Vector3 getBeginAngle() { return ProcModal.getBeginAngle(); }
}
//if (Input.GetTouch(0).phase == TouchPhase.Began)
//                        {
//                            UIFigureManager.GetInstance().Listener();

//                            if (RayManager.GetInstance().MainCameraToRay(out hit))
//                            {
//                                if (hit.transform.tag == "Character")
//                                {
//                                    AniController.GetInstance().setAniParameter(true);
//                                }
//                            }

//                        }