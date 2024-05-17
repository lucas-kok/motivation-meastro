using UnityEngine;

public class AppLogger : GenericSingleton<AppLogger>
{
    public void LogInfo(string message, Object context)
    {
        if (context != null)
            Debug.Log($"[{context.GetType().Name}] {message}", context);
        else
            Debug.Log($"[Unknown Source] {message}");
    }

    public void LogWarning(string message, Object context)
    {
        if (context != null)
            Debug.LogWarning($"[{context.GetType().Name}] {message}", context);
        else
            Debug.LogWarning($"[Unknown Source] {message}");
    }

    public void LogError(string message, Object context)
    {
        if (context != null)
            Debug.LogError($"[{context.GetType().Name}] {message}", context);
        else
            Debug.LogError($"[Unknown Source] {message}");
    }
}