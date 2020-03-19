shader_type spatial;
render_mode cull_disabled;

uniform sampler2D Texture;

void fragment()
{
	vec4 color = texture(Texture, UV);
	ALBEDO = color.rgb;
	if(color.a < .5) discard;
}