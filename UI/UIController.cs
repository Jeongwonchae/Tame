using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
[System.Serializable]

//chapter figure 순서 list
public class _chapter
{
    public int[] figureList;
}

public class UIController : MonoBehaviour {

    private static UIController instance;
    public static UIController GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(UIController)) as UIController;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    //현재까지 사용한 도형 수
    int curCount;

    //현재 챕터 저장
    int curChapter = 6;

    //바뀔 UI정보 저장하는 변수
    RectTransform rt;

    Button myButton;

    //바뀔 UI담아놓는 변수
    public Sprite[] figure;
    
    //chapter Size
    public _chapter[] chapter;
    
    //바뀔 정보
    Image changeImage;

    GameObject UI;

    float beginAngle;

    public void incChapter() { curChapter++; }

    public void printList()
    {
        for (int i = 0 ; i < chapter[curChapter].figureList.Length ; i++)
        {

        }
    }



    Text Count;
    Text Limit;

    void Awake()
    {
        UI = GameObject.Find("UI").gameObject;

        Count = UI.transform.FindChild("InGameUIPenul").FindChild("count").GetComponent<Text>();
        Limit = UI.transform.FindChild("InGameUIPenul").FindChild("limit").GetComponent<Text>();
        
        myButton = GetComponent<Button>(); // <-- you get access to the button component here
        myButton.onClick.AddListener(myFunctionForOnClickEvent);
        changeImage = GetComponent<Image>();
        UIImageLoader();
    }

    public GameObject getUI() { return UI; }

    // Use this for initialization
    public void Init () {
        //초기화
        PlayerManager.GetInstance().setCharState((int)CharState.idle);

        rt = GetComponent<RectTransform>();

        beginAngle = PlayerManager.GetInstance().getPlayerTransfrom().eulerAngles.y;

        getChapterFigureList(curChapter);
        curCount = 0;
        Limit.text = "/ " + chapter[curChapter - (Manager.GetInstance().getSceneCount() + 1)].figureList.Length.ToString();
        changeFigure();

        //origin


    }

    public float getBeginAngle() { return beginAngle; }

    //도형 순서 리턴
    public int[] getChapterFigureList(int chapterInfo)
    {

        curChapter = chapterInfo;
        //int[] anArray = chapter[chapterInfo-1].figureList;
        int[] anArray = chapter[curChapter - (Manager.GetInstance().getSceneCount() +1)].figureList;

        return anArray;
    }

    void myFunctionForOnClickEvent()
    {
        figureTun();
    }

    //도형 이미지 세팅
    public bool setFigureImage(int spriteNum)
    {
        changeImage.sprite = figure[spriteNum];
        return true;
    }

    //현재 횟수 리턴
    public int getCurCount()
    {
        return curCount;
    }

    public int[] getFigureInfo(int figureNum)
    {
        int[] order = new int[4];
        switch(figureNum)
        {
            case 0:
                order = ZeroF();
                break;
            case 1:
                order = OneF();
                break;
            case 2:
                order = TwoF();
                break;
            //case 3:
            //    order = ThreeF();
            //    break;
            case 4:
                order = forF();
                break;

            case 5:
                order = fiveF();
                break;
        }

        return order;
    }

    public int getAngle()
    {
        Transform pt = PlayerManager.GetInstance().getPlayerTransfrom();
        Vector3 r = pt.eulerAngles;
        return  Mathf.Abs((int)rt.eulerAngles.z + (int)(r.y - beginAngle)) % 360; 
    }

    int[] ZeroF()
    {
        int angle = getAngle();
        int[] order = new int[4];

        if (angle <= 5)
            angle = 0;
        else if (angle % 10 == 9)
            angle = angle + 1;
        else if (angle % 10 == 1)
            angle = angle - 1;

        switch (angle)
        {
            case 360:
            case 0:
                order[0] = 2;
                order[1] = 1;
                order[2] = 1;
                order[3] = 2;   
                break;
            case 270: 
                order[0] = 1;
                order[1] = 2;
                order[2] = 1;
                order[3] = 2;                
                break;
            case 180:
               order[0] = 1;
                order[1] = 2;
                order[2] = 2;
                order[3] = 1;          
                break;
            case 90:
                order[0] = 2;
                order[1] = 1;
                order[2] = 2;
                order[3] = 1;            
                break;
        }


        return order;
    }

    int[] OneF()
    {
        int angle = getAngle();
        int[] order = new int[4];

        if (angle <= 5)
            angle = 0;
        else if (angle % 10 == 9)
            angle = angle + 1;
        else if (angle % 10 == 1)
            angle = angle - 1;

        print(angle);

        switch (angle)
        {
            case 360:
            case 0:
                order[0] = 2;
                order[1] = 1;
                order[2] = 1;
                order[3] = 2;                
                break;
            case 270:
                order[0] = 1;
                order[1] = 2;
                order[2] = 1;
                order[3] = 2;
                break;
            case 180:
                order[0] = 1;
                order[1] = 2;
                order[2] = 2;
                order[3] = 1;
                break;
            case 90:
                order[0] = 2;
                order[1] = 1;
                order[2] = 2;
                order[3] = 1;
                break;
        }

        return order;
    }

    int[] TwoF()
    {
        int angle = getAngle();
        int[] order = new int[4];

        if (angle <= 5)
            angle = 0;
        else if (angle % 10 == 9)
            angle = angle + 1;
        else if (angle % 10 == 1)
            angle = angle - 1;

        TestWonhyuk.GetInstance().setA(angle.ToString());
        switch (angle)
        {
            case 360:
            case 0:
            case 180:
                order[0] = 2;
                order[1] = 2;
                order[2] = 0;
                order[3] = 0;                                              
                break;
            case 270:
            case 90:
                order[0] = 0;
                order[1] = 0;
                order[2] = 2;
                order[3] = 2;                                   
                break;
        }
        return order;
    }

    //int[] ThreeF()
    //{
    //    int[] order = new int[4];
    //    int angle = 0;
    //    switch ((int)rt.eulerAngles.z)
    //    {
    //        case 0:
    //            break;
    //        case 90:
    //            break;
    //        case 180:
    //            break;
    //        case 270:
    //            break;
    //    }
    //    return order;
    //}

    int[] forF()
    {
        int[] order = new int[4];
        order[0] = 2;
        order[1] = 2;
        order[2] = 2;
        order[3] = 2;
        return order;
    }

    int[] fiveF()
    {
        int[] order = new int[4];
        int angle = (int)rt.eulerAngles.z;
        if (angle <= 5)
            angle = 0;
        switch (angle)
        {
            case 0:
                order[0] = 3;
                order[1] = 3;
                order[2] = 0;
                order[3] = 0;
                break;
            case 90:
                order[0] = 0;
                order[1] = 0;
                order[2] = 3;
                order[3] = 3;
                break;
            case 180:
                order[0] = 3;
                order[1] = 3;
                order[2] = 0;
                order[3] = 0;
                break;
            case 270:
                order[0] = 0;
                order[1] = 0;
                order[2] = 3;
                order[3] = 3;
                break;
        }
        return order;
    }

    //도형 바꾸기
    public bool changeFigure()
    {
        //-------------------
        //if (curCount < chapter[curChapter-1].figureList.Length)
        //{
        //    bool pass = false;
        //    do
        //    {
        //        pass = false;
        //        usingFigure[curCount] = Random.RandomRange(0, chapter[curChapter-1].figureList.Length);
        //        for (int i = 0; i < curCount; i++)
        //        {
        //            if (usingFigure[i] == usingFigure[curCount])
        //            {
        //                pass = true;
        //                break;
        //            }
        //        }

        //    } while (pass);
        //    InitFigure(chapter[curChapter-1].figureList[usingFigure[curCount++]]);
        //    return true;
        //}
        //-------------------
        //if (curCount < chapter[curChapter - 1].figureList.Length)
        //{
        //    InitFigure(chapter[curChapter - 1].figureList[curCount]);
        //    ObjectController.GetInstance().setTile(getFigureInfo(chapter[curChapter - 1].figureList[curCount++]));
        //    return true;
        //}
        if (curCount < chapter[curChapter - (Manager.GetInstance().getSceneCount() + 1)].figureList.Length)
        {
            setFigureImage(chapter[curChapter - (Manager.GetInstance().getSceneCount() + 1)].figureList[curCount]);
            ObjectController.GetInstance().setTile(getFigureInfo(chapter[curChapter - (Manager.GetInstance().getSceneCount() + 1)].figureList[curCount++]));
            Count.text = curCount.ToString();
            //for (int i = 0; i < chapter[curChapter - (Manager.GetInstance().getSceneCount() + 1)].figureList.Length; i++)
            //{
            //    print("i : " + i + " > " + chapter[curChapter - (Manager.GetInstance().getSceneCount() + 1)].figureList[i]);
            //}
            return true;
        }
        return false;
    }

    public int getCurFigureNum()
    {
        return chapter[curChapter- 1].figureList[curCount];
    }

    public void setcurCount(int _curCount) { curCount = _curCount; }

    //도형 턴
    public void figureTun()
    {
        Vector3 rotateZ;
        rotateZ = new Vector3(0, 0, rt.eulerAngles.z - 90);
        rt.rotation = Quaternion.Euler(rotateZ);
        ObjectController.GetInstance().setTile(getFigureInfo(chapter[curChapter - (Manager.GetInstance().getSceneCount() + 1)].figureList[curCount - 1]));
        ObjectController.GetInstance().setAlign();
        ObjectController.GetInstance().possibility();
        //t = getFigureInfo(chapter[curChapter - 1].figureList[usingFigure[curCount - 1]]);
    }

    public int getCurChapter() { return curChapter; }
    public void incCurChapter(int _incVal) { curChapter += _incVal; }
    public void incCurChapter() { curChapter++; }

    //도형 초기화.
    void InitFigure(int imgNum)
    {
        Vector3 rotateZ;
        rotateZ = new Vector3(0, 0, 0);
        setFigureImage(imgNum);
        rt.rotation = Quaternion.Euler(rotateZ);
    }

    void UIImageLoader()
    {
        
        string[,] imagePath = new string[,]{
            { "Back" },
            { "Title" },
            { "Sound" },
            { "Vibration" },
            { "Main" },
            { "Exit" },
            { "BackgroundSound" }
                };

        GameObject[] UIList = new GameObject[imagePath.Length];

        for (int i = 0; i < UIList.Length; i++)
        {
            string s1 = "dummy";
            //Debug.Log(imagePath[i, 0]);
            //Transform UITransformList = transform.Find("UI").transform;
            
            UIList[i] = GameObject.Find("UI").transform.FindChild("PauseWindow").FindChild(imagePath[i, 0]).gameObject;
            //UIList[i] = GameObject.Find("UI").transform.FindChild("PauseWindow").FindChild("Back").gameObject;
            //print(UIList[i].name);         

       }

        int[] empty = new int[10];

    }
}
