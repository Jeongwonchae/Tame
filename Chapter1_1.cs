using UnityEngine;
using System.Collections;

class Chapter1_1 : Modal{

    bool a = false;

    public override void Init()
    {
        UIController.GetInstance().getUI().transform.FindChild("InGameUIPenul").gameObject.SetActive(true);
        CameraManager.GetInstance().getCamera().GetComponent<Camera>().fieldOfView = 11;
        UIFigureManager.GetInstance().Init();

        dist = PlayerManager.GetInstance().getPostion() - CameraManager.GetInstance().getPostion();

        pBeginPos = PlayerManager.GetInstance().getPostion();

        mainCamera = GameObject.Find("Camera").transform;

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(1.002084f, 0.072f, 3.0047f);

        PlayerManager.GetInstance().getPlayerTransfrom().eulerAngles = new Vector3(0,90.0f,0);

        mainCamera.position = new Vector3(-13.01f, 14.45f, -9.65f);
        mainCamera.rotation = Quaternion.Euler(39.5945f, 49.7335f, 3.6973f);

        PlayerManager.GetInstance().getPlayerTransfrom().gameObject.SetActive(true);

    }

    public override void Loop()
    {

        touchPlayer();
    }

}
