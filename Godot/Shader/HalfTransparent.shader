shader_type spatial;

uniform vec4 Color : hint_color;
uniform int PointSize = 1;

void fragment()
{
	ivec2 pUV = ivec2(SCREEN_UV * VIEWPORT_SIZE) / PointSize;
	bool xOdd = (pUV.x % 2) == 0;
	bool yOdd = (pUV.y % 2) == 0;
	ALBEDO = Color.rgb;
	if(xOdd == yOdd) discard;
}