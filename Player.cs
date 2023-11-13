using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400;

	public Vector2 ScreenSize;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

    public override void _Process(double delta)
    {
			var velocity = Vector2.Zero;

			if (Input.IsActionPressed("move_right"))
			{
				velocity.X += 1;
			}

			if (Input.IsActionPressed("move_left"))
			{
				velocity.X -= 1;
			}

			if (Input.IsActionPressed("move_down"))
			{
				velocity.Y += 1;
			}

			if (Input.IsActionPressed("move_up"))
			{
				velocity.Y -= 1;
			}

			var animateSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

			if (velocity.X != 0) {
				animateSprite2D.Animation = "walk";
				animateSprite2D.FlipV = false;
				animateSprite2D.FlipH = velocity.X < 0;
			}
			else if (velocity.Y != 0)
			{
				animateSprite2D.Animation = "up";
				animateSprite2D.FlipV = velocity.Y > 0;
			}

			if (velocity.Length() > 0)
			{
				velocity = velocity.Normalized() * Speed;
				animateSprite2D.Play();
			}
			else {
				animateSprite2D.Stop();
			}

			Position += velocity * (float)delta;
			Position = new Vector2(
				x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
				y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
			);
    }
}