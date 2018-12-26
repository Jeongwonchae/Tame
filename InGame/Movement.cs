using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum RState{
    idle,
    left,
    right
}

public class Movement : MonoBehaviour
{

    private Transform playerTr;

    Vector3 startPoint;

    Vector3 rayDir;

    private float moveDistance;

    private bool rotateTile;

    RState rState;

    int tileCount;

    Vector3? endPos;

    bool bBezier;

    Vector3? text1;

    bool bLeft;
    bool bRight;

    float moveSpeed;

    int iPos;


    //움직임 경로 저장하는 곳
    Vector3[] tPos;

    Vector3 x1;
    Vector3 x2;
    Vector3 x3;

    Vector3 rayD;

    float rot;
    // Use this for initialization

    public void setSpeed(float speed) { moveSpeed = speed; }

    
    void Awake()
    {
        
        moveSpeed = 0.79f;
        //움직임 카운트
        iPos = 0;
        rayDir = new Vector3(1, -0.5f, 0);

        bLeft = false;
        bRight = false;

        bBezier = false;

        endPos = null;

        rState = RState.idle;

        rotateTile = false;
        playerTr = GetComponent<Transform>();
        startPoint = playerTr.position;
        moveDistance = 0f;

        text1 = null;

        rState = RState.left;
        tileCount = 0;

        rayD = new Vector3(0, -1, 0);

    }

    // Update is called once per frame
    static int i = 0;
    static int j = 0;
    public void MoveUpdate()
    {
        
        string check;

        if (endPos == null)
        {
            check = "null";
        }
        else
        {
            check = endPos.ToString();
        }
        RaycastHit hitInfo;

        if (endPos != null)
        {
            Vector3 p = (Vector3)endPos;         
        }

        if (!bBezier)
        {

            if (endPos != null)
            {

                if (!(bLeft == true && bRight == true))
                {
                    if (Physics.Raycast(playerTr.position, (playerTr.up * -1), out hitInfo, 10f))
                    {
                        //--------------------distance check to change (
                        if (endPos != hitInfo.transform.position)
                        {
                            bBezier = true;
                            PlayerManager.GetInstance().setCharState((int)CharState.idle);
                            tPos = M(playerTr.position, 30);
                            StartCoroutine("Other");
                        }
                    }
                }
            }
        }

        GetObjectInfo();

        Move(rState, 3, 3);

        if (b)
        {
            if (AniController.GetInstance().checkAniPlaying("idle"))
            {
                ObjectController.GetInstance().setActive(false);
                b = false;
            }
        }

    }
    bool a = false;

    bool b = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MiddleTarget")
        {
            //SoundManager.getInstance.ESMiddleTarget();
            MiddleTarget.GetInstance().startAction(other.gameObject.layer);
            b = true;
        }

        if (other.tag == "Portal")
        {
            print("in");
            Manager.GetInstance().setGameState((int)GameState.Destroy);
        }

