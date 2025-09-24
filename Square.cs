using System.Collections.Generic;
using Godot;

public partial class Square : Button
{
    [Signal]
    public delegate void OpenAllEmptySquaresEventHandler();
    [Signal]
    public delegate void GameOverEventHandler();
    [Signal]
    public delegate void ButtonDownedEventHandler();

    public int XData { get; private set; }
    public int YData { get; private set; }
  
    public bool _flagged { get; private set; } = false;
    public int _mineCount { get; private set; }
    private bool _isMine ;
    
    private Sprite2D _sprite;

    
    public void Init(bool isMine, int mineCount, int x, int y)
    {
        _isMine = isMine;
        _mineCount = mineCount;
        XData = x;
        YData = y;
    }

    public override void _Ready()
    {
        ButtonDown += () => _onButtonDown();
        _sprite = GetNode<Sprite2D>( "SquareSprite");
    }

    public void PressedSpriteTexture()
    {
        
        if (_flagged 
            || Disabled) return;
        Disabled = true;
        if (_isMine)
        {
            _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_07.png");
            EmitSignalGameOver();
            return;
        }
        EmitSignalButtonDowned();
        switch (_mineCount)
        {
            case (0):
            {
                _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_01.png");
                break;
            }
            case (1):
                _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_08.png");
                break;
            case (2):
                _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_09.png");
                break;
            case (3):
                _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_10.png");
                break;
            case (4):
                _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_11.png");
                break;
            case (5):
                _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_12.png");
                break;
            case (6):
                _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_13.png");
                break;
            case (7):
                _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_14.png");
                break;
            case (8):
                _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_15.png");
                break;
            default:
                GD.Print("Invalid ");
                break;
        }
    }

    public void GameEnded()
    {
        Disabled = true;
        if (!_isMine)
        {
            if(_flagged) _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_00.png");
            return;
        }
        _sprite.Texture = GD.Load<Texture2D>("res://Assets/Minesweeper-pack/single-files/minesweeper_05.png");
    }

    private void _onButtonDown()
    {
        if (Input.IsMouseButtonPressed(MouseButton.Right))
        {
            if (!_flagged)
            {
                _sprite.Texture =
                    GD.Load<Texture2D>(
                        "res://Assets/Minesweeper-pack/single-files/minesweeper_02.png");
                _flagged = true;
            }
            else
            {
                _sprite.Texture =
                    GD.Load<Texture2D>(
                        "res://Assets/Minesweeper-pack/single-files/minesweeper_00.png");
                _flagged = false;
            }
        }

        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            if(_mineCount == 0)
                EmitSignalOpenAllEmptySquares();
            PressedSpriteTexture();
        }
    }

}
