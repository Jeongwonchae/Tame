using UnityEngine;
using System.Collections;
[System.Serializable]

public class TileComp
{
    public int Top;
    public int Bottom;
    public int Left;
    public int Right;
}

public class ObjectController : MonoBehaviour {

    static Transform Tp;

    private static ObjectController instance;
    public static ObjectController GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(ObjectController)) as ObjectController;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        if (!Tp)
            Tp = PlayerManager.GetInstance().getPlayerTransfrom();

        return instance;
    }

    int crushTile;

	// Use this for initialization
    TileComp tileComp = new TileComp();

    bool possible = true;

    int TileSize;
    int extraTileCnt;

    public GameObject[] Tile;
    RaycastHit h;
    public bool getPossible()
    {
        Transform playerTr = PlayerManager.GetInstance().getPlayerTransfrom();
        RaycastHit hitInfo;
        if (RayManager.GetInstance().RayCastIsTag(playerTr.position, playerTr.right, out hitInfo, Mathf.Infinity, "Tile", "MiddleTarget", "Portal", "Event"))
        {
            Vector3 originHit = hitInfo.transform.position;
            h = hitInfo;
            int i = 0;
            float Dist = Vector3.Distance(playerTr.position, hitInfo.transform.position);
            while (Vector3.Distance(hitInfo.transform.position + (playerTr.right * 0.25f * -1) + playerTr.right * i * 0.5f * -1, hitInfo.transform.position) < Dist - 0.3f)
            {
                if (!RayManager.GetInstance().RayCastIsTag(hitInfo.transform.position + (playerTr.right * 0.25f * -1) + playerTr.right * i * 0.5f * -1 + Vector3.up * 0.5f, playerTr.up * -1, 1.0f, "Earth", "Portal", "MiddleTarget"))
                {
                    return false;
                }
                i++;
            }

            RaycastHit hit;
            if (!RayManager.GetInstance().RayCastIsTag(originHit + playerTr.right * -1 * 0.25f, playerTr.forward, out hit, 0.26f, "Tile", "MiddleTarget", "Portal", "Event"))
            {
                i = 0;
                if (RayManager.GetInstance().RayCastIsTag(originHit + playerTr.right * -1 * 0.25f, playerTr.forward, out hit, Mathf.Infinity, "Tile", "MiddleTarget", "Portal"))
                {
                    Dist = Vector3.Distance(originHit + playerTr.right * -1 * 0.25f, hit.transform.position);
                    h = hitInfo;
                    while (Vector3.Distance(originHit + playerTr.right * 0.25f * -1 + playerTr.forward * 0.5f * i, originHit + playerTr.right * -1 * 0.25f) < Dist)
                    {
                        if (!RayManager.GetInstance().RayCastIsTag(originHit + playerTr.right * 0.25f * -1 + playerTr.forward * 0.5f * i + Vector3.up * 0.5f, playerTr.up * -1, out hitInfo, 1.0f, "Earth", "Portal", "MiddleTarget"))
                        {
                            return false;
                        }
                        i++;
                    }
                }
            }
            else
            {
                i = 0;
                if (RayManager.GetInstance().RayCastIsTag(originHit + playerTr.right * -1 * 0.25f, playerTr.forward * -1, out hit, Mathf.Infinity, "Tile", "MiddleTarget", "Portal", "Event"))
                {
                    Dist = Vector3.Distance(originHit + playerTr.right * -1 * 0.25f, hit.transform.position);
                    h = hitInfo;
                    while (Vector3.Distance(originHit + playerTr.right * 0.25f * -1 + playerTr.forward * -1 * 0.5f * i, originHit + playerTr.right * -1 * 0.25f) < Dist)
                    {
                        if (!RayManager.GetInstance().RayCastIsTag(originHit + playerTr.right * 0.25f * -1 + playerTr.forward * -1 * 0.5f * i + Vector3.up * 0.5f, playerTr.up * -1, out hitInfo, 1.0f, "Earth", "Portal", "MiddleTarget"))
                        {
                            return false;
                        }
                        i++;
                    }
                }

            }
        }
        else
        {
            print("ininin");
            return false;
        }
        return true;
    }

    void Awake()
    {
        //test.gameObject.SetActive(false);
        crushTile = 0;
        extraTileCnt = 0;
    }

    public void setTile(int[] order)
    {
        tileComp.Top = order[0];
        tileComp.Bottom = order[1];
        tileComp.Left = order[2];
        tileComp.Right = order[3];
    }

    public void printTile()
    {
        print(tileComp.Top + " 1");
        print(tileComp.Bottom + " 1");
        print(tileComp.Left + " 1");
        print(tileComp.Right + " 1");
    }

    public void extraTile()
    {

        for (int i = TileSize; i < Tile.Length ; i++)
        {
            Tile[i].SetActive(false);
        }
        extraTileCnt = Tile.Length - TileSize;
    }

    public void OnextraTile()
    {
        if (extraTileCnt == 0)
            return;

        for (int i = Tile.Length - TileSize ; i < Tile.Length ; i++)
        {
            Tile[i].SetActive(true);
        }
    }

    public void CreateEffectTile()
    {
        if (tileComp.Left == tileComp.Right && tileComp.Top == tileComp.Bottom)
        {
            TileSize = tileComp.Left + tileComp.Right + tileComp.Bottom + tileComp.Top + 2;
        }
        else
            TileSize = tileComp.Left + tileComp.Right + tileComp.Bottom + tileComp.Top + 2;

        Tile = new GameObject[TileSize];

        for (int i = 0; i < TileSize; i++)
        {
            Tile[i] = (GameObject)Instantiate(Resources.Load("effectTile"));
        }
        for (int i = 1; i < TileSize; i++)
        {
            Tile[i].AddComponent<dummy>();
            Tile[i].AddComponent<BoxCollider>();
            Tile[i].GetComponent<BoxCollider>().isTrigger = true;
        }
        setAlign();
    }

    public void possibility()
    {
        crushTile = 0;
        for (int i = 0; i < TileSize; i++)
        {
            Tile[i].GetComponent<dummy>().setPossibility(false);
            Tile[i].GetComponent<dummy>().checkPossibility();
        }

        for (int i = 0; i < TileSize; i++)
        {
            if (crushTile == TileSize)
                Tile[i].GetComponent<dummy>().setPossibility(true);
        }
    }

    public void setCrush(int _set)
    {
        crushTile = _set;
    }

    public void incCrush()
    {
        crushTile++;
    }

    public void setAlign()
    {
        TileSize = tileComp.Left + tileComp.Right + tileComp.Bottom + tileComp.Top + 2;
        ObjectController.GetInstance().OnextraTile();

        Vector3 pT = PlayerManager.GetInstance().getPostion() + Tp.up * 0.17f + Tp.right * 0.5f;
        if (tileComp.Top > tileComp.Bottom)
        {
            SetATop(pT);
        }
        else if (tileComp.Top == tileComp.Bottom)
        {
            //print("inner");
            if (tileComp.Top == 0 || tileComp.Right == 0)
                SetAOneSame(pT);
            else if (tileComp.Top > tileComp.Left)
                SetATBSame(pT);
            else if (tileComp.Top < tileComp.Left)
                SetALRSame(pT);
            else
                SetASame(pT);
        }

        else
        {
            SetABottom(pT);
        }

        possible = getPossible();
        possibility();
        ObjectController.GetInstance().extraTile();
    }

    public bool getpossible() { return possible; }

    public void setActive(bool _Ative)
    {
        for(int i = 0 ; i < Tile.Length ; i++)
        {
            Tile[i].SetActive(_Ative);
        }
    }

    void SetAOneSame(Vector3 pT)
    {

        Vector3 l = new Vector3(0.05f, 0.5f, 0.05f);
        Vector3 r;

        if ((PlayerManager.GetInstance().getPlayerTransfrom().localEulerAngles.y / 90) % 2 >= 0.9f)
        {
            r = new Vector3(90, 90, 0);
        }
        else
        {
            r = new Vector3(90, 0, 0);
        }
        Vector3 p;

        if (tileComp.Top > tileComp.Left)
        {

            TestWonhyuk.GetInstance().setB("out!!!!");

            if (UIController.GetInstance().getAngle() > 1)
            {

                TestWonhyuk.GetInstance().setC("out!!!!");
                //bottom
                for (int i = 0; i < tileComp.Bottom; i++)
                {
                    p = pT - Tp.right * 0.25f + Tp.forward * (-i * 0.5f);

                    Tile[i].transform.localScale = l;
                    Tile[i].transform.position = p;
                    Tile[i].transform.rotation = Quaternion.Euler(r);
                }

                //top
                for (int i = 0; i < tileComp.Top; i++)
                {
                    p = pT + Tp.right * 0.25f + Tp.forward * (-i * 0.5f);

                    Tile[i + tileComp.Top].transform.localScale = l;
                    Tile[i + tileComp.Top].transform.position = p;
                    Tile[i + tileComp.Top].transform.rotation = Quaternion.Euler(r);
                }

                r.y = Mathf.Abs(r.y - 90);

                p = pT + Tp.forward * (0.25f);
                Tile[tileComp.Top + tileComp.Bottom].transform.localScale = l;
                Tile[tileComp.Top + tileComp.Bottom].transform.position = p;
                Tile[tileComp.Top + tileComp.Bottom].transform.rotation = Quaternion.Euler(r);

                p = pT + Tp.forward * (0.25f - tileComp.Top * 0.5f);
                Tile[tileComp.Top + tileComp.Bottom + 1].transform.localScale = l;
                Tile[tileComp.Top + tileComp.Bottom + 1].transform.position = p;
                Tile[tileComp.Top + tileComp.Bottom + 1].transform.rotation = Quaternion.Euler(r);
            }
            else
            {
                TestWonhyuk.GetInstance().setC("in!!!!");
                //bottom
                for (int i = 0; i < tileComp.Bottom; i++)
                {
                    p = pT - Tp.right * 0.25f + Tp.forward * (i * 0.5f);

                    Tile[i].transform.localScale = l;
                    Tile[i].transform.position = p;
                    Tile[i].transform.rotation = Quaternion.Euler(r);
                }

                //top
                for (int i = 0; i < tileComp.Top; i++)
                {
                    p = pT + Tp.right * 0.25f + Tp.forward * (i * 0.5f);

                    Tile[i + tileComp.Top].transform.localScale = l;
                    Tile[i + tileComp.Top].transform.position = p;
                    Tile[i + tileComp.Top].transform.rotation = Quaternion.Euler(r);
                }

                r.y = Mathf.Abs(r.y - 90);

                p = pT + Tp.forward * -(0.25f);
                Tile[tileComp.Top + tileComp.Bottom].transform.localScale = l;
                Tile[tileComp.Top + tileComp.Bottom].transform.position = p;
                Tile[tileComp.Top + tileComp.Bottom].transform.rotation = Quaternion.Euler(r);

                p = pT + Tp.forward * -(0.25f - tileComp.Top * 0.5f);
                Tile[tileComp.Top + tileComp.Bottom + 1].transform.localScale = l;
                Tile[tileComp.Top + tileComp.Bottom + 1].transform.position = p;
                Tile[tileComp.Top + tileComp.Bottom + 1].transform.rotation = Quaternion.Euler(r);
            }
        }
        else
        {
            TestWonhyuk.GetInstance().setB("in!!!!");
            p = pT - Tp.right * 0.25f;
            Tile[0].transform.localScale = l;
            Tile[0].transform.position = p;
            Tile[0].transform.rotation = Quaternion.Euler(r);

            p = pT + Tp.right * (-0.25f + tileComp.Left * 0.5f);
            Tile[1].transform.localScale = l;
            Tile[1].transform.position = p;
            Tile[1].transform.rotation = Quaternion.Euler(r);

            r.y = Mathf.Abs(r.y - 90);

            //right
            for (int i = 0; i < tileComp.Right; i++)
            {
                p = pT + Tp.right * (i * 0.5f) + Tp.forward * -0.25f;

                Tile[i+2].transform.localScale = l;
                Tile[i+2].transform.position = p;
                Tile[i+2].transform.rotation = Quaternion.Euler(r);
            }

            //left
            for (int i = 0; i < tileComp.Left; i++)
            {
                p = pT + Tp.right * (i * 0.5f) + Tp.forward * 0.25f;

                Tile[i + tileComp.Right + 2].transform.localScale = l;
                Tile[i + tileComp.Right + 2].transform.position = p;
                Tile[i + tileComp.Right + 2].transform.rotation = Quaternion.Euler(r);
            }
        }
    }

    void SetATBSame(Vector3 pT)
    {
        //로컬 Tp.foword 를 사용하면 방향을 맞춤.

        Vector3 l = new Vector3(0.05f, 0.5f, 0.05f);
        Vector3 r = new Vector3(90, 90, 0);
        Vector3 p;

        //bottom
        for (int i = 0; i < tileComp.Bottom; i++)
        {
            //p = new Vector3(PlayerManager.GetInstance().getPostion().x + (i * 0.5f), -0.222f, PlayerManager.GetInstance().getPostion().z + 0.25f);
            if (i < tileComp.Left + tileComp.Right)
                p = pT + Tp.right * (-0.25f) - Tp.forward * (i * 0.5f);
            else
                p = pT + Tp.right * (-0.25f + tileComp.Right * 0.5f) - Tp.forward * (i * 0.5f);

            Tile[i + tileComp.Left + tileComp.Bottom + tileComp.Right].transform.localScale = l;
            Tile[i + tileComp.Left + tileComp.Bottom + tileComp.Right].transform.position = p;
            Tile[i + tileComp.Left + tileComp.Bottom + tileComp.Right].transform.rotation = Quaternion.Euler(r);
        }


        //top
        for (int i = 0; i < tileComp.Top; i++)
        {
            //p = new Vector3(PlayerManager.GetInstance().getPostion().x + (i * 0.5f), -0.222f, PlayerManager.GetInstance().getPostion().z + 0.25f);
            if (i < tileComp.Left)
                p = pT + Tp.right * 0.25f - Tp.forward * (i * 0.5f);
            else
                p = pT + Tp.right * (0.25f + tileComp.Left * 0.5f) - Tp.forward * (i * 0.5f);

            Tile[i + tileComp.Left].transform.localScale = l;
            Tile[i + tileComp.Left].transform.position = p;
            Tile[i + tileComp.Left].transform.rotation = Quaternion.Euler(r);
        }


        l = new Vector3(0.05f, 0.5f, 0.05f);
        r = new Vector3(90, 0, 0);

        //left middle
        p = pT + Tp.forward * -0.25f + Tp.right * 0.5f;
        Tile[0].transform.localScale = l;
        Tile[0].transform.position = p;
        Tile[0].transform.rotation = Quaternion.Euler(r);

        //right middle
        p = pT + Tp.forward * -0.75f;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Bottom].transform.localScale = l;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Bottom].transform.position = p;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Bottom].transform.rotation = Quaternion.Euler(r);

        // bottom
        p = pT + Tp.forward * 0.25f;
        Tile[tileComp.Left + tileComp.Top].transform.localScale = l;
        Tile[tileComp.Left + tileComp.Top].transform.position = p;
        Tile[tileComp.Left + tileComp.Top].transform.rotation = Quaternion.Euler(r);

        p = pT + Tp.right * 0.5f * tileComp.Right + Tp.forward * (0.25f + 0.5f * -tileComp.Top);
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Bottom + 1].transform.localScale = l;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Bottom + 1].transform.position = p;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Bottom + 1].transform.rotation = Quaternion.Euler(r);
    }

    void SetALRSame(Vector3 pT)
    {
        Vector3 l = new Vector3(0.05f, 0.5f, 0.05f);
        Vector3 r = new Vector3(90, 90, 0);
        Vector3 p;

        //left middle
        p = pT + Tp.forward * 0.5f + Tp.right * 0.25f;
        Tile[tileComp.Left + tileComp.Right].transform.localScale = l;
        Tile[tileComp.Left + tileComp.Right].transform.position = p;
        Tile[tileComp.Left + tileComp.Right].transform.rotation = Quaternion.Euler(r);

        //right middle
        p = pT + Tp.right * 0.75f;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right].transform.localScale = l;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right].transform.position = p;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right].transform.rotation = Quaternion.Euler(r);

        // bottom
        p = pT + Tp.right * -0.25f;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Top].transform.localScale = l;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Top].transform.position = p;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Top].transform.rotation = Quaternion.Euler(r);

        p = pT + Tp.right * (-0.25f + 0.5f * tileComp.Left) + Tp.forward * 0.5f * tileComp.Top;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Top + 1].transform.localScale = l;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Top + 1].transform.position = p;
        Tile[tileComp.Left + tileComp.Bottom + tileComp.Right + tileComp.Top + 1].transform.rotation = Quaternion.Euler(r);

        l = new Vector3(0.05f, 0.5f, 0.05f);
        r = new Vector3(90, 0, 0);

        //right
        for (int i = 0; i < tileComp.Right; i++)
        {
            if (i < tileComp.Top + tileComp.Bottom)
                p = pT + Tp.right * (i * 0.5f) + Tp.forward * (-0.25f);
            else
                p = pT + Tp.right * (i * 0.5f) + Tp.forward * (-0.25f + tileComp.Top * 0.5f);

            Tile[i].transform.localScale = l;
            Tile[i].transform.position = p;
            Tile[i].transform.rotation = Quaternion.Euler(r);
        }

        //bottom
        for (int i = 0; i < tileComp.Left; i++)
        {
            if (i < tileComp.Top)
                p = pT + Tp.right * (i * 0.5f) + Tp.forward * (0.25f);
            else
                p = pT + Tp.right * (i * 0.5f) + Tp.forward * (0.25f + tileComp.Top * 0.5f);

            Tile[i + tileComp.Left].transform.localScale = l;
            Tile[i + tileComp.Left ].transform.position = p;
            Tile[i + tileComp.Left].transform.rotation = Quaternion.Euler(r);
        }
    }

    void SetASame(Vector3 pT)
    {
        Vector3 l = new Vector3(0.05f, 0.5f, 0.05f);
        Vector3 r = new Vector3(90, 90, 0);
        Vector3 p;

        //bottom
        for (int i = 0; i < tileComp.Bottom; i++)
        {
            p = pT + Tp.right * (-0.25f) - Tp.forward * (i * 0.5f);
            Tile[i].transform.localScale = l;
            Tile[i].transform.position = p;
            Tile[i].transform.rotation = Quaternion.Euler(r);
        }

        for (int i = 0; i < tileComp.Top; i++)
        {
            p = pT + Tp.right * (-0.25f + 0.5f * tileComp.Top) - Tp.forward * (i * 0.5f);
            Tile[i + tileComp.Bottom].transform.localScale = l;
            Tile[i + tileComp.Bottom].transform.position = p;
            Tile[i + tileComp.Bottom].transform.rotation = Quaternion.Euler(r);
        }

        r = new Vector3(90, 0, 0);
        for (int i = 0; i < tileComp.Left; i++)
        {
            p = pT + Tp.right * (i * 0.5f) - Tp.forward * (-0.25f + 0.5f * tileComp.Left);
            Tile[i + tileComp.Top + tileComp.Bottom].transform.localScale = l;
            Tile[i + tileComp.Top + tileComp.Bottom].transform.position = p;
            Tile[i + tileComp.Top + tileComp.Bottom].transform.rotation = Quaternion.Euler(r);
        }

        for (int i = 0; i < tileComp.Right; i++)
        {
            p = pT + Tp.right * (i * 0.5f) - Tp.forward * (-0.25f);
            Tile[i + tileComp.Top + tileComp.Bottom + tileComp.Left].transform.localScale = l;
            Tile[i + tileComp.Top + tileComp.Bottom + tileComp.Left].transform.position = p;
            Tile[i + tileComp.Top + tileComp.Bottom + tileComp.Left].transform.rotation = Quaternion.Euler(r);
        }

        p = pT - Tp.forward * (0.25f);
        Tile[tileComp.Top + tileComp.Bottom + tileComp.Left + tileComp.Right].transform.localScale = l;
        Tile[tileComp.Top + tileComp.Bottom + tileComp.Left + tileComp.Right].transform.position = p;
        Tile[tileComp.Top + tileComp.Bottom + tileComp.Left + tileComp.Right].transform.rotation = Quaternion.Euler(r);
    }

    void SetATop(Vector3 pT)
    {
        int dir = 1;
        if (tileComp.Left < tileComp.Right)
            dir = -1;

        Vector3 l = new Vector3(0.05f, 0.5f, 0.05f);
        Vector3 r;

        int cnt;
        if (tileComp.Left > tileComp.Right)
            cnt = tileComp.Left;
        else
            cnt = tileComp.Right;


        int angle = UIController.GetInstance().getAngle();
        r = new Vector3(90, Mathf.Abs(PlayerManager.GetInstance().getPlayerTransfrom().eulerAngles.y%180 - 90), 0);

        Vector3 p;

        p = pT + Tp.right * -0.25f;
        Tile[0].transform.localScale = l;
        Tile[0].transform.position = p;
        Tile[0].transform.rotation = Quaternion.Euler(r);

        for (int i = 0; i < tileComp.Top; i++)
        {

            p = pT + Tp.right * (cnt * 0.5f - 0.25f) + Tp.forward * (-(0.5f * i)) * dir;
            Tile[i + 1].transform.localScale = l;
            Tile[i + 1].transform.position = p;
            Tile[i + 1].transform.rotation = Quaternion.Euler(r);
        }

        

        for (int i = 0; i < tileComp.Bottom; i++)
        {
            p = pT + Tp.right * ((cnt-1) * 0.5f - 0.25f) + -Tp.forward * (0.5f * (i + 1)) * dir;

            Tile[i + tileComp.Top + 1].transform.localScale = l;
            Tile[i + tileComp.Top + 1].transform.position = p;
            Tile[i + tileComp.Top + 1].transform.rotation = Quaternion.Euler(r);
        }

        r.y = Mathf.Abs(r.y - 90);

        for (int i = 0; i < tileComp.Left; i++)
        {
            p = pT + Tp.right * (i * 0.5f) + Tp.forward * 0.25f;
            Tile[1 + tileComp.Bottom + tileComp.Top + i].transform.localScale = l;
            Tile[1 + tileComp.Bottom + tileComp.Top + i].transform.position = p;
            Tile[1 + tileComp.Bottom + tileComp.Top + i].transform.rotation = Quaternion.Euler(r);
        }


        for (int i = 0; i < tileComp.Right; i++)
        {
            p = pT + Tp.right * (i * 0.5f) + Tp.forward * -0.25f;
            Tile[i + tileComp.Left + tileComp.Bottom + tileComp.Top + 1].transform.localScale = l;
            Tile[i + tileComp.Left + tileComp.Bottom + tileComp.Top + 1].transform.position = p;
            Tile[i + tileComp.Left + tileComp.Bottom + tileComp.Top + 1].transform.rotation = Quaternion.Euler(r);
        }

        p = pT + Tp.right * ((cnt - 1) * 0.5f) + -Tp.forward * (0.5f * tileComp.Top - 0.25f) * dir;
        Tile[TileSize - 1].transform.localScale = l;
        Tile[TileSize - 1].transform.position = p;
        Tile[TileSize - 1].transform.rotation = Quaternion.Euler(r);
        
    }


    void SetABottom(Vector3 pT)
    {
        int dir = 1;
        if (tileComp.Left < tileComp.Right)
            dir = -1;

        Vector3 l = new Vector3(0.05f, 0.5f, 0.05f);
        Vector3 r;

        Vector3 p;

        r = new Vector3(90, Mathf.Abs(PlayerManager.GetInstance().getPlayerTransfrom().eulerAngles.y % 180 - 90), 0);

        for (int i = 0; i < tileComp.Bottom; i++)
        {

            p = pT + Tp.right * (-0.25f) + Tp.forward * (0.5f * (i)) * dir;
            Tile[i].transform.localScale = l;
            Tile[i].transform.position = p;
            Tile[i].transform.rotation = Quaternion.Euler(r);
        }

        for (int i = 0; i < tileComp.Top; i++)
        {

            p = pT + Tp.right * (0.25f) + Tp.forward * ((0.5f * i)) * dir;
            Tile[i + tileComp.Bottom].transform.localScale = l;
            Tile[i + tileComp.Bottom].transform.position = p;
            Tile[i + tileComp.Bottom].transform.rotation = Quaternion.Euler(r);
        }

        int cnt;
        if (tileComp.Left > tileComp.Right)
            cnt = tileComp.Left;
        else
            cnt = tileComp.Right;

        p = pT + Tp.right * (cnt * 0.5f - 0.25f) + Tp.forward * (0.5f * (tileComp.Bottom - 1)) * dir;

        Tile[TileSize - 1].transform.localScale = l;
        Tile[TileSize - 1].transform.position = p;
        Tile[TileSize - 1].transform.rotation = Quaternion.Euler(r);

        r.y = Mathf.Abs(r.y - 90);

        for (int i = 0; i < tileComp.Left; i++)
        {
            //p = new Vector3(PlayerManager.GetInstance().getPostion().x + (i * 0.5f), -0.222f, PlayerManager.GetInstance().getPostion().z + 0.25f);
            if (dir == -1)
                p = pT + Tp.right * ((i * 0.5f) + 0.5f) + (Tp.forward * -((tileComp.Top * 0.5f) - 0.25f));
            else
                p = pT + Tp.right * (i * 0.5f) + (Tp.forward * ((tileComp.Bottom * 0.5f) - 0.25f));
            Tile[i + tileComp.Bottom + tileComp.Top].transform.localScale = l;
            Tile[i + tileComp.Bottom + tileComp.Top].transform.position = p;
            Tile[i + tileComp.Bottom + tileComp.Top].transform.rotation = Quaternion.Euler(r);
        }

        for (int i = 0; i < tileComp.Right; i++)
        {
            if (dir == 1)
                p = pT + Tp.right * ((i * 0.5f) + 0.5f) + (Tp.forward * ((tileComp.Top * 0.5f) - 0.25f));
            else
                p = pT + Tp.right * ((i * 0.5f)) + (Tp.forward * -((tileComp.Bottom * 0.5f) - 0.25f));
            Tile[i + tileComp.Left + tileComp.Bottom + tileComp.Top].transform.localScale = l;
            Tile[i + tileComp.Left + tileComp.Bottom + tileComp.Top].transform.position = p;
            Tile[i + tileComp.Left + tileComp.Bottom + tileComp.Top].transform.rotation = Quaternion.Euler(r);
        }

        p = pT + Tp.forward * (-0.25f) * dir;
        Tile[TileSize - 2].transform.localScale = l;
        Tile[TileSize - 2].transform.position = p;
        Tile[TileSize - 2].transform.rotation = Quaternion.Euler(r);
        

        
    }
}
