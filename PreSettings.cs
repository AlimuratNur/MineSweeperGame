using Godot;

public partial class PreSettings : Window
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int mineCount { get; private set; }

    public override void _Ready()
    {
        var firstLine = GetNode<LineEdit>("WidthInput");
        firstLine.TextSubmitted += text => GD.Print(text);
        
    }

    
}
