using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetGameButton : MonoBehaviour
{
    public enum ButtonType { None, PairBtn, ShapeBtn }
    [SerializeField] public ButtonType buttonType = ButtonType.None;
    public GameSet.Pairs pairs = GameSet.Pairs.None;
    public GameSet.PuzzleCategory shapes = GameSet.PuzzleCategory.None;

    public void SetGame(string SceneName)
    {
        switch (buttonType)
        {
            case ButtonType.PairBtn:
                GameSet.Instance.SetPair(pairs);
                break;
            case ButtonType.ShapeBtn:
                GameSet.Instance.SetShape(shapes);
                break;
        }
        if (GameSet.Instance.AllSet())
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
