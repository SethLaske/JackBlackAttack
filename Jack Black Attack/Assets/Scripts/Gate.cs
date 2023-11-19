using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Collider2D teleportBox;
    [SerializeField] private Collider2D wallBox;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    public void CloseGate() {
        teleportBox.enabled = false;
        wallBox.enabled = true;
        spriteRenderer.sprite = closedSprite;
    }

    public void OpenGate()
    {
        teleportBox.enabled = true;
        wallBox.enabled = false;
        spriteRenderer.sprite = openSprite;
    }
}
