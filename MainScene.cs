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

        
        switch (difficutlt)
        {
            case Difficults.Easy:
                _grid.Init(10, 15, 40);
                DisplayServer.WindowSetSize(new Vector2I(10*32 + 15, 20*32 -20));
                break;
            case Difficults.Medium:
                _grid.Init(15, 20, 80);
                DisplayServer.WindowSetSize(new Vector2I(10 * 32, 15 * 32));
                break;
            case Difficults.Hard:
                _grid.Init(20, 30, 120);
                DisplayServer.WindowSetSize(new Vector2I(20 * 32, 30 * 32 + (int)restartButton.Size.X));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(difficutlt), difficutlt, null);
        }
    }

    private void RestartGame()
    {
        GetTree().ReloadCurrentScene();
        
    }
}
