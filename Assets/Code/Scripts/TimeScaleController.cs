using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    public KeyCode incrementKey = KeyCode.PageUp;
    public KeyCode decrementKey = KeyCode.PageDown;
    public KeyCode resetKey = KeyCode.Home;
    public float step = 0.1f;
    public float dpadDuration = 2.0f;

    void Update ()
    {
        float timeScale = Time.timeScale;
        timeScale += JoysticksHelper.FilterAndEase(Input.GetAxis("DpadHorizontal")) * Time.unscaledDeltaTime / dpadDuration;
        if (Input.GetKeyUp(incrementKey))
        {
            timeScale += step;
        }
        else if(Input.GetKeyUp(decrementKey))
        {
            timeScale -= step;
        }
        if (Input.GetKeyUp(resetKey) || Input.GetButtonUp("LeftStickClick"))
        {
            timeScale = 1.0f;
        }

        Time.timeScale = Mathf.Clamp01(timeScale);
    }
}
