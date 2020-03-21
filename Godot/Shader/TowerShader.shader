shader_type spatial;

uniform sampler2D Texture;
uniform vec4 AlbedoColor : hint_color = vec4(1,1,1,1);
uniform int PointSize = 1;
uniform vec4 HighlightColor : hint_color = vec4(0,1,1,1);
uniform bool ShowTexture = true;
uniform bool Highlight = false;
uniform vec2 Tiling = vec2(1,1);
uniform vec2 Offset = vec2(0,0);

void fragment()
{
	ivec2 pUV = ivec2(SCREEN_UV * VIEWPORT_SIZE) / PointSize;
	bool xOdd = (pUV.x % 2) == 0;
	bool yOdd = (pUV.y % 2) == 0;
	ALBEDO = HighlightColor.rgb;
	bool _highlight = xOdd == yOdd;
	
	if(Highlight && _highlight) ALBEDO = HighlightColor.rgb;
	else if(ShowTexture) 
	{
		ALBEDO = texture(Texture, Tiling * UV + Offset).rgb * AlbedoColor.rgb;
	}
	else discard;
}