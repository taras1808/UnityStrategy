using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{
    public enum GameMode
    {
        Creative, Survival
    }

    public GameMode Mode { get; private set; }

    public Text text;

    private void Start()
    {
        Mode = GameMode.Creative;
        text.text = "Creative";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Mode = GameMode.Survival;
            text.text = "Survival";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Mode = GameMode.Creative;
            text.text = "Creative";
        }
    }
}