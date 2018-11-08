using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CoroutineAgent
{

    public static IEnumerator DelayOperation(float delay, System.Action operation, MonoBehaviour behaviour = null)
    {
        return DelayOperation(new WaitForSeconds(delay), operation, behaviour);
    }

    public static IEnumerator DelayOperation(YieldInstruction delay, System.Action operation, MonoBehaviour behaviour)
    {
        IEnumerator enumerator = DelayOperation(delay, operation);
        (behaviour ?? LuaFramework.Main.Instance).StartCoroutine(enumerator);
        return enumerator;
    }

    public static IEnumerator DelayOperation(YieldInstruction delay, System.Action operation)
    {
        if (operation != null)
        {
            yield return delay;
            operation();
        }
    }

    public static IEnumerator DelayOperation(params KeyValuePair<YieldInstruction, System.Action>[] operations)
    {
        foreach (KeyValuePair<YieldInstruction, System.Action> operation in operations)
        {
            yield return operation.Key;
            if (operation.Value != null)
            {
                operation.Value();
            }
        }
    }

    public static KeyValuePair<YieldInstruction, System.Action> PackDelay(YieldInstruction delay, System.Action operation)
    {
        return new KeyValuePair<YieldInstruction, System.Action>(delay, operation);
    }

    public static IEnumerator LoopOperation(System.Action<float> loopOperation, float duration, System.Action endOperation, MonoBehaviour behaviour)
    {
        IEnumerator enumerator = LoopOperation(loopOperation, duration, endOperation);
        (behaviour ?? LuaFramework.Main.Instance).StartCoroutine(enumerator);
        return enumerator;
    }

    public static IEnumerator LoopOperation(System.Action<float> loopOperation, float duration, System.Action endOperation = null)
    {
        float time = RealTime.time;
        float pastTime;
        while ((pastTime = RealTime.time - time) < duration)
        {
            if (loopOperation != null)
            {
                loopOperation(pastTime / duration);
            }
            yield return null;
        }
        if (endOperation != null)
        {
            endOperation();
        }
    }

    public static IEnumerator WaitOperation(System.Func<bool> waitFor, System.Action operation, MonoBehaviour behaviour)
    {
        IEnumerator enumerator = WaitOperation(waitFor, operation);
        (behaviour ?? LuaFramework.Main.Instance).StartCoroutine(enumerator);
        return enumerator;
    }

    private static IEnumerator WaitOperation(System.Func<bool> waitFor, System.Action operation = null)
    {
        if (operation != null)
        {
            if (waitFor != null)
            {
                while (!waitFor())
                {
                    yield return null;
                }
            }
            operation();
        }
    }

    public static IEnumerator EndOfLagOperation(System.Action operation, MonoBehaviour behaviour = null)
    {
        IEnumerator enumerator = DoEndOfLagOperation(operation, behaviour);
        (behaviour ?? LuaFramework.Main.Instance).StartCoroutine(enumerator);
        return enumerator;
    }

    public static IEnumerator DoEndOfLagOperation(System.Action operation, MonoBehaviour behaviour = null)
    {
        if (operation != null)
        {
            yield return WaitForEndOfLag(behaviour);
            operation();
        }
    }

    public static Coroutine WaitForEndOfLag(MonoBehaviour behaviour = null, int maxFrameCount = 20)
    {
        return (behaviour ?? LuaFramework.Main.Instance).StartCoroutine(WaitForEndOfLag(maxFrameCount));
    }

    public static IEnumerator WaitForEndOfLag(int maxFrameCount = 20)
    {
        float maxFrame = Time.frameCount + maxFrameCount;

        List<float> deltaTimeList = new List<float>();
        deltaTimeList.Add(Time.unscaledDeltaTime);
        yield return null;
        deltaTimeList.Add(Time.unscaledDeltaTime);
        while (Time.frameCount < maxFrame)
        {
            yield return null;
            deltaTimeList.Add(Time.unscaledDeltaTime);
            if (deltaTimeList.Count > 3)
            {
                deltaTimeList.RemoveAt(0);
            }
            float variance = GetVariance(deltaTimeList);
            if (variance < 0.001F)
            {
                yield return null;
                break;
            }
        }
        deltaTimeList.Clear();
    }

    public static float GetVariance(ICollection<float> nums)
    {
        int count = nums.Count;
        float average = nums.Average();
        float varianceSum = 0;
        foreach (float num in nums)
        {
            varianceSum += Mathf.Pow(num - average, 2);
        }
        return varianceSum / count;
    }
}
