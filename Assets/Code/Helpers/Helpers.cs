using UnityEngine;
using System.Collections;
using System;

public static class Helpers
{
    public static Vector2 GetDirectionFromAngle(float degAngle)
    {
        float angle = Mathf.Deg2Rad * degAngle;

        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);

        Vector2 originalDirection = new Vector2(0, 1);
        Vector2 newDirection = originalDirection;

        newDirection.x = originalDirection.x * cosAngle - originalDirection.y * sinAngle;
        newDirection.y = originalDirection.x * sinAngle + originalDirection.y * cosAngle;

        return newDirection;
    }

    public static T ChaseValue<T>(T originValue, T targetValue, float duration, float deltaTime)
    {
        if (typeof(T) == typeof(float))
        {
            return (T)(object)Mathf.Lerp((float)(object)originValue, (float)(object)targetValue, deltaTime / duration);
        }
        else if (typeof(T) == typeof(Vector2))
        {
            return (T)(object)Vector2.Lerp((Vector2)(object)originValue, (Vector2)(object)targetValue, deltaTime / duration);
        }
        else if (typeof(T) == typeof(Vector3))
        {
            return (T)(object)Vector3.Lerp((Vector3)(object)originValue, (Vector3)(object)targetValue, deltaTime / duration);
        }
        else if (typeof(T) == typeof(Vector4))
        {
            return (T)(object)Vector4.Lerp((Vector4)(object)originValue, (Vector4)(object)targetValue, deltaTime / duration);
        }
        else if (typeof(T) == typeof(Quaternion))
        {
            return (T)(object)Quaternion.Slerp((Quaternion)(object)originValue, (Quaternion)(object)targetValue, deltaTime / duration);
        }
        else
        {
            throw new NotImplementedException("ChaseValue for type \"" + typeof(T) + "\" is not implemented");
        }
    }

    public static void PlayRandomAudioClip(AudioClip[] audioClips, AudioSource audioSource, float pitch)
    {
        if (audioClips.Length > 0)
        {
            audioSource.Stop();
            float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            randomValue = randomValue - Mathf.Floor(randomValue);
            randomValue *= audioClips.Length;
            int clipID = Mathf.FloorToInt(randomValue);
            audioSource.clip = audioClips[clipID];
            audioSource.pitch = pitch;
            audioSource.Play();
        }
    }
}
