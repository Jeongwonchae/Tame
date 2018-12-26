using UnityEngine;
using System.Collections;

public class Modal : MonoBehaviour
{
    protected Transform mainCamera;

    protected Vector3 dist;

    protected RaycastHit hit;

    protected Vector3 pBeginPos;
    protected Vector3 pBeginAngle;

    public virtual void Init() { }
    public virtual void Loop() { }
    public void Destroy(string _sceneName)
    {
        FadeManager.GetInstance().SetFadeIn(_sceneName);
    }

    public void setBeginPos() { pBeginPos = PlayerManager.GetInstance().getPostion(); }
    public void setBeginAngle() { pBeginAngle = PlayerManager.GetInstance().getPlayerTransfrom().eulerAngles; }
    public Vector3 getBeginPos() { return pBeginPos; }
    public Vector3 getBeginAngle() { return pBeginAngle; }

    public void touchPlayer()
    {
        if (Input.touchCount > 0)
        {
            RaycastHit hit;

            if (RayManager.GetInstance().MainCameraToRay(out hit))
            {
                if (hit.transform.tag == "Character")
                {
                    if (ObjectController.GetInstance().getpossible())
                        AniController.GetInstance().setAniParameter(true);
                }
            }
        }

    }
}
