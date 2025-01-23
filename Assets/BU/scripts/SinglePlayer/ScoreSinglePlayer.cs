using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public Button[] buttons;

    public void DisableAllButtons()
    {
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }
    }

    public void EnableAllButtons()
    {
        foreach (Button btn in buttons)
        {
            btn.interactable = true;
        }
    }
}
