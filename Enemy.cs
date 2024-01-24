using Godot;

public partial class Enemy : CharacterBody2D
{
    private Vector2 _motion;

    public override void _Ready()
    {
        base._Ready();
        GD.Print("Enemy ready ");
    }

    public override void _PhysicsProcess(double delta)
    {
        var player = GetParent().GetNode<CharacterBody2D>("Player");

        Position += (player.Position - Position) / 50;
        LookAt(player.Position);

        MoveAndCollide(_motion);
    }

    public void OnArea2BodyEntered(Node2D body)
    {
        GD.Print("Enemy HIT by", body.Name);
        if ((bool)body.GetMeta("IsBullet")) QueueFree();
    }
}