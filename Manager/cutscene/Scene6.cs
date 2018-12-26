using UnityEngine;
using System.Collections;

class Scene6 : Modal
{
    float lateTime = 0;

    bool middleEvent = false;

    public override void Init()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(0.2800837f, 0.4276876f, -0.3592997f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 270f, 0.0f);

        CameraManager.GetInstance().setPostion(new Vector3(0.7f, 1.78f, 9.2f));
        CameraManager.GetInstance().setAngle(Quaternion.Euler(8.2f, 180.0015f, 6.581058e-12f));
    }
    public override void Loop()
    {
        AniController.GetInstance().setJAniParameter(true);
        AniController.GetInstance().setAniSpeed(0.50f);
          
    }

}
