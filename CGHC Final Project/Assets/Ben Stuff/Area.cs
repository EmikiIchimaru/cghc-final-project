using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public CheckpointBen checkpoint { get; private set; }
    private Button[] buttons;
    private Block[] blocks;

    void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        blocks = GetComponentsInChildren<Block>(true);
        checkpoint = GetComponentInChildren<CheckpointBen>();
    }

    public void ResetArea()
    {
        foreach (Button button in buttons)
        {
            button.ResetButton();
        }

        foreach (Block block in blocks)
        {
            block.ResetBlock();
        }
    }


}
