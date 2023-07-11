shader_type canvas_item;

void fragment() {
	vec2 uv = vec2(UV.x + (TIME / 20.0), UV.y);
	
	COLOR = texture(TEXTURE, uv);
}
