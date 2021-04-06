using UnityEngine;

public class Timer
{
    private bool _isActive;
    private float startTime;

    public bool isActive
    {
        get
        {
            return _isActive;
        }
    }

    public void Reset()
    {
        _isActive = false;
    }

    public bool Tick(float time)
    {
        if (_isActive && Time.time > startTime + time)
        {
            return _isActive = false;
        }
        else if (_isActive)
        {
            return true;
        }
        else
        {
            startTime = Time.time;
            return _isActive = true;
        }
    }
}
