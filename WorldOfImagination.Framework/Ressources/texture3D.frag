#version %glslVersion%

#ifdef GL_ES
precision mediump float;
#endif

layout(binding = 0) uniform sampler2D material_texture;

in vec2 pass_texture;
in vec4 pass_color;

out vec4 color;

void main(void)
{
  color = texture(material_texture, pass_texture) ;//* pass_color;
}
