using Godot;
using System;

public class ColorPickerButton1 : ColorPickerButton
{

	public override void _Ready()
	{
		ColorPickerButton node = GetNode<ColorPickerButton>("/root/Node2D/GUI/VBoxContainer/HBoxContainer7/ColorPickerButton");
		node.Color = HankinsOptions.getHankinsOptions().colors[0];
	}


    private void _on_ColorPickerButton_color_changed(Color color)
    {
        HankinsOptions.getHankinsOptions().colors[0] = color;
        HankinsOptions.getHankinsOptions().SerializeNow();
    }
}



