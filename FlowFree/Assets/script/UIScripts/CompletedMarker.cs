using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedMarker : MonoBehaviour
{
    public Image spriteRenderer;
    public Sprite completed;
    public Sprite perfect;
    
    public void SetSprite(PlayerData.PassedLevelInfo passed) {
        if (passed == PlayerData.PassedLevelInfo.NO)
        {
            spriteRenderer.enabled = false;
        }
        else if (passed == PlayerData.PassedLevelInfo.PASSED)
        {
            spriteRenderer.sprite = completed;
        }
        else spriteRenderer.sprite = perfect;
    }
}
