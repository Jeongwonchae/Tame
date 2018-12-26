using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    Transform t;

	// Use this for initialization
	void Start () {
        t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        t.position += PlayerManager.GetInstance().getPlayerTransfrom().right * 0.005f;
	}


    //코루틴 예제
    IEnumerator WaitForRealSeconds(float _time)
    {
        float _start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < _start + _time)
        {
            yield return null;
        }
    }

//    IEnumerator Start () 로 사용하실 수 있습니다

//  Start () 뿐만 아니라 다른 MonoBehaviour 상속 함수들에도 사용가능합니다~
}
