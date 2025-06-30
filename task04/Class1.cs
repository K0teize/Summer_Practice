namespace task04;

public interface ISpaceship
{
    void MoveForward();
    void Rotate(int angle);
    void Fire();
    int Speed { get; }
    int FirePower { get; }

}

public class Cruiser : ISpaceship
{
    public void MoveForward() => Distance += Speed;
    public void Rotate(int angle) => Angle=(angle + Angle) % 360;
    public void Fire() => IsShot = true;
    public int Speed{ get; } = 50;
    public int FirePower { get; } = 100;
    public int Distance;
    public int Angle;
    public bool IsShot;
}

public class Fighter : ISpaceship
{
    public void MoveForward() => Distance += Speed;
    public void Rotate(int angle) => Angle=(angle + Angle) % 360;
    public void Fire() => IsShot = true;
    public int Speed{ get; } = 100;
    public int FirePower { get; } = 50;
    public int Distance;
    public int Angle;
    public bool IsShot;
}