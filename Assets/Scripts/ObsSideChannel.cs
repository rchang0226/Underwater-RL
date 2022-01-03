using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;

public class ObsSideChannel : SideChannel
{
    //WaterSettings settings = WaterSettings.Default;

    public ObsSideChannel()
    {
        ChannelId = new Guid("621f0a70-4f87-11ea-a6bf-784f4387d1f7");
    }

    protected override void OnMessageReceived(IncomingMessage msg)
    {
        if (RobotAgent.testMode)
        {
            IList<float> startPos = msg.ReadFloatList();

            TestAgent.testStart = new Vector3(startPos[0], startPos[1], startPos[2]);
            TestAgent.testGoal = new Vector3(startPos[3], startPos[4], startPos[5]);

            RobotAgent.randomGoalX = startPos[6];
            RobotAgent.randomGoalY = startPos[7];
            RobotAgent.randomGoalZ = startPos[8];
        }
        else
        {
            var visibility = msg.ReadFloat32();
            WaterSettings.controlVisibility = visibility;
        }
    }

    public void SendObsToPython(float horizontalDist, float verticleDist, float Angle,
    float Depth_from_Surface, float pos_z)
    {

        List<float> msgToSend = new List<float>() { horizontalDist, verticleDist, Angle, Depth_from_Surface,
         pos_z };
        using (var msgOut = new OutgoingMessage())
        {
            msgOut.WriteFloatList(msgToSend);
            QueueMessageToSend(msgOut);
        }

    }
}