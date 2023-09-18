using Godot;
using System;

public class ColorPickerButton1 : ColorPickerButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Color = Colors.Red;
        _on_ColorPickerButton_color_changed(this.Color);
	}

    private void _on_ColorPickerButton_color_changed(Color color)
    {
        HankinsOptions.colors[2] = color;
    }
}



