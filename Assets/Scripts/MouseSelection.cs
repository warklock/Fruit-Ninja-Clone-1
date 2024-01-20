using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSelection : MonoBehaviour
{
    public List<Image> icons = new List<Image>();
    public List<Image> pausedIcons = new List<Image>();
    public List<GameObject> mouseList = new List<GameObject>();

    public void MouseSelect(int i)
    {
        foreach (var item in icons)
        {
            item.gameObject.SetActive(false);
        }
        icons[i].gameObject.SetActive(true);

        foreach (var item in mouseList)
        {
            item.gameObject.SetActive(false);
        }
        mouseList[i].gameObject.SetActive(true);

        foreach (Image item in pausedIcons)
        {
            item.DOFade(0.5f, 0.3f).SetUpdate(true);
        }
        pausedIcons[i].DOFade(1, 0.3f).SetUpdate(true);
    }
}
