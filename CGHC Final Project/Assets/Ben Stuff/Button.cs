using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //public enum ButtonType { Switch, Tap, Timer }

    //public ButtonType buttonType;
    public bool state;
    public Block[] blocks;
    private AudioManager audio;
    void Awake()
    {
        ResetButton();
        audio = AudioManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (!pc) { return; }
        if (!pc.Conditions.IsCollidingBelow) { return; }
           
        ButtonUse();
    }
    
    private void ButtonUse()
    {
        if (!state) { return; }

        state = false;
        
        if (blocks.Length == 0) { return; }

        foreach (Block block in blocks)
        {
            switch (block.buttonFunc)
            {
                case Block.ButtonFunc.Fall:
                    block.StartFall();
                    Debug.Log("fall");
                    break;
                case Block.ButtonFunc.Toggle:
                    block.ToggleDoor();
                    if (audio != null) {audio.Play("button");}
                    Debug.Log("tog");
                    break;
                case Block.ButtonFunc.Summon:
                    block.Summon();
                    Debug.Log("sum");
                    break;
            }      
        }
    

        
        
    }

    public void ResetButton()
    {
        state = true;
    }
}
