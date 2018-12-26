using UnityEngine;
using System.Collections;

class Scene5 : Modal
{
    float lateTime = 0;

    bool middleEvent = false;

    public override void Init()
    {
        PlayerManager.GetInstance().getPlayerTransfrom().position = new Vector3(0.503f, 0f, 0.515f);

        PlayerManager.GetInstance().getPlayerTransfrom().rotation = Quaternion.Euler(0.0f, 270f, 0.0f);

        CameraManager.GetInstance().setPostion(new Vector3(0.7f, 1.78f, 9.2f));
        CameraManager.GetInstance().setAngle(Quaternion.Euler(8.2f, 180.0015f, 6.581058e-12f));
    }
    public override void Loop()
    {
        if (!middleEvent)
        {
            AniController.GetInstance().setLookDownAniParameter(true);
            AniController.GetInstance().setAniSpeed(0.50f);
            middleEvent = true;
        }
        else
        {
            if (Input.touchCount > 0)
            {
                RaycastHit hit;

                if (RayManager.GetInstance().MainCameraToRay(out hit))
                {
                    if (hit.transform.tag == "Character")
                    {
                        AniController.GetInstance().setLookDownAniParameter(false);
                    }

                }
            }
        }
    }

}
