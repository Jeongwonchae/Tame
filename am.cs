using UnityEngine;
using System.Collections;

public class am : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 rot;
        rot = transform.eulerAngles + Vector3.up * 0.707f;

        transform.rotation = Quaternion.Euler(rot);
        print(Mathf.Rad2Deg * 0.707f);
        Vector3 a = new Vector3(0, 0.707f, 0.707f);
        Vector3 b = new Vector3(0, 0, 0.707f);
        print(Vector3.Angle(a, b));

        print(Mathf.Cos(45));

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
