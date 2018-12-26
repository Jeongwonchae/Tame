using UnityEngine;
using System.Collections;

public class portal : MonoBehaviour {

	// Use this for initialization

    bool b;
    public float smoothing = 0.02f;
    float dist;

    Vector3 sp;

    Vector3 offset;

    bool b_start;

    Collider getColl;

	void Awake () {
        b = false;
        GetComponent<BoxCollider>().isTrigger = true;

        b_start = false;
	}

    void OnTriggerEnter(Collider other)
    {
        print("Portal in");

        if (other.tag == "Character")
        {
            PlayerManager.GetInstance().setCharState((int)CharState.idle);
            AniController.GetInstance().setAniParameter(true);
            GameObject.FindWithTag("Character").GetComponent<Movement>().InitMovement();
            Transform eTr = GameObject.FindWithTag("targetPortal").transform;
            other.transform.position = eTr.position + eTr.forward * 0.25f;
            offset = (GameObject.Find("1-1").transform.position + PlayerManager.GetInstance().getPlayerTransfrom().up * 0.3f * -1) - PlayerManager.GetInstance().getPlayerTransfrom().position;

            dist = Vector3.Distance(PlayerManager.GetInstance().getPlayerTransfrom().position, GameObject.Find("1-1").transform.position);

            sp = PlayerManager.GetInstance().getPlayerTransfrom().position;

            b = true;
        }
    }

    void FixedUpdate()
    {

        if (b)
        {
            if (Move.GetInstance().getCameraoffset())
            {
                b_start = true;
            }
            
        }
        if (b_start)
        {
            if (Vector3.Distance(PlayerManager.GetInstance().getPlayerTransfrom().position, sp) < dist)
            {
                PlayerManager.GetInstance().getPlayerTransfrom().Translate(offset * smoothing * Time.deltaTime, Space.World);
            }
            else
            {
                if (Move.GetInstance().getCameraoffset())
                {
                    Move.GetInstance().setArgument(true);
                    AniController.GetInstance().setAniParameter(false);
                    b = false;
                    b_start = false;
                }
            }
        }
    }
}
