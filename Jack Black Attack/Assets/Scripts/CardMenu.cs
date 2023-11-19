using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardMenu : MonoBehaviour
{
    [SerializeField] RectTransform LeftMenu;
    [SerializeField] RectTransform RightMenu;
    [SerializeField] float menuOpenTime = 1f;       // How long in seconds menu takes to open
    [SerializeField] float menuMoveAmount = 400f;   // How far the menu travels

    [SerializeField] private bool menuOpen = false;
    [SerializeField] private Ease ease;

    private float leftClosedPosition;
    private float rightClosedPosition;
    private float leftOpenedPosition;
    private float rightOpenedPosition;

    public static CardMenu Instance; // Singleton Reference

    private void Awake()
    {
        Instance = this;
    }

    public void ButtonClick()
    {
        if(menuOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }
    public void CloseMenu()
    {
        if (!menuOpen) return; // If the menu is currently moving or already open do not attempt to tween again
        //LeftMenu.pivot = new Vector2(1,0.5f);
        //RightMenu.pivot = new Vector2(0,0.5f);
        LeftMenu.DOPivotX(1, menuOpenTime).SetEase(ease);
        RightMenu.DOPivotX(0, menuOpenTime).SetEase(ease);
        menuOpen = false;
    }

    public void OpenMenu()
    {

        if (menuOpen) return; // If the menu is currently moving do not attempt to tween again
        //LeftMenu.pivot = new Vector2(0,0.5f);
        //RightMenu.pivot = new Vector2(1,0.5f);
        LeftMenu.DOPivotX(0, menuOpenTime).SetEase(ease);
        RightMenu.DOPivotX(1, menuOpenTime).SetEase(ease);
        menuOpen = true;
    }
}
