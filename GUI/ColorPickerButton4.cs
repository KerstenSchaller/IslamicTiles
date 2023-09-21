using Godot;
using System;

public class ColorPickerButton4 : ColorPickerButton
{
	public override void _Ready()
	{
		ColorPickerButton node = GetNode<ColorPickerButton>("/root/Node2D/GUI/VBoxContainer/HBoxContainer8/ColorPickerButton4");
		node.Color = HankinsOptions.getHankinsOptions().colors[3];
	}
	private void _on_ColorPickerButton4_color_changed(Color color)
	{
		HankinsOptions.getHankinsOptions().colors[3] = color;
		HankinsOptions.getHankinsOptions().SerializeNow();
	}
}


