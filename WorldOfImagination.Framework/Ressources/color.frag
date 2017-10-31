#version %glslVersion%

#ifdef GL_ES
precision mediump float;
#endif

uniform vec4 material_color;

in vec2 pass_texture;

out vec4 color;

void main(void)
{
  color = material_color;
}
