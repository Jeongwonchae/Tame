using UnityEngine;
using System.Collections;

class SIntro : Modal
{
    public GameObject came;
    Vector3 offset;
    public override void Init()
    {
        UIController.GetInstance().getUI().SetActive(false);

        offset = CameraManager.GetInstance().getPostion() - new Vector3(15.2f, 14.01f, 15.27f);

    }

    public override void Loop()
    {
        if (Input.touchCount > 0)
        {
            UIController.GetInstance().getUI().SetActive(true);
            Manager.GetInstance().setGameState((int)GameState.Destroy);

            CameraManager.GetInstance().setPostion(CameraManager.GetInstance().getPostion() + offset * Time.deltaTime);

            print("in");

        }
    }

}
