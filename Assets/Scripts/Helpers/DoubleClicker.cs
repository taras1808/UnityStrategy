using UnityEngine;

public class DoubleClicker
{
    /// <summary>
    /// Construcor with keycode and deltaTime set
    /// </summary>
    public DoubleClicker(KeyCode key, float deltaTime)
    {
        //set key
        this._key = key;

        //set deltaTime
        this._deltaTime = deltaTime;
    }

    /// <summary>
    /// Construcor with defult deltatime 
    /// </summary>
    public DoubleClicker(KeyCode key)
    {
        //set key
        this._key = key;
    }

    private KeyCode _key;
    private float _deltaTime = defultDeltaTime;

    //defult deltaTime
    public const float defultDeltaTime = 0.3f;

    /// <summary>
    /// Current key property
    /// </summary>
    public KeyCode key
    {
        get { return _key; }
    }

    /// <summary>
    /// Current deltaTime property
    /// </summary>
    public float deltaTime
    {
        get { return _deltaTime; }
    }

    //time pass
    private float timePass = 0;
    /// <summary>
    /// Cheak for double press
    /// </summary>
    public bool DoubleClickCheak()
    {
        if (timePass > 0) { timePass -= Time.deltaTime; }

        if (Input.GetKeyDown(_key))
        {
            if (timePass > 0) { timePass = 0; return true; }

            timePass = _deltaTime;
        }

        return false;
    }
}