using Godot;


public partial class GetChosedDifficult : Window
{
    [Signal]
    public delegate void SetDiffEventHandler(int diff);
    
    public override void _Ready()
    {
        GetNode<Button>("EasyButton").ButtonDown += () => _DiffcultsButtonDown(0);
        GetNode<Button>("MediumButton").ButtonDown += () => _DiffcultsButtonDown(1);
        GetNode<Button>("ExpertButton").ButtonDown += () => _DiffcultsButtonDown(2);
    }

    private void _DiffcultsButtonDown(int diff)
    {
            EmitSignalSetDiff(diff);
    }
}
