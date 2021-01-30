using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFader : MonoBehaviour
{
    public RawImage rawImage;
    public float floatColorChangeSpeed = 1f;
    
    public bool isBlack = false;
    private bool isAnimanating = false;
    /// <summary>
    /// 屏幕逐渐清晰(淡入)
    /// </summary>
    ///
    private void Start()
    {
        
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Z) && isBlack)
        {
            ScreenToClear();
        }

        if (Input.GetKeyDown(KeyCode.X) && !isBlack)
        {
            ScreenToBlack();
        }*/
    }

    private void FadeToClear()
    {
        //插值运算
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, floatColorChangeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 屏幕逐渐暗淡(淡出)
    /// </summary>
    private void FadeToBlack()
    {
        //插值运算
        rawImage.color = Color.Lerp(rawImage.color, Color.black, floatColorChangeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 屏幕的淡入
    /// </summary>
    public void ScreenToClear(Util.NoParmsCallBack successCallback = null)
    {
        if (isAnimanating)
        {
            return;
        }
        isAnimanating = true;
        Tween t = DOTween.To(() => rawImage.color, x => rawImage.color = x, Color.clear, floatColorChangeSpeed);
        t.OnComplete(() =>
        {
            //设置为完全透明
            rawImage.color = Color.clear;
            //组件的开关设置为关闭的状态
            rawImage.enabled = false;
            isBlack = false;
            isAnimanating = false;
            successCallback?.Invoke();
        });
        // FadeToClear();
        // //当我们的a值小于等于0.05f的时候 就相当于完全透明了
        // if (rawImage.color.a <= 0.01f)
        // {
        //     //设置为完全透明
        //     rawImage.color = Color.clear;
        //     //组件的开关设置为关闭的状态
        //     rawImage.enabled = false;
        //     isBlack = false;
        // }
    }

    /// <summary>
    /// 屏幕的淡出
    /// </summary>
    public void ScreenToBlack(Util.NoParmsCallBack successCallback = null)
    {
        if (isAnimanating)
        {
            return;
        }
        isAnimanating = true;
        rawImage.enabled = true;
        Tween t = DOTween.To(() => rawImage.color, x => rawImage.color = x, Color.black, floatColorChangeSpeed);
        t.OnComplete(() =>
        {
            rawImage.color = Color.black;
            isBlack = true;
            isAnimanating = false;
            successCallback?.Invoke();
        });
        // //组件的打开
        // rawImage.enabled = true;
        // FadeToBlack(); 
        // //当前的阿尔法值大于0.95f的时候 表示已经接近于完全不透明的状态
        // if (rawImage.color.a >= 0.99f)
        // {
        //     //设置为完全不透明的状态
        //     rawImage.color = Color.black;
        //     isBlack = true;
        // }
    }
}
