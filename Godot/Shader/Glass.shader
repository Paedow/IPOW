shader_type spatial;

uniform sampler2D Displacement;

void fragment()
{
	vec2 disp = texture(Displacement, UV).xy - vec2(.5,.5);
	ALBEDO = textureLod(SCREEN_TEXTURE, SCREEN_UV + disp * .1, 0.0).rgb;
}