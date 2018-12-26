using UnityEngine;
using System.Collections;
using System.Linq.Expressions;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Reflection;
using UnityEngine.Events;

class MyClass
{
    public int DataFile { get; set; }

    public int _datafile;
}

public class ButtonClick : MonoBehaviour
{

    //Back Button
    Button[] button;

    GameObject p;
    GameObject ingmae;
    GameObject pauseImage;

    GameObject BgSound;

    public Sprite[] changeImage;

    bool[] b_ctButton;
    GameObject[] g_ctButton;

    bool isClick = false;
    GameObject[] r_childButton;

    bool returnButton;

    GameObject[] button2;

    void Awake()
    {

        string[] findText = {
                                "newGame", "callGame", "Start"
                            };
        button2 = new GameObject[findText.Length];

        for (int i = 0; i < findText.Length; i++)
        {
            button2[i] = GameObject.Find("UI").transform.FindChild("SceneText").transform.FindChild(findText[i]).gameObject;
        }

        button2[0].GetComponent<Button>().onClick.AddListener(Click);
        button2[2].GetComponent<Button>().onClick.AddListener(bStart);


        returnButton = false;

        string[] findButtonName = { "BackgroundSound", "Sound", "Vibration", "Back" };
        b_ctButton = new bool[findButtonName.Length-1];
        g_ctButton = new GameObject[findButtonName.Length];

        for (int i = 0; i < findButtonName.Length; i++ )
        {
            if (i < findButtonName.Length-1)
                b_ctButton[i] = true;
            g_ctButton[i] = GameObject.Find("UI").transform.FindChild("PauseWindow").transform.FindChild(findButtonName[i]).gameObject;
        }
        g_ctButton[0].GetComponent<Button>().onClick.AddListener(delegate { optionButtonClick(0); });
        g_ctButton[1].GetComponent<Button>().onClick.AddListener(delegate { optionButtonClick(1); });
        g_ctButton[2].GetComponent<Button>().onClick.AddListener(delegate { optionButtonClick(2); });
        g_ctButton[3].GetComponent<Button>().onClick.AddListener(backButtonClick);

        string[] returnChildButtonName = { "reset", "greturn", "background" };
        r_childButton = new GameObject[returnChildButtonName.Length];

        for (int i = 0 ; i < returnChildButtonName.Length ; i++)
        {
            r_childButton[i] = GameObject.Find("UI").transform.FindChild("InGameUIPenul").transform.FindChild("identify").transform.FindChild(returnChildButtonName[i]).gameObject;
        }

        r_childButton[0].GetComponent<Button>().onClick.AddListener(reset);
        r_childButton[1].GetComponent<Button>().onClick.AddListener(greturn);

        string[] findScriptName = {
                                      "return", "setting"
                                  };

        button = new Button[findScriptName.Length];

        for (int i = 0; i < findScriptName.Length; i ++ )
        {
            button[i] = GameObject.Find(findScriptName[i]).GetComponent<Button>();
        }

        button[0].onClick.AddListener(delegate { returnButtonAnswer(returnChildButtonName); });
        button[1].onClick.AddListener(settingButtonAnswer);

        //s_rt = GameObject.Find("setting1").GetComponent<RectTransform>();
        //r_rt = GameObject.Find("return1").GetComponent<RectTransform>();


        BgSound = GameObject.Find("Sound");

        ingmae = GameObject.Find("UI").transform.FindChild("InGameUIPenul").gameObject;
       
        //p = GameObject.Find("Camera").transform.FindChild("Plane").gameObject;
        pauseImage = GameObject.Find("UI").transform.FindChild("PauseWindow").gameObject;

    }

    public void Click()
    {
        isClick = !isClick;
        button2[2].SetActive(isClick);
    }

    public void bStart()
    {
        Manager.GetInstance().setGameState((int)GameState.Destroy);
        for (int i = 0; i < button2.Length; i++)
        {
            button2[i].SetActive(false);
        }
        GameObject.Find("UI").transform.FindChild("SceneText").transform.FindChild("Text").gameObject.SetActive(false);
        GameObject.Find("UI").transform.FindChild("SceneText").transform.FindChild("Image").gameObject.SetActive(false);
    }

    public void reset()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().position = Manager.GetInstance().getBeginPos();
        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(Manager.GetInstance().getBeginAngle());
        UIController.GetInstance().setcurCount(0);
        UIController.GetInstance().changeFigure();
        ObjectController.GetInstance().setAlign();
        returnButton = !returnButton;
        for (int i = 0; i < r_childButton.Length; i++)
        {
            r_childButton[i].SetActive(returnButton);
        }

    }

    public void greturn()
    {
        returnButton = !returnButton;
        for (int i = 0; i < r_childButton.Length; i++)
        {
            r_childButton[i].SetActive(returnButton);
        }
    }

    public void optionButtonClick(int buttonNum)
    {
        b_ctButton[buttonNum] = !b_ctButton[buttonNum];

        Image img = g_ctButton[buttonNum].GetComponent<Image>();
        if (b_ctButton[buttonNum])
        {
            //g_ctButton[buttonNum].GetComponent<AudioSource>().Play();
            img.sprite = changeImage[0 + buttonNum];
            
        }

        else
        {
            //BgSound.GetComponent<AudioSource>().Stop();
            img.sprite = changeImage[3 + buttonNum];
        }

        switch (buttonNum)
        {
            case 0:
                //SoundManager.getInstance.setBGSound(b_ctButton[buttonNum]);
                break;
            case 1:
                break;
            case 2:
                //SoundManager.getInstance.setEFFSound(b_ctButton[buttonNum]);
                break;
        }
    }
    public void backButtonClick()
    {
        Time.timeScale = 1;
        GameObject.Find("UI").transform.FindChild("InGameUIPenul").gameObject.SetActive(true);
        GameObject.Find("UI").transform.FindChild("PauseWindow").gameObject.SetActive(false);
        //GameObject.Find("Camera").transform.FindChild("Plane").gameObject.SetActive(false);
    }

    public void returnButtonAnswer(string[] buttonName)
    {
        returnButton = !returnButton;
        for (int i = 0; i < buttonName.Length; i++)
        {
            r_childButton[i].SetActive(returnButton);
        }
        
    }
    public void settingButtonAnswer()
    {
        Time.timeScale = 0;
        pauseImage.SetActive(true);
        ingmae.SetActive(false);
    }


    public void Fire(string name)
    {
        MethodInfo _method = this.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (_method != null)
        {
            //print("in1");
            //print(_method.Name);
            //_method.Invoke(this, null);
            //Delegate Fdelegate = Delegate.CreateDelegate(this.GetType(), _method);
            //MyDelegate mdelegate new MyDelegate(reutrnButtonAnswer);
            //button[0].onClick.AddListener((UnityAction)mdelegate);
            //if (method != null)
            //{
            //    print("in2");
            //    method.Method.Invoke(method.Target, new object[0]);
            //}
        }
    }
}
