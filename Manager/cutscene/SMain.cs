using UnityEngine;
using System.Collections;
using UnityEngine.UI;

class SMain : Modal
{
    public GameObject came;
    Vector3 StartPoint;
    Vector3 offset;


    GameObject[] button2;
    
    public override void Init()
    {
        
        
        came = GameObject.Find("UI").transform.FindChild("SceneText").transform.FindChild("Text").gameObject;
        UIController.GetInstance().getUI().transform.FindChild("InGameUIPenul").gameObject.SetActive(false);

        StartPoint = CameraManager.GetInstance().getPostion();

        CameraManager.GetInstance().getCamera().position = new Vector3(13.2f, 11.63f, 13.27f);
        CameraManager.GetInstance().getCamera().rotation = Quaternion.Euler(new Vector3(33.7409f, 225f, 0f));
        offset = CameraManager.GetInstance().getPostion() - new Vector3(15.2f, 14.01f, 15.27f);
        came.SetActive(true);

        string[] findText = {
                                "newGame", "callGame", "Start"
                            };
        button2 = new GameObject[findText.Length];

        for (int i = 0; i < findText.Length; i++)
        {
            button2[i] = GameObject.Find("UI").transform.FindChild("SceneText").transform.FindChild(findText[i]).gameObject;
        }
        
    }

    public override void Loop()
    {
        if (Vector3.Distance(new Vector3(15.2f, 14.01f, 15.27f), StartPoint) > Vector3.Distance(StartPoint, CameraManager.GetInstance().getPostion()))
        {
            CameraManager.GetInstance().setPostion(CameraManager.GetInstance().getPostion() - offset * Time.deltaTime * 0.5f);

        }
        else
        {
            for (int i = 0; i < button2.Length - 1; i++)
            {
                button2[i].SetActive(true);
            }
            GameObject.Find("UI").transform.FindChild("SceneText").transform.FindChild("Image").gameObject.SetActive(false);
            came.SetActive(false);

            
        }
    }
}
