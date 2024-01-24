using Godot;

public partial class Player : CharacterBody2D
{
    private const float MoveSpeed = 400.0f;
    private const float BulletSpeed = 2000f;
    private Node2D _barrel;
    private PackedScene _bulletScene;

    public override void _Ready()
    {
        base._Ready();

        _bulletScene = ResourceLoader.Load<PackedScene>("res://bullet.tscn");
        _barrel = (Node2D)FindChild("BarrelPoint");
        var screenSize = GetViewportRect().Size;

        Position = screenSize / 2;
    }


    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;

        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        velocity = direction * MoveSpeed; //.MoveToward(direction, MoveSpeed);

        Velocity = velocity;


        MoveAndSlide();
        LookAt(GetGlobalMousePosition());

        if (Input.IsActionJustPressed("LMB")) Fire();
    }

    private void Fire()
    {
        var bullet = _bulletScene.Instantiate<RigidBody2D>();
        bullet.SetMeta("IsBullet", true);
        bullet.Position = _barrel.GlobalPosition;
        bullet.RotationDegrees = RotationDegrees;

        var shootDirection = new Vector2(BulletSpeed, 0).Rotated(Rotation);
        bullet.ApplyImpulse(shootDirection);

        // GetParent().AddChild(bullet);
        GetTree().Root.AddChild(bullet);
    }

    private void Kill()
    {
        GD.Print("KILL");
        GetTree().ReloadCurrentScene();
    }

    public void OnArea2BodyEntered(Node2D body)
    {
        if (body.Name == "Enemy")
        {
            GD.Print("player HIT by ", body.Name);
            Kill();
        }
    }
}