        if (other.tag == "Event")
        {

            if (other.name == "cutSceneEvent")
                PlayerManager.GetInstance().setCharState((int)CharState.lookup);
            else if (other.name == "endSceneEvent")
            {
                Manager.GetInstance().setGameState((int)GameState.Destroy);
                other.gameObject.SetActive(false);
                print("tagInCompare");
            }
            else if (other.name == "endEvent")
            {
                PlayerManager.GetInstance().setCharState((int)CharState.idle);
                PlayerManager.GetInstance().getPlayerTransfrom().GetComponent<Movement>().setMoveSpeed(0.79f);
                ObjectController.GetInstance().setActive(true);
            }
            if (other.name == "gameEvent")
            {
                a = true;
            }
        }
    }

    public bool getA() { return a; }

    bool GetObjectInfo()
    {
        if (PlayerManager.GetInstance().getCharState() == (int)CharState.idle)
        {
            if (Input.touchCount > 0)
            {

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (RayManager.GetInstance().MainCameraToRay("Character"))
                    {
                        if (ObjectController.GetInstance().getpossible())
                        {
                            rot = 0f;
                            endPos = null;
                            bLeft = false;
                            bRight = false;
                            iPos = 0;
                            startPoint = playerTr.position;
                            PlayerManager.GetInstance().setCharState((int)CharState.walk);
                            //ObjectController.GetInstance().setActive(false);
                            return true;
                        }
                    }
                }
                return true;
            }
        }

        return false;
    }

    IEnumerator Other()
    {
        float addAngle = (float)(90/tPos.Length);

        Vector3 rot;

        for (int i = 0; i < tPos.Length; i++ )
        {
            playerTr.position = tPos[i];
            if (rState == RState.right)
                rot = playerTr.eulerAngles + Vector3.up * addAngle;
            else
                rot = playerTr.eulerAngles + Vector3.up * -addAngle;

            playerTr.rotation = Quaternion.Euler(rot);
            yield return new WaitForSeconds(.01f);
        }

        //    playerTr.position = tPos[iPos++];
        //if (rState == RState.right)
        //    rot = playerTr.eulerAngles + Vector3.up * addAngle;
        //else
        //    rot = playerTr.eulerAngles + Vector3.up * -addAngle;

        //playerTr.rotation = Quaternion.Euler(rot);

        //yield return new WaitForSeconds(.01f);
        //if (iPos < tPos.Length)
        //{
        //    StartCoroutine("Other");
        //}

        PlayerManager.GetInstance().setCharState((int)CharState.walk);
        endPos = null;
        bBezier = false;
        bLeft = false;
        bRight = false;
        iPos = 0;
        //ObjectController.GetInstance().setTile(UIController.GetInstance().getFigureInfo (UIController.GetInstance().getCurFigureNum()));
        //ObjectController.GetInstance().setAlign();
        //ObjectController.GetInstance().setActive(true);
    }

    public void InitMovement()
    {
        PlayerManager.GetInstance().setCharState((int)CharState.idle);
        AniController.GetInstance().setAniParameter(false);
        endPos = null;
        bBezier = false;
        bLeft = false;
        bRight = false;
    }

    public bool getWalkEnd() { return endPos == null; }

    public void setMoveSpeed(float _moveSpeed) { moveSpeed = _moveSpeed; }

    public void characterMove() { playerTr.Translate(playerTr.right * moveSpeed * Time.deltaTime, Space.World); }
            
    void Move(RState State, int _tileCount, int _tunCount)
    {
        if (PlayerManager.GetInstance().getCharState() == (int)CharState.walk)
        {
            RaycastHit hitInfo;
            if (RayManager.GetInstance().RayCastIsTag(playerTr.position, playerTr.right, out hitInfo, 0.26f, "Tile"))
            {
                AniController.GetInstance().setAniParameter(false);
                PlayerManager.GetInstance().setCharState((int)CharState.idle);
                UIController.GetInstance().changeFigure();
                ObjectController.GetInstance().setAlign();

                playerTr.transform.rotation = Quaternion.Euler(new Vector3(playerTr.transform.eulerAngles.x, playerTr.transform.eulerAngles.y + rot, playerTr.transform.eulerAngles.z));

                ObjectController.GetInstance().setAlign();
            }

            playerTr.Translate(playerTr.right * moveSpeed * Time.deltaTime, Space.World);
            if (endPos == null)
            {
                Vector3 tV;
                if (RayManager.GetInstance().RayCastIsTag(playerTr.position, playerTr.right, out hitInfo, 0.75f, "Tile"))
                {
                    tV = hitInfo.transform.position;
                    if (Physics.Raycast(playerTr.position + playerTr.right * -0.1f, (Vector3.up * -1), out hitInfo, 0.6f))
                    {
                        endPos = hitInfo.transform.position;
                        RaycastHit hit;
                        if (RayManager.GetInstance().RayCastIsTag(tV + playerTr.right * 0.25f * -1, playerTr.forward, out hit, 0.26f))
                        {
                            rState = RState.right;
                            bLeft = true;
                        }
                        if (RayManager.GetInstance().RayCastIsTag(tV + playerTr.right * 0.25f * -1, playerTr.forward * -1, out hit, 0.26f))
                        {
                            bRight = true;
                            rState = RState.left;

                        }


                        if (bLeft == true && bRight == true)
                        {

                            if (!RayManager.GetInstance().RayCastIsTag(tV + playerTr.right * 0.25f, playerTr.up * -1, 0.26f, "Earth"))
                            {
                                rot -= 90f;
                            }

                            if (rot == -90f)
                            {
                                if (!RayManager.GetInstance().RayCastIsTag(tV + playerTr.right * -0.25f + playerTr.forward * 0.5f, playerTr.up * -1, 0.26f, "Earth", "MiddleTarget"))
                                {
                                    rot += 180f;
                                }
                            }
                        }
                    }
                }

            }
        }
        Debug.DrawLine(playerTr.position + playerTr.right * -0.1f, playerTr.up *  10);
    }


    Vector3 _getBezier(Vector3 v1, Vector3 v2, Vector3 v3, float fDetail)  
    {  
        Vector3 vResult = Vector3.zero;

        vResult.x = v3.x * (fDetail * fDetail) + v2.x * (fDetail * 2 * (1-fDetail)) + v1.x * ((1-fDetail)*(1-fDetail));
        vResult.y = v3.y;
        vResult.z = v3.z * (fDetail * fDetail) + v2.z * (fDetail * 2 * (1-fDetail)) + v1.z * ((1-fDetail)*(1-fDetail));  
  
        return vResult;  
    }

    public Vector3[] M(Vector3 p1, int posCnt)
    {
        x1 = p1;
        if (rState == RState.right)
        {
            x2 = x1 + playerTr.right * 0.25f;
            x3 = x1 + -playerTr.forward * 0.25f + playerTr.right * 0.25f;
        }
        else if (rState == RState.left)
        {
            x2 = x1 + playerTr.right * 0.25f;
            x3 = x1 + playerTr.forward * 0.25f + playerTr.right * 0.25f;
        }

        Vector3[] result = new Vector3[posCnt];
        int i = 0;
        for (float detail = 0.0f; detail <= 1.0f; detail += (1.0f / posCnt))
        {
            result[i] = _getBezier(x1, x2, x3, detail);
            i++;
        }

        return result;
    }

    public Vector3[] CameraB(Vector3 p1, Vector3 p2, int posCnt)
    {
        x1 = p1;
        x2 = p1 + (p2 - p1)/2;
        x3 = p2;

        Vector3[] result = new Vector3[posCnt];
        int i = 0;
        for (float detail = 0.0f; i < posCnt; detail += (1.0f / posCnt))
        {
            result[i] = _getBezier_3(x1, x2, x3, detail);
            i++;
        }

        return result;
    }

    Vector3 _getBezier_3(Vector3 v1, Vector3 v2, Vector3 v3, float fDetail)
    {
        Vector3 vResult = Vector3.zero;

        vResult.x = v3.x * (fDetail * fDetail) + v2.x * (fDetail * 2 * (1 - fDetail)) + v1.x * ((1 - fDetail) * (1 - fDetail));
        vResult.y = v3.y * (fDetail * fDetail) + v2.y * (fDetail * 2 * (1 - fDetail)) + v1.y * ((1 - fDetail) * (1 - fDetail));
        vResult.z = v3.z * (fDetail * fDetail) + v2.z * (fDetail * 2 * (1 - fDetail)) + v1.z * ((1 - fDetail) * (1 - fDetail));

        return vResult;
    }

    public Vector3[] CameraB(Vector3 p1, Vector3 p2, Vector3 p3, int posCnt)
    {
        x1 = p1;
        x2 = p2;
        x3 = p3;

        Vector3[] result = new Vector3[posCnt];
        int i = 0;
        for (float detail = 0.0f; i < posCnt; detail += (1.0f / posCnt))
        {
            result[i] = _getBezier_3(x1, x2, x3, detail);
            i++;
        }

        return result;
    }
}
