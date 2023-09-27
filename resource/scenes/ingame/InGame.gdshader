shader_type canvas_item;
uniform float speed = 0.1;
void fragment() {
	vec2 vec = UV;
	float t = TIME * speed- floor(TIME * speed);//0～1の範囲の小数を得る
	vec.y = (vec.y - t);
	if (vec.y < 0.0) vec.y += 1.0;
	COLOR = texture(TEXTURE,vec);
}
