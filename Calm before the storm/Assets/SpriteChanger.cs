using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void ChangeSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }
}
