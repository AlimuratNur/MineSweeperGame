using Godot;


public partial class GetChosedDifficult : Window
{
    [Signal]
    public delegate void SetEasyDiffEventHandler();
    [Signal]
    public delegate void MediumDiffEventHandler();
    [Signal]
    public delegate void ExtremeDiffEventHandler();


    public override void _Ready()
    {
        GetNode<Button>("EasyButton").ButtonDown += () => _DiffcultsButtonDown(0);
        GetNode<Button>("MediumButton").ButtonDown += () => _DiffcultsButtonDown(1);
        GetNode<Button>("ExpertButton").ButtonDown += () => _DiffcultsButtonDown(2);
    }

    private void _DiffcultsButtonDown(int diff)
    {
        if(diff ==0)
            EmitSignalSetEasyDiff();
        else if(diff == 1)
            EmitSignalMediumDiff();
        else
            EmitSignalExtremeDiff();
            
        
    }
}
