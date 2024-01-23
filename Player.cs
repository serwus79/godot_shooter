using Godot;

public partial class Player : CharacterBody2D
{
    public const float MoveSpeed = 500.0f;

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
    }
}