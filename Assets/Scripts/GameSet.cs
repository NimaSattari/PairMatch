using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSet : MonoBehaviour
{
    private readonly Dictionary<PuzzleCategory, string> puzzleCatDirectory = new Dictionary<PuzzleCategory, string>();

    int setting;
    const int settingNumb = 2;
    public static GameSet Instance;

    public enum Pairs
    {
        None = 0, Pair10 = 10, Pair20 = 20,
    }
    public enum PuzzleCategory
    {
        None, Fruits, Vegtables
    }
    public struct Settings
    {
        public Pairs PairNumb;
        public PuzzleCategory puzzleCategory;
    }
    private Settings gameSet;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        SetPuzzleCatDirectory();
        gameSet = new Settings();
        ResetSettings();
    }

    private void SetPuzzleCatDirectory()
    {
        puzzleCatDirectory.Add(PuzzleCategory.Fruits, "Fruits");
        puzzleCatDirectory.Add(PuzzleCategory.Vegtables, "Vegtables");
    }

    public void SetPair(Pairs Number)
    {
        if (gameSet.PairNumb == Pairs.None)
        {
            setting++;
        }
        gameSet.PairNumb = Number;
    }
    public void SetShape(PuzzleCategory cat)
    {
        if (gameSet.puzzleCategory == PuzzleCategory.None)
        {
            setting++;
        }
        gameSet.puzzleCategory = cat;
    }
    public Pairs GetPairNumber()
    {
        return gameSet.PairNumb;
    }
    public PuzzleCategory GetShape()
    {
        return gameSet.puzzleCategory;
    }
    public void ResetSettings()
    {
        setting = 0;
        gameSet.PairNumb = Pairs.None;
        gameSet.puzzleCategory = PuzzleCategory.None;
    }
    public bool AllSet()
    {
        return setting == settingNumb;
    }
    public string GetMaterialDirectoryName()
    {
        return "Materials/";
    }
    public string GetPuzzleCategoryTextureDirectoryName()
    {
        if (puzzleCatDirectory.ContainsKey(gameSet.puzzleCategory))
        {
            return "Graphics/PuzzleCat/" + puzzleCatDirectory[gameSet.puzzleCategory] + "/";
        }
        else
        {
            Debug.Log("Error: Cannot Get Dir Name");
            return "";
        }
    }
}
