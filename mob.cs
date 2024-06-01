using Godot;
using System;

public partial class mob : CharacterBody2D
{
	public const float speed = 300.0f;
	public int health = 3;
	private Node2D player;
	
	private void _on_ready()
	{
		player = GetNode<Node2D>($"../Player");
		AddToGroup("enemies");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
		Velocity = direction * speed;
		MoveAndSlide();
	}
}
