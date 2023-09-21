using Godot;
using System;

public class OffsetSlider : HSlider
{
	public override void _Ready()
	{
		HSlider node = GetNode<HSlider>("/root/Node2D/GUI/VBoxContainer/HBoxContainer5/HSlider");
		node.Value = HankinsOptions.getHankinsOptions().offset;

	}


	private void _on_HSlider_value_changed(float value)
	{
		HankinsOptions.getHankinsOptions().offset = value;
		HankinsOptions.getHankinsOptions().SerializeNow();
	}
}
