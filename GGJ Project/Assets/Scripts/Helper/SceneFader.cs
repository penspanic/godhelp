﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    Image img;
    Sprite black;
    #region Singleton
    static SceneFader _instance;
    public static SceneFader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("Prefabs/Fade")).AddComponent<SceneFader>();
                _instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
                _instance.img = _instance.GetComponent<Image>();
            }
            return _instance;
        }
    }
    #endregion

    void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
        black = Resources.Load<Sprite>("Black");
    }

    public void FillScreenBlack()
    {
        img.enabled = true;
        img.sprite = black;
    }

    public IEnumerator FadeOut(float duration, string nextScene = null)
    {
        float fadeAlpha = 0;

        img.enabled = true;
        img.sprite = black;

        while (fadeAlpha != 1)
        {
            // 시작시간과 지나가는 시간의 차이 / 지속시간 
            fadeAlpha = Mathf.MoveTowards(fadeAlpha, 1, Time.unscaledDeltaTime * (1 / duration));
            img.color = new Color(0, 0, 0, fadeAlpha);
            yield return null;
        }

        fadeAlpha = 1;
        img.color = new Color(0, 0, 0, fadeAlpha);

        if (nextScene != null)
            SceneManager.LoadScene(nextScene);
    }

    public IEnumerator FadeIn(float duration, string nextScene = null)
    {
        float fadeAlpha = 1;

        img.enabled = true;
        img.sprite = black;

        while (fadeAlpha != 0)
        {
            // 시작시간과 지나가는 시간의 차이 / 지속시간 
            fadeAlpha = Mathf.MoveTowards(fadeAlpha, 0, Time.unscaledDeltaTime * (1 / duration));
            img.color = new Color(0, 0, 0, fadeAlpha);
            yield return null;
        }

        img.enabled = false;

        if (nextScene != null)
            SceneManager.LoadScene(nextScene);
    }

    public IEnumerator SoundFadeOut(float duration, AudioSource[] audioSources) // 점점 작아지게
    {
        float fadeVolume = 1;

        List<float> originalVolumeList = new List<float>();
        for (int i = 0; i < audioSources.Length; i++)
            originalVolumeList.Add(audioSources[i].volume);

        // 0.5f~ 0
        while (fadeVolume != 0)
        {
            fadeVolume = Mathf.MoveTowards(fadeVolume, 0, Time.unscaledDeltaTime * (1 / duration));
            for (int i = 0; i < audioSources.Length; i++)
            {
                try
                {
                    audioSources[i].volume = originalVolumeList[i] * fadeVolume;
                }
                catch (System.Exception)
                {
                    continue;
                }
            }
            yield return null;
        }
    }
}