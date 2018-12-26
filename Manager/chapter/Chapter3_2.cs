using UnityEngine;
using System.Collections;

class Chapter3_2 : Modal
{
    GameObject[] middleTarget;

    public override void Init()
    {
        UIFigureManager.GetInstance().Init();

        dist = PlayerManager.GetInstance().getPostion() - CameraManager.GetInstance().getPostion();

        pBeginPos = PlayerManager.GetInstance().getPostion();

        mainCamera = GameObject.Find("Camera").transform;

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(-0.0009163022f, 0.07968765f, -0.4972997f);

        PlayerManager.GetInstance().getPlayerTransfrom().eulerAngles = new Vector3(0, 270f, 0);

        mainCamera.position = new Vector3(19.74f, 16.88f, 5.78f);
        mainCamera.rotation = Quaternion.Euler(39.80001f, 263.3007f, 0f);

        PlayerManager.GetInstance().getPlayerTransfrom().gameObject.SetActive(true);

        middleTarget = new GameObject[10];
        Vector3[] pos = {
                            new Vector3(0.015f, 0.086f + 0.484f, 2.481f),
                            new Vector3(-0.47f, 0.11f + 0.484f, 5.008f),
                            new Vector3(-0.468f, 0.08f + 0.484f, 10.498f),
                            new Vector3(-1.977f, 1.174f + 0.484f, 7.501f),       
                            new Vector3(-10.808f, 1.874f + 0.484f, 22.691f),    
                            new Vector3(-7.077f, 1.874f + 0.484f, 22.691f), 
                            new Vector3(-5.064f, 1.874f + 0.484f, 23.689f),   
                            new Vector3(-4.068f, 1.874f + 0.484f, 22.21f),       
                            new Vector3(-7.084f, 1.803f + 0.484f, 19.164f),       
                            new Vector3(-3.75f, 2.22f + 0.484f, 19.703f)      
        };
        for (int i = 0; i < middleTarget.Length; i++)
        {
            middleTarget[i] = (GameObject)Instantiate(Resources.Load("MiddleEffect"));
            middleTarget[i].transform.position = pos[i];
            middleTarget[i].AddComponent<BoxCollider>().isTrigger = true;
            middleTarget[i].GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.3f, 0.3f);
            middleTarget[i].GetComponent<BoxCollider>().center = new Vector3(0f, -0.3f, 0f);
            middleTarget[i].layer = 8 + i;

            if (i > 4)
            {
                middleTarget[i].SetActive(false);
            }
        }

    }

    bool isTrue = false;

    public override void Loop()
    {
        touchPlayer();

        if (MiddleTarget.GetInstance().getPlaying())
        {
            isTrue = true;
        }
        else
        {
            if (isTrue)
            {
                for (int i = 5 ; i < middleTarget.Length ; i++)
                {
                    middleTarget[i].SetActive(true);
                }
                isTrue = false;
            }
        }
    }

}
