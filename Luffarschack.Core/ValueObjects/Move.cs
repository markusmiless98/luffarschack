namespace Luffarschack.Core.ValueObjects;

public class Move //MoveRequest kommer från frontend
{
    public int x { get; set; }
    public int y { get; set; }
    public int z { get; set; }
    public string Player { get; set; } //delete
}