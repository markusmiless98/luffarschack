namespace Luffarschack.Core;

public class MoveRequest
{
    public MoveRequest()
    {
    
    }
    public MoveRequest(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public int x { get; set; }
    public int y { get; set; }
    public int z { get; set; }
}