using Godot;
using System;


public enum Difficults{
    Easy,
    Medium,
    Expert
    }

public partial class MainScene : Node2D
{
    
    private GridOfMines _grid;

    public override void _Ready()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        
        _grid = GetNode<GridOfMines>("GridOfMines");
        Init(Difficults.Expert);

    }
    
    public void Init(Difficults difficult)
    {
        var restartButton = GetNode<Button>("RestartButton");
        restartButton.Position = new Vector2(5*32-restartButton.Size.X , 0);
        restartButton.ButtonDown += () => RestartGame();

        var restartButtonSizeY= (int)restartButton.Size.Y;
        switch (difficult)
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
        }
    }

    private Vector2I GetWindowSize(int width, int height, int buttonSize) 
        => new Vector2I(width * 32 + 15, height * 32 + buttonSize); 

    private void RestartGame()
    {
        GetTree().ReloadCurrentScene();
        
    }
}
