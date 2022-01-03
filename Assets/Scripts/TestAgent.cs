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

public class TestAgent : RobotAgent
{
    public static Vector3 testStart;
    public static Vector3 testGoal;

    // Start is called before the first frame update
    void Start()
    {
        testMode = true;
    }

    public override void OnEpisodeBegin()
    {
        m_AgentRb.transform.position = testStart;
        m_AgentRb.transform.eulerAngles = testGoal;
    }
}
