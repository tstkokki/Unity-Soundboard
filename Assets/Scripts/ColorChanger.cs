using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class ColorChanger : MonoBehaviour
{
    [SerializeField] Color[] colors;
    Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        Observable
            .Interval(TimeSpan.FromSeconds(5))
            .TakeUntilDestroy(gameObject)
            .Subscribe(x => 
            {
                currentIndex = (currentIndex < colors.Length-1) ? currentIndex+1 : 0;
            });
    }

    int currentIndex = 0;
    // Update is called once per frame
    void Update()
    {
        _image.color = Color.Lerp(_image.color, colors[currentIndex], 0.5f*Time.deltaTime);
    }
}
