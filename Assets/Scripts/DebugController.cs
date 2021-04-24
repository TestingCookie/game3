using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;

    public PlayerController debugScript;

    public static DebugCommand jump;

    public List<object> commandList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            showConsole = !showConsole;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (showConsole)
            {
                HandleInput();
                input = "";
            }
        }
    }


    private void Awake()
    {
        jump = new DebugCommand("jump", "HigerJumpForce", "jump", () =>
        {
            debugScript.AddJumpForce(20);
        }
        );

        commandList = new List<object>
        {
            jump
        };
    }

    private void OnGUI()
    {
        if (!showConsole) { return; }

        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    private void HandleInput()
    {
        for(int i=0; i<commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                if(commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
        }
    }
}
