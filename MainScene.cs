using Godot;
using System;

namespace MyNamespace;

    
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
        GetNode<Window>("PreSettings").Hide();
        _grid = GetNode<GridOfMines>("GridOfMines");
        Init(Difficults.Easy);

    }
    
    public void Init(Difficults difficutlt)
    {
        var restartButton = GetNode<Button>("RestartButton");
        restartButton.Position = new Vector2(5*32-restartButton.Size.X/2, 0);
        restartButton.ButtonDown += () => RestartGame();

        var window = GetWindow();
        switch (difficutlt)
        {
            case Difficults.Easy:
                _grid.Init(10, 15, 40);
                break;
            case Difficults.Medium:
                _grid.Init(15, 20, 80);
                break;
            case Difficults.Hard:
                _grid.Init(20, 30, 120);
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
