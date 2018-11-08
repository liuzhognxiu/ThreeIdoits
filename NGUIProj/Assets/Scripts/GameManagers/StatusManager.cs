using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public enum SceneStatus
{
    Login,
    Chat,
}

public class SceneMessage
{
    public SceneStatus Status { get; set; }
}

public class SceneStatusManager : MonoBehaviour {

    private Queue<SceneMessage> messageQueue = new Queue<SceneMessage>();

    void Update()
    {
        lock(messageQueue)
        {
            while(messageQueue.Count > 0)
            {
                ChangeScene(messageQueue.Dequeue().Status);
            }
        }
    }

    public void AddMessage(SceneMessage message)
    {
        lock(messageQueue)
        {
            messageQueue.Enqueue(message);
        }
    }

    protected void ChangeScene(SceneStatus status)
    {
        switch (status)
        {
            case SceneStatus.Login:
                SceneManager.LoadScene("login");
                break;
            case SceneStatus.Chat:
                SceneManager.LoadScene("chat");
                break;
        }
    }
}
