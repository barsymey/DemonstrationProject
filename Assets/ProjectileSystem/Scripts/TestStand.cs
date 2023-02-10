using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStand : MonoBehaviour
{
    List<IUsableTool> tools;
    // Start is called before the first frame update
    void Start()
    {
        tools = new List<IUsableTool>();
        foreach(IUsableTool tool in GetComponentsInChildren<IUsableTool>())
            tools.Add(tool);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T))
            UseTool();
    }

    void UseTool()
    {
        foreach(IUsableTool tool in tools)
            tool.Use();
    }
}
