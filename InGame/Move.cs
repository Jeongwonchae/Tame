using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
    private static Move instance;
    public static Move GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(Move)) as Move;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    public Transform target;
    public float dist;
    public float height = 5.0f;
    public float dampRotate = 0.1f;

    public GameObject text;

    private Transform tr;
    private Transform targetTr;

    public float smoothing = 0.2f;
    public float _dist;

    float angle;

    Vector3 startPos;

    Vector3 offset;
    Vector3 orgin_offset;

    bool b_startCutScene;

    bool b_outPoint;

    Transform carmeraPoint;

    Vector3 c_offset;

    float angleDist;

    bool b_offset;

    // Use this for initialization
    void Awake()
    {
        b_startCutScene = false;
        b_outPoint = false;
        tr = GameObject.Find("Camera").GetComponent<Transform>();
        targetTr = GameObject.Find("targetCamera").GetComponent<Transform>();
        dist = Vector3.Distance(PlayerManager.GetInstance().getPlayerTransfrom().position, tr.position);

        carmeraPoint = GameObject.Find("charCarPoint").GetComponent<Transform>();

        Vector3 pPos = carmeraPoint.position;
        orgin_offset = tr.position - carmeraPoint.position;

        c_offset = carmeraPoint.position -  PlayerManager.GetInstance().getPostion();
    }

    public void setArgument(bool set)
    {
        b_startCutScene = set;

        angleDist = Vector3.Distance(tr.eulerAngles, new Vector3(11f, 180f, tr.eulerAngles.z));
    }

     //Update is called once per frame
    void LateUpdate()
    {
        if (b_startCutScene)
        {
            Vector3 Angle = new Vector3(11f, 180f, tr.eulerAngles.z);
            TansAngle(tr, Angle, 0.003f);

            if (tr.eulerAngles.x <= 12.2f && tr.eulerAngles.y <= 181.2f)
            {
                b_startCutScene = false;
                b_outPoint = true;
                _dist = Vector3.Distance(tr.position, targetTr.position);
                startPos = tr.position;
                offset = tr.position - target.position;
                angle = tr.eulerAngles.x;

            } 

        }


        if (b_outPoint)
        {
            if (Vector3.Distance(startPos, tr.position) < _dist)
            {
                tr.Translate(offset * smoothing * Time.deltaTime, Space.World);
            }
            if (angle-3.0f <= tr.eulerAngles.x)
            {
                float fangle = tr.eulerAngles.x - 0.2f * Time.deltaTime;
                Vector3 t = new Vector3(fangle, tr.eulerAngles.y, tr.eulerAngles.z);
                tr.rotation = Quaternion.Euler(t);
            }
        }

        carmeraPoint.position = c_offset + PlayerManager.GetInstance().getPostion();

        if (!check())
        {
            Vector3 pPos = carmeraPoint.position;

            Vector3 targetCamPos = pPos - offset;

            tr.position = Vector3.Lerp(tr.position, targetCamPos, 0.8f * Time.deltaTime);

            Vector3 spPos = Camera.main.ScreenToViewportPoint(pPos);
            Vector3 stPos = Camera.main.ScreenToViewportPoint(tr.position);

            if (Vector3.Distance(tr.position, targetCamPos) < 0.3f)
            {
                b_offset = true;
            }
            else
            {
                b_offset = false;
            }
        }
    }

    public bool getCameraoffset()
    {
        return b_offset;
    }

    public bool check()
    {
        if (b_outPoint == false && b_startCutScene == false)
            return false;

        return true;
    }

    bool TansAngle(Transform _Tr, Vector3 _Angle, float _Time)
    {
        float currYAngleX = Mathf.LerpAngle(_Tr.eulerAngles.x, _Angle.x, _Time);
        float currYAngleY = Mathf.LerpAngle(_Tr.eulerAngles.y, _Angle.y, _Time);
        float currYAngleZ = Mathf.LerpAngle(_Tr.eulerAngles.z, _Angle.z, _Time);

        Quaternion rot = Quaternion.Euler(currYAngleX, currYAngleY, currYAngleZ);

        tr.position = target.position - (rot * Vector3.forward * dist);
        tr.LookAt(target);

        return true;
    }

	// Use this for initialization
    //void Start()
    //{
    //    //StartCoroutine("Other");
    //}

    //IEnumerator Other()
    //{
    //    //print("Other() Method");
    //    yield return new WaitForSeconds(2);
    //    //print("After WaitForSeconds() Method");
    //    StartCoroutine("Other");
    //}
}
