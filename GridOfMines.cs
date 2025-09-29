using Godot;
using System;
using System.Collections.Generic;

public partial class GridOfMines : GridContainer
{
    [Signal]
    public delegate void GameIsWonEventHandler();

    [Export] 
    private PackedScene _squareScene = GD.Load<PackedScene>("res://square.tscn");
    
    private Square[,] _gridSquares;
    
    public int Height { get; private set; }
    
    public int Width { get; private set; }
    
    public int MineCount { get; private set; }
    
    private int howMuchLeft;
    
    private Dictionary<int, HashSet<int>> _minesDict;
    
    public void Init(int width, int height, int mineCount)
    {
        Height = height;
        Width = width;
        Columns = width;
        MineCount = mineCount;
        _gridSquares = new Square[Height,Width];
        howMuchLeft = Height * Width - MineCount;
        
        

        GameStart();
    }
    
    public void GameStart()
    {
        _minesDict = GetMinesPositions();
        for (var w = 0; w < Height; w++)
        {
            for (var h = 0; h < Width; h++)
            {
                var isMine = false;
                var mineCount = GetMinesCount(w, h, _minesDict);
                int x = w;
                int y = h;
                _gridSquares[x, y] = (Square)_squareScene.Instantiate();
                if (IsSquareIsMine(x,y))
                {
                    isMine = true;
                    _gridSquares[x,y].GameOver += () => GameOver();
                }
                AddChild(_gridSquares[x, y]);
                _gridSquares[x, y].ButtonDowned += () => GameWon();
                _gridSquares[x, y].Init(isMine,mineCount, x, y);
                
                if(mineCount == 0)
                    _gridSquares[x,y].OpenAllEmptySquares += () => OpenAllEmptySquares(x, y);
                
            }
        }
    }

    public void RestartGame()
    {
        GetTree().ReloadCurrentScene();
    }

    #region PreGameInit

    private Dictionary<int,HashSet<int>> GetMinesPositions()
    {
        var dict = new Dictionary<int, HashSet<int>>();
        var rand = new Random();
        for (var i = 0; i < MineCount; i++)
        {
            var xAxesValue = rand.Next(0, Height);
            var yAxesValue = rand.Next(0, Width);
            if (!dict.ContainsKey(xAxesValue))
                dict[xAxesValue]= new HashSet<int>();
            if (dict[xAxesValue].Contains(yAxesValue))
            {
                i--;
                continue;
            }
            dict[xAxesValue].Add(yAxesValue);
        }

        return dict;
    }

    private int GetMinesCount(int x, int y, Dictionary<int, HashSet<int>> dict)
    {
        var count = 0;
        for (var i = -1; i < 2; i++)
        {
            var xAxis = x + i;
            if (!dict.ContainsKey(xAxis) 
                || IsOutOfGrid(xAxis, Height)) continue;
            
            for (var j = -1; j < 2; j++)
            {
                var yAxis = y + j;
                if (!dict[xAxis].Contains(yAxis)
                    || IsOutOfGrid(yAxis, Width)) continue;
                
                count++;
            }
        }
        return count;
    }
    #endregion

    private void OpenAllEmptySquares(int x, int y)
    {
        

       GD.Print("first step");
        for (var i = -1; i < 2; i++)
        {
            var xAxis = x + i;
            if (IsOutOfGrid(xAxis, Height))
                continue;

            GD.Print("x = " + xAxis);
            for (var j = -1; j < 2; j++)
            {
                var yAxis = y + j;
                if(IsOutOfGrid(yAxis, Width)) 
                    continue;

                GD.Print("y = " + yAxis);
                var square = _gridSquares[xAxis, yAxis];
                if(square.Disabled) continue;

                square.PressedSpriteTexture();
                if (square._mineCount == 0) 
                    OpenAllEmptySquares(xAxis, yAxis);

            }
        }
        GD.Print("End");
    }

    private void GameOver()
    {
        for (var x = 0; x < Height; x++)
        {
            for (var y = 0; y < Width; y++)
            {
                var square = _gridSquares[x, y];
                square.GameEnded();
            }
        }
        
    }

    private void GameWon()
    {
        howMuchLeft--;
        if (howMuchLeft == 0) EmitSignalGameIsWon();
    }



    #region HelperMethods
    private bool IsSquareIsMine(int x, int y) => 
        _minesDict.ContainsKey(x)
        && _minesDict[x].Contains(y);

    private static bool IsOutOfGrid(int v, int m) =>
        v < 0 || v >= m;
    #endregion
}
