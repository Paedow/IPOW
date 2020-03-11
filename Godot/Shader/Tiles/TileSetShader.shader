shader_type spatial;
render_mode unshaded;

uniform sampler2D tex;
uniform vec2 tileOffset = vec2(0,0);
uniform vec2 tiles = vec2(1,1);

void fragment()
{
	vec2 uv = (UV + tileOffset)/tiles;
	ALBEDO = texture(tex, uv).rgb;
	
}