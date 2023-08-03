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

public class RobotAgent : Agent
{
    public Rigidbody m_AgentRb;

    public Transform target;

    RayPerceptionSensorComponent3D RayInput;
    ObsSideChannel obsSideChannel;

    public static float randomGoalX = 0f;
    public static float randomGoalY = 0f;
    public static float randomGoalZ = 0f;

    public static float horizontal_distance = 0f;
    public static float angle_rb_2_g = 0f;

    public static bool testMode;

    public Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        obsSideChannel = new ObsSideChannel();
        SideChannelManager.RegisterSideChannel(obsSideChannel);
    }

    public override void Initialize()
    {
        m_AgentRb = GetComponent<Rigidbody>();

    }

    public void OnDestroy()
    {
        SideChannelManager.UnregisterSideChannel(obsSideChannel);
    }

    public override void OnEpisodeBegin()
    {
        var random_robot_goal = GetRandomSpawnPos();
        m_AgentRb.transform.position = random_robot_goal.Item1;
        m_AgentRb.transform.eulerAngles = new Vector3(0f, random_robot_goal.Item2, 0f);

        target.position = random_robot_goal.Item3;
        float randomAngle = Random.Range(0f, 360f);
        target.eulerAngles = new Vector3(0f, 45f, 0f);

        // test0
//        m_AgentRb.transform.position = new Vector3(8f, -2f, -0.376f);
//        m_AgentRb.transform.eulerAngles = new Vector3(0f, 270f, 0f);

//        // test1
//        m_AgentRb.transform.position = new Vector3(15.0f, -3.0f, 8.5f);
//        m_AgentRb.transform.eulerAngles = new Vector3(0f, 270f, 0f);

        // test2
//        m_AgentRb.transform.position = new Vector3(11.23f, -3.42f, -0.376f);
//        m_AgentRb.transform.eulerAngles = new Vector3(0f, 180f, 0f);

        m_AgentRb.velocity = Vector3.zero;
        m_AgentRb.angularVelocity = Vector3.zero;

//        randomGoalX = -6.67f;
//        randomGoalY = -1.97f;
//        randomGoalZ = -3.45f;

        SetResetParameters();
    }

    // Randomize the initial position
    public (Vector3, float, Vector3) GetRandomSpawnPos()
    {
        var randomSpawnPos = Vector3.zero;
        var randomGoal = Vector3.zero;

        float chance_Robot = Random.Range(1.2f, 1.4f);
        if (chance_Robot > 1.2f) {
            randomSpawnPos = new Vector3(1.17f, -1.6f, -5.62f);
            randomGoal = new Vector3(3.17f, -2.23f, -7.62f);
            return (randomSpawnPos, 135f, randomGoal);
        }
        else if (chance_Robot > 1f)
        {
            var randomPosX = Random.Range(-1f, 1f);
            var randomPosY = Random.Range(-1.25f, -0.75f);
            var randomPosZ = Random.Range(3f, 4.5f);

            float rotationAngle = Random.Range(175f, 185f);

            randomGoalX = Random.Range(-1.5f, -0.5f);
            randomGoalY = Random.Range(-2f, -1.9f);
            randomGoalZ = Random.Range(1f, -1f);

            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }

        else if (chance_Robot < 0.2f)
        {
            var randomPosX = Random.Range(-1f, 1f);
            var randomPosY = Random.Range(-2f, -1f);
            var randomPosZ = Random.Range(3f, 4.5f);

            float rotationAngle = Random.Range(135f, 225f);

            float chance_Goal = Random.Range(0f, 1f);
            if (chance_Goal < 0.5f)
            {
                randomGoalX = Random.Range(-7f, 0f);
                randomGoalY = Random.Range(-2f, -1f);
                randomGoalZ = Random.Range(-4.5f, -11.5f);
            }
            else
            {
                randomGoalX = Random.Range(0f, 7f);
                randomGoalY = Random.Range(-2f, -1f);
                randomGoalZ = Random.Range(-4.5f, -11.5f);
            }
            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }

        else if (chance_Robot < 0.4f)
        {
            var randomPosX = Random.Range(-4f, -3f);
            var randomPosY = Random.Range(-2f, -1f);
            var randomPosZ = Random.Range(-7f, -8f);

            float rotationAngle = Random.Range(-45f, 135f);

            randomGoalX = Random.Range(-6.5f, 6.5f);
            randomGoalY = Random.Range(-1.9f, -1.1f);
            randomGoalZ = Random.Range(0.5f, 7f);

            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }

        else if (chance_Robot < 0.6f)
        {
            var randomPosX = Random.Range(3f, 4f);
            var randomPosY = Random.Range(-2f, -1f);
            var randomPosZ = Random.Range(-7f, -8f);

            float rotationAngle = Random.Range(-135f, 45f);

            randomGoalX = Random.Range(-6.5f, 6.5f);
            randomGoalY = Random.Range(-1.9f, -1.1f);
            randomGoalZ = Random.Range(0.5f, 7f);

            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }

        else if (chance_Robot < 0.8f)
        {
            var randomPosX = Random.Range(-9.5f, -8.5f);
            var randomPosY = Random.Range(-3f, -2f);
            var randomPosZ = Random.Range(3f, 4f);

            float rotationAngle = Random.Range(-45f, 45f);

            randomGoalX = Random.Range(12f, 19f);
            randomGoalY = Random.Range(-1.5f, -2f);
            randomGoalZ = Random.Range(12f, 19f);
            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }

        else
        {
            var randomPosX = Random.Range(8.5f, 9.5f);
            var randomPosY = Random.Range(-2f, -1f);
            var randomPosZ = Random.Range(3f, 4f);

            float rotationAngle = Random.Range(-45f, 45f);

            randomGoalX = Random.Range(-19f, -12f);
            randomGoalY = Random.Range(-2.0f, -2.5f);
            randomGoalZ = Random.Range(12f, 19f);

            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }


    }

    // Moves the agent according to the selected action
    public void MoveAgent(float act0, float act1)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;
        dirToGo = transform.forward * 0.03f + transform.up * act0 * 0.02f;
        rotateDir = -transform.up * act1;

        transform.Rotate(rotateDir, Time.fixedDeltaTime * Math.Abs(act1));
        m_AgentRb.AddForce(dirToGo, ForceMode.VelocityChange);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Move the agent using the action.
        var continuous_actions = actionBuffers.ContinuousActions;
        MoveAgent(continuous_actions[0], continuous_actions[1]);

        // Penalty given each step to encourage agent to finish task quickly.
        var ObsInfo = GetObsInfo(); // horizontal_distance, vertical_distance, angle_rb_2_g
        DisplayObs(m_AgentRb.transform.position, m_AgentRb.transform.eulerAngles[1], 
            new Vector3(randomGoalX, randomGoalY, randomGoalZ), ObsInfo.Item1, ObsInfo.Item2, ObsInfo.Item3);

        AddReward(-1f / MaxStep);
        obsSideChannel.SendObsToPython(ObsInfo.Item1, ObsInfo.Item2, ObsInfo.Item3, ObsInfo.Item4,
        m_AgentRb.transform.position[0], m_AgentRb.transform.position[2], m_AgentRb.transform.eulerAngles[1]);
    }

    public (float, float, float, float) GetObsInfo()
    {
        Vector3 Current_pos = m_AgentRb.transform.position;

        // first compute the distance from robot to goal
        horizontal_distance = (float)Math.Sqrt((float)Math.Pow(Current_pos[0] - randomGoalX, 2) +
            (float)Math.Pow(Current_pos[2] - randomGoalZ, 2));

        // secondly compute the angle the robot needs to turn to face the goal
        Vector3 angle_goal_vector = new Vector3(randomGoalX - Current_pos[0],
            randomGoalY - Current_pos[1], randomGoalZ - Current_pos[2]);
        Vector3 angle_goal_vector_proj = angle_goal_vector - Vector3.Project(angle_goal_vector, Vector3.up);

        angle_rb_2_g = Vector3.Angle(m_AgentRb.transform.forward, angle_goal_vector_proj);
        float dir = (Vector3.Dot(Vector3.up, Vector3.Cross(m_AgentRb.transform.forward, angle_goal_vector_proj)) < 0 ? 1 : -1);
        angle_rb_2_g *= dir; // source implementation: https://blog.csdn.net/qq_14838361/article/details/79459391

        return (horizontal_distance, (randomGoalY - Current_pos[1]), angle_rb_2_g, Current_pos[1]);
    }

    public void DisplayObs(Vector3 pos_rb, float rotation, Vector3 pos_goal,
        float horizontal_distance, float vertical_distance, float angle_rb_2_g)
    {
        debugText.text = "Delta Time: " + Time.deltaTime.ToString("0.0000") + ", " +
        "\nCurrent Time: " + Time.time.ToString("0.0000") + ", " + "\nCurrent Pos: " +
        pos_rb[0].ToString("0.0000") + ", " + pos_rb[1].ToString("0.0000") + ", "
        + pos_rb[2].ToString("0.0000") + "\nCurrent Rot: " + rotation.ToString("0.0000") +
         "\nCurrent Goal: " + pos_goal + "\nhorizontal_distance: " + horizontal_distance +
        "\nvertical_distance: " + vertical_distance + "\nangle_rb_2_g: " + angle_rb_2_g
        + "\ntransform:" + transform.up[0].ToString("0.0000") + ", " + transform.up[1].ToString("0.0000")
        + ", " + transform.up[2].ToString("0.0000");
    }


    void SetResetParameters()
    {
        // set the haze, fog and attenuation here
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {

    }
}
