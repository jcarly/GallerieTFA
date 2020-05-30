using UnityEngine;
using System.Collections;

public static class JoysticksHelper
{
    private static Vector2 joystickDeadZone = new Vector2(0.2f, 0.95f);
    private static float joystickPower = 3.0f;

    public static float FilterAndEase(float inputValue, Vector2 deadZones, float easingPower)
    {
        return Mathf.Pow(Mathf.InverseLerp(deadZones.x, deadZones.y, Mathf.Abs(inputValue)), easingPower) * Mathf.Sign(inputValue);
    }

    public static float FilterAndEase(float inputValue)
    {
        return Mathf.Pow(Mathf.InverseLerp(joystickDeadZone.x, joystickDeadZone.y, Mathf.Abs(inputValue)), joystickPower) * Mathf.Sign(inputValue);
    }
}
