using Godot;

public class main : Node2D
{

	public override void _Ready()
	{
		Tesselator tesselator = new Tesselator();
		AddChild(tesselator);
		tesselator.init();
	}



}


