using Godot;

public partial class MainWindowScene : Node2D
{
    private int _Width;
    private int _Height;
    private int _MineCount;
    private int _CurrentDifficult; 
    private MainScene Playground;

    public override void _Ready()
    {
        Playground = GetNode<MainScene>("Square");
        Playground.Hide();

        var choseDiffWindow = GetNode<GetChosedDifficult>("GetChosedDifficult");
        choseDiffWindow.SetDiff += x => _DiffcultsButtonDown(x);

        var window = GetNode<GetChosedDifficult>("GetChosedDifficult");
        window.CloseRequested += () => this.Hide();

    }

    private void _DiffcultsButtonDown(int diff)
    {
        _CurrentDifficult = diff;
        
        Playground.Init((Difficults)diff);
        Playground.Show();
        GetNode<GetChosedDifficult>("GetChosedDifficult").Hide();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsKeyPressed(Key.H))
        {
            GetNode<GetChosedDifficult>("GetChosedDifficult").Show();
        }

        if (Input.IsActionPressed("ui_text_scroll_up"))
        {
            DisplayServer.ScreenGetScale(2);
        }
    }
}
