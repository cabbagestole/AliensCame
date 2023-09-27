shader_type canvas_item;
uniform float fill = 0;
void fragment() {
	vec2 vec = UV;
	float deg = atan(UV.y-0.5, UV.x-0.5) / (PI *2.0 ) + 0.5;// degは0.0～1.0の範囲
//	float t = TIME - floor(TIME);//テスト用.scriptからfillを操作する
//	if(deg < t){
	if(deg < fill){
		COLOR = texture(TEXTURE,UV);
	}else{
		COLOR = vec4(0.0, 0.0, 0.0, 0.0);
	}
}
