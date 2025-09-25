using Godot;
using System;


public enum Difficults{
    Easy,
    Medium,
    Hard
    }

public partial class MainScene : Node2D
{
    
    private GridOfMines _grid;

    public override void _Ready()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        
        _grid = GetNode<GridOfMines>("GridOfMines");
        Init(Difficults.Easy);

    }
    
    public void Init(Difficults difficutlt)
    {
        var restartButton = GetNode<Button>("RestartButton");
        restartButton.Position = new Vector2(5*32-restartButton.Size.X/2 + 15, 0);
        restartButton.ButtonDown += () => RestartGame();

        var restartButtonSizeY= (int)restartButton.Size.Y;
        switch (difficutlt)
        {
            
            case Difficults.Easy:
                _grid.Init(10, 15, 40);
                DisplayServer.WindowSetSize(GetWindowSize(10,15,restartButtonSizeY));
                break;
            case Difficults.Medium:
                _grid.Init(15, 20, 80);
                DisplayServer.WindowSetSize(GetWindowSize(15 ,20, restartButtonSizeY));
                break;
            case Difficults.Hard:
                _grid.Init(20, 30, 120);
                DisplayServer.WindowSetSize(GetWindowSize(20, 30,restartButtonSizeY));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(difficutlt), difficutlt, null);
        }
    }

    private Vector2I GetWindowSize(int width, int height, int buttonsize) 
        => new Vector2I(width * 32 + 15, height * 32 + buttonsize); 

    private void RestartGame()
    {
        GetTree().ReloadCurrentScene();
        
    }
}
