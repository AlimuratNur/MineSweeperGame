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
    private bool isfirstgame = true;
    private GridOfMines _grid;

    private static Dictionary<Difficults, Tuple<int,int,int>> _DifficultsSettings = new Dictionary<Difficults, Tuple<int, int, int>>
    {
        [Difficults.Easy] = Tuple.Create(10, 15, 40),
        [Difficults.Medium] = Tuple.Create(15, 20, 80),
        [Difficults.Expert] = Tuple.Create(20,25,100)
    };

    public override void _Ready()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        _grid = GetNode<GridOfMines>("GridOfMines");
    }
    
    public void Init(Difficults difficult)
    {
        
        var restartButton = GetNode<Button>("RestartButton");
        restartButton.ButtonDown += () => RestartGame();
        var restartButtonSizeY= (int)restartButton.Size.Y;

        var width = _DifficultsSettings[difficult].Item1;
        var height = _DifficultsSettings[difficult].Item2;
        var mineCount = _DifficultsSettings[difficult].Item3;
        
        restartButton.Position = new Vector2(width*16-restartButton.Size.X/2 , 0);

        _grid.Init(width, height, mineCount);
        DisplayServer.WindowSetSize(GetWindowSize(width, height, restartButtonSizeY));

        /*switch (difficult)
        {
            
            case Difficults.Easy:
                _grid.Init(10, 15, 40);
                DisplayServer.WindowSetSize(GetWindowSize(10,15,restartButtonSizeY));
                break;
            case Difficults.Medium:
                _grid.Init(15, 20, 80);
                DisplayServer.WindowSetSize(GetWindowSize(15 ,20, restartButtonSizeY));
                break;
            case Difficults.Expert:
                _grid.Init(20, 25, 100);
                DisplayServer.WindowSetSize(GetWindowSize(20, 25,restartButtonSizeY));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(difficult), difficult, null);
        }*/
    }

    private Vector2I GetWindowSize(int width, int height, int buttonSize) 
        => new (width * 32 + 23, height * 32 + buttonSize); 

    private void RestartGame()
    {
        GetTree().ReloadCurrentScene();
        
    }
}
