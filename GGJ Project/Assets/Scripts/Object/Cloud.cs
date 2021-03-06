﻿using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public float moveTime;

    Vector2 originalPos;
    void Awake()
    {
        originalPos = transform.position;
    }

    bool isFirst = true;
    float elapsedTime = 0f;
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (isFirst)
            transform.position = Vector3.Lerp(originalPos, endPos, elapsedTime / moveTime);
        else
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveTime);
        if (elapsedTime > moveTime)
        {
            isFirst = false;
            elapsedTime = 0;
            transform.position = startPos;
        }
    }
}
