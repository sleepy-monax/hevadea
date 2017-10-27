#version %glslVersion%

#ifdef GL_ES
precision mediump float;
#endif

layout (location = 0) in vec3 vertex;
layout (location = 1) in vec2 texture;
layout (location = 2) in vec4 color;

uniform mat4 view;
uniform mat4 projection;
uniform mat4 transform;

out vec2 pass_texture;
out vec4 pass_color;

void main(void)
{
	pass_texture 	= texture;
	pass_color 		= color;

	vec4 world_position = transform * vec4(vertex, 1.0);
	vec4 position_from_camera = view * world_position;

  gl_Position = projection * position_from_camera;
}
