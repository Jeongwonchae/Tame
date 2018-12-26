using UnityEngine;
using System.Collections;

class ProcManager : Modal{

    public override void Init()
    {
        GameObject.FindWithTag("Portal").AddComponent<portal>();
        UIFigureManager.GetInstance().Init();

        dist = PlayerManager.GetInstance().getPostion() - CameraManager.GetInstance().getPostion();

        pBeginPos = PlayerManager.GetInstance().getPostion();

        mainCamera = GameObject.Find("Camera").transform;

        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(0.5600829f, 0.2586876f, -0.0342989f);

        mainCamera.position = new Vector3(16.15f, 15.13f, 4.37f);
        mainCamera.rotation = Quaternion.Euler(39.5945f, 229.7335f, 3.6973f);

        
    }

    public override void Loop()
    {
        print("Loop");
        touchPlayer();
    }

}
