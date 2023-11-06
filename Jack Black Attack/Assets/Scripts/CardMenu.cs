using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardMenu : MonoBehaviour
{
    [SerializeField] Transform LeftMenu;
    [SerializeField] Transform RightMenu;
    [SerializeField] float menuOpenTime = 1f;       // How long in seconds menu takes to open
    [SerializeField] float menuMoveAmount = 400f;   // How far the menu travels

    private bool menuOpen = true;

    private float leftClosedPosition;
    private float rightClosedPosition;
    private float leftOpenedPosition;
    private float rightOpenedPosition;

    public static CardMenu Instance; // Singleton Reference

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        leftClosedPosition = LeftMenu.position.x - menuMoveAmount;
        rightClosedPosition = RightMenu.position.x + menuMoveAmount;
        leftOpenedPosition = LeftMenu.position.x;
        rightOpenedPosition = RightMenu.position.x; 
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
        LeftMenu.DOMoveX(leftClosedPosition, menuOpenTime).SetEase(Ease.InOutSine);
        RightMenu.DOMoveX(rightClosedPosition, menuOpenTime).SetEase(Ease.InOutSine);
        menuOpen = false;
    }

    public void OpenMenu()
    {

        if (menuOpen) return; // If the menu is currently moving do not attempt to tween again
        LeftMenu.DOMoveX(leftOpenedPosition, menuOpenTime).SetEase(Ease.InOutSine);
        RightMenu.DOMoveX(rightOpenedPosition, menuOpenTime).SetEase(Ease.InOutSine);
        menuOpen = true;
    }
}
