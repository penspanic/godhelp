﻿using UnityEngine;
using System.Collections;

public class GoatUseEffect : MonoBehaviour
{
    float speed = 0.5f;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    public void OnEffectEnd()
    {
        Destroy(this.gameObject);
    }
}