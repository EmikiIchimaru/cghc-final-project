using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //public enum ButtonType { Switch, Tap, Timer }

    //public ButtonType buttonType;
    public bool state;

    public FallingBlock[] entryBlocks;
    public FallingBlock[] exitBlocks;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (!pc) { return; }
        if (!pc.Conditions.IsCollidingBelow) { return; }
           
        ButtonUse();
    
    }
    
    private void ButtonUse()
    {
        if (entryBlocks.Length == 0) { return; }
        foreach (FallingBlock block in entryBlocks)
        {
            block.Summon();
        }

        if (exitBlocks.Length == 0) { return; }
        foreach (FallingBlock block in exitBlocks)
        {
            block.StartFall();
        }
    }
}
