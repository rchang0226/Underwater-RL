using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;

public class ObsSideChannel_3 : SideChannel
{
    //WaterSettings settings = WaterSettings.Default;

    public ObsSideChannel_3()
    {
        ChannelId = new Guid("621f0a70-4f87-11ea-a6bf-784f4387d1f7");
    }

    protected override void OnMessageReceived(IncomingMessage msg)
    {
        IList<float> Init = msg.ReadFloatList();
        if (RobotAgent_3.testMode)
        {
            TestAgent_3.testStartPos = new Vector3(Init[0], Init[1], Init[2]);
            TestAgent_3.testStartOri = new Vector3(Init[3], Init[4], Init[5]);

            RobotAgent_3.randomGoalX = Init[6];
            RobotAgent_3.randomGoalY = Init[7];
            RobotAgent_3.randomGoalZ = Init[8];

            WaterSettings.controlVisibility = Init[9];
        }
        else
        {
            WaterSettings.controlVisibility = Init[9];
        }
    }

    public void SendObsToPython(float horizontalDist, float verticleDist, float Angle,
    float Depth_from_Surface, float pos_x, float pos_z, float orientation)
    {

        List<float> msgToSend = new List<float>() { horizontalDist, verticleDist, Angle, Depth_from_Surface,
         pos_x, pos_z, orientation};
        using (var msgOut = new OutgoingMessage())
        {
            msgOut.WriteFloatList(msgToSend);
            QueueMessageToSend(msgOut);
        }

    }
}