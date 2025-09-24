using Godot;
using System;

public partial class MainScene : Node2D
{
    private GridOfMines _grid;
    public override void _Ready()
    {
        GetNode<Window>("PreSettings").Hide();
        _grid = GetNode<GridOfMines>("GridOfMines");
        _grid.Init(10, 15, 40);

        var restartButton = GetNode<Button>("RestartButton");
        restartButton.Position = new Vector2(5*32, 0);
        restartButton.ButtonDown += () => RestartGame();

        var window = GetWindow();
        
        
    }

    private void RestartGame()
    {
        GetTree().ReloadCurrentScene();
        
    }
}
