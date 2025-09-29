using Godot;
using System;
using System.Collections.Generic;

public enum Difficults{
    Easy,
    Medium,
    Expert
    }

public partial class MainScene : Node2D
{
    public bool IsFirstGame { get; private set; } = true;
    private GridOfMines _grid;


    private static Dictionary<Difficults, Tuple<int, int, int>> _DifficultsSettings = new Dictionary<Difficults, Tuple<int, int, int>>
    {
        [Difficults.Easy] = Tuple.Create(8, 9, 10),
        [Difficults.Medium] = Tuple.Create(10, 15, 40),
        [Difficults.Expert] = Tuple.Create(15, 20, 80)
    };


    public void Init(Difficults difficult)
    {
        if (IsFirstGame)
            IsFirstGame = false;
        

        var width = _DifficultsSettings[difficult].Item1;
        var height = _DifficultsSettings[difficult].Item2;
        var mineCount = _DifficultsSettings[difficult].Item3;

        var restartButton = GetNode<Button>("RestartButton");
        restartButton.ButtonDown += () => RestartGame();
        var restartButtonSizeY = (int)restartButton.Size.Y;
        restartButton.Position =
                new Vector2(width * 16 - restartButton.Size.X / 2, 0);

        _grid = GetNode<GridOfMines>("GridOfMines");
        _grid.Init(width, height, mineCount);
        _grid.GameIsWon += () =>
            GetNode<Label>("ShowFlagsCount").Text = "You won";


        WindowSet.SetSize(width,height, restartButtonSizeY);
    }

    public void RestartGame()
    {
        _grid.GetTree().ReloadCurrentScene();
    }
}


internal static class WindowSet
{
    internal static void SetSize(int width, int height, int restartButtonSizeY)
    {
        DisplayServer
            .WindowSetMode(DisplayServer.WindowMode.Windowed);
        DisplayServer
            .WindowSetSize(GetWindowSize((int)width, (int)height, restartButtonSizeY));
    }

    private static Vector2I GetWindowSize(int width, int height, int buttonSize)
        => new((int)(width * 32 * 0.8), (int)(height * 32 * 0.8) + buttonSize / 2 + 9);
}
