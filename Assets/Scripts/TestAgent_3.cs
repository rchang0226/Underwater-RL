using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Random = UnityEngine.Random;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.SideChannels;
using Unity.MLAgents.Actuators;

public class TestAgent_3 : RobotAgent_3
{
    public static Vector3 testStartPos;
    public static Vector3 testStartOri;

    // Start is called before the first frame update
    void Start()
    {
        testMode = true;
    }

    public override void OnEpisodeBegin()
    {
        m_AgentRb.transform.position = testStartPos;
        m_AgentRb.transform.eulerAngles = testStartOri;
    }
}
