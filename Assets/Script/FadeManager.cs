using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;
    [SerializeField] private bool fadeOut = false;

    public void HideUI()
    {
        fadeOut = true;
    }

    private void Update()
    {
        if (fadeOut)
        {
            if (myUIGroup.alpha > 0)
            {
                myUIGroup.alpha -= Time.deltaTime;
                if (myUIGroup.alpha <= 0)
                {
                    myUIGroup.alpha = 0;
                    fadeOut = false;
                    myUIGroup.gameObject.SetActive(false); // Disable the Canvas GameObject
                }
            }
        }
    }
}
