namespace task04tests;

using Xunit;
using task04;
public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }
    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        ISpaceship fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(50, fighter.FirePower);
    }
    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Cruiser_ShouldBePowerfulThanFighter()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(cruiser.FirePower > fighter.FirePower);
    }
    [Fact]
    public void FighterAndCruiser_CorrectRotate()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        fighter.Rotate(90);
        Assert.Equal(90, fighter.Angle);
        cruiser.Rotate(45);
        Assert.Equal(45, cruiser.Angle);
    }
    [Fact]
    public void FighterAndCruiser_CorrectMovement()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        fighter.MoveForward();
        Assert.Equal(100, fighter.Distance);
        cruiser.MoveForward();
        Assert.Equal(50, cruiser.Distance);
        cruiser.MoveForward();
        Assert.Equal(100, cruiser.Distance);
    }
    [Fact]
    public void FighterAndCruiser_CorrectShoot()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        fighter.Fire();
        cruiser.Fire();
        Assert.True(fighter.IsShot);
        Assert.True(cruiser.IsShot);
    }
}
