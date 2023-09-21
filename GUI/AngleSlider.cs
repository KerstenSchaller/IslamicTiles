using Godot;
using System;

public class AngleSlider : HSlider
{
	public override void _Ready()
	{
		HSlider node = GetNode<HSlider>("/root/Node2D/GUI/VBoxContainer/HBoxContainer4/AngleHSlider");
		node.Value = HankinsOptions.getHankinsOptions().AngleGrad;

	}
	private void _on_HSlider_value_changed(float value)
	{
		GD.Print("Angle changed: " + value);
		HankinsOptions.getHankinsOptions().AngleGrad = value;
		HankinsOptions.getHankinsOptions().SerializeNow();
	}
}

