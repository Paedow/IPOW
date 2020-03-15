shader_type spatial;

uniform vec2 SurfaceSize = vec2(1,1);
uniform vec2 TextureSize = vec2(1,1);
uniform sampler2D Texture;

void fragment()
{
	vec2 _uv = UV * SurfaceSize / TextureSize;
	ALBEDO = texture(Texture, _uv).rgb;
}