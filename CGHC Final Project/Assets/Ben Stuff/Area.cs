using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    private Button[] buttons;
    private FallingBlock[] fallingBlocks;

    void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        fallingBlocks = GetComponentsInChildren<FallingBlock>(true);
    }

    public void ResetArea()
    {
        foreach (Button button in buttons)
        {
            //button.ResetBlock();
        }

        foreach (FallingBlock fb in fallingBlocks)
        {
            fb.ResetBlock();
        }
    }


}
