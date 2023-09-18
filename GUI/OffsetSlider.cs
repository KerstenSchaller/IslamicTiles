using Godot;
using System;

public class OffsetSlider : HSlider
{

	private void _on_HSlider_value_changed(float value)
	{
		HankinsOptions.offset = value;
	}
}
