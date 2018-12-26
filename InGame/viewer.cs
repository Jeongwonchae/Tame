using UnityEngine;
using System.Collections;

public class viewer : MonoBehaviour {

    public Transform transform;
    Vector2 d;
	// Use this for initialization
	void Awake () {
        transform = GetComponent<Transform>();
        d = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Began)
                d = Input.GetTouch(0).position;

            if (d != Input.GetTouch(0).position)
            {
                Vector2 t = Input.GetTouch(0).position - d;
                d = Input.GetTouch(0).position;
                Vector3 rt = new Vector3(t.x * Time.deltaTime, t.y * Time.deltaTime, 0);
                transform.Translate(rt);
            }

            if (Input.GetTouch(0).position.x > 0)
            {
            }
        }
	}
}
