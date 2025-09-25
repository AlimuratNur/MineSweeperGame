using Godot;

public partial class MainWindowScene : Node2D
{
    private int _Width;
    private int _Height;
    private int _MineCount;
    private MainScene Playground;

    public override void _Ready()
    {
        Playground = GetNode<MainScene>("Square");
        var choseDiffWindow = GetNode<GetChosedDifficult>("GetChosedDifficult");
        choseDiffWindow.SetDiff += x => _DiffcultsButtonDown(x);
    }

    private void _DiffcultsButtonDown(int diff)
    {
        Playground.Init((Difficults)diff);
        GetNode<GetChosedDifficult>("GetChosedDifficult").Hide();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsKeyPressed(Key.H))
        {
            GetNode<GetChosedDifficult>("GetChosedDifficult").Show();
        }
    }
}
