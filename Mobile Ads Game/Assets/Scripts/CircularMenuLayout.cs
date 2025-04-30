using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMenuLayout : MonoBehaviour
{
    public float radius = 100f;

    void Update()
    {
        ArrangeInCircle();
    }

    void ArrangeInCircle()
    {
        int count = transform.childCount;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            float angle = angleStep * i * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            child.localPosition = pos;
        }
    }
}
