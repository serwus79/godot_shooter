using Godot;

public partial class Player : CharacterBody2D
{
    private const float MoveSpeed = 400.0f;
    private const float BulletSpeed = 2000f;
    private PackedScene _bulletScene;
    private Node2D _barrel;

    public override void _Ready()
    {
        base._Ready();
        
        _bulletScene = ResourceLoader.Load<PackedScene>("res://bullet.tscn");
                _barrel = (Node2D)this.FindChild("BarrelPoint");
    }


    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;

        // // Add the gravity.
        // if (!IsOnFloor())
        // 	velocity.Y += gravity * (float)delta;
        //
        // // Handle Jump.
        // if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        // 	velocity.Y = JumpVelocity;
        //
        // // Get the input direction and handle the movement/deceleration.
        // // As good practice, you should replace UI actions with custom gameplay actions.
        // Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        // if (direction != Vector2.Zero)
        // {
        // 	velocity.X = direction.X * Speed;
        // }
        // else
        // {
        // 	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        // }
        //
        // Velocity = velocity;

        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        velocity = direction * MoveSpeed; //.MoveToward(direction, MoveSpeed);

        Velocity = velocity;


        MoveAndSlide();
        LookAt(GetGlobalMousePosition());

        if (Input.IsActionJustPressed("LMB") )
        {
            Fire();
        }
    }

    private void Fire()
    {
        GD.Print("fire");
        var bullet = _bulletScene.Instantiate<RigidBody2D>();
        bullet.Position = _barrel.GlobalPosition;
        bullet.RotationDegrees = RotationDegrees;

        var shootDirection = new Vector2(BulletSpeed, 0).Rotated(Rotation);
        bullet.ApplyImpulse(shootDirection);

        // GetParent().AddChild(bullet);
        GetTree().Root.AddChild(bullet);
    }
    
}