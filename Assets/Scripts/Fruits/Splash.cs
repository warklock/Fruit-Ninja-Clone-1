using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private SpriteRenderer _renderer;
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float i = 1f;
        while (i > 0f)
        {
            i -= 0.001f;
            _renderer.color = new Color(1f, 1f, 1f, i);
            yield return null;
        }
        Destroy(gameObject);
    }
}
