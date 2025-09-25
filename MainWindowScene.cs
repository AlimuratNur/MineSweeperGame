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
        GetNode<Button>("GetNode").ButtonDown += () => _DiffcultsButtonDown(0);
        GetNode<Button>("MediumButton").ButtonDown += () => _DiffcultsButtonDown(1);
        GetNode<Button>("ExpertButton").ButtonDown += () => _DiffcultsButtonDown(2);
    }

    private void _DiffcultsButtonDown(int diff)
    {
        Playground.Init((Difficults)diff);
    }
}
