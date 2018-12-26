using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	// Use this for initialization
    Renderer rend;

    Transform Tr;

    void Awake()
    {
        Tr = GameObject.Find("UI").transform.FindChild("InGameUIPenul").transform.FindChild("Fade").transform;

        _SetActive(true);
        rend = Tr.GetComponent<Renderer>();
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        for (float i = 1f; i >= 0; i -= 0.01f)
        {
            Color color = new Vector4(0, 0, 0, i);
            rend.material.color = color;
            yield return 0;
        }
    }

    IEnumerator FadeOut()
    {
        for (float i = 1f; i <= 1; i += 0.01f)
        {
            Color color = new Vector4(0, 0, 0, i);
            rend.material.color = color;
            yield return 0;
        }
    }

    public void _SetActive(bool _set)
    {
        Tr.gameObject.SetActive(_set);
    }
}
