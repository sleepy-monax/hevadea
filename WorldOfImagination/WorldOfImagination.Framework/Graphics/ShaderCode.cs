namespace WorldOfImagination.Framework.Graphics
{
    public static class ShaderCode
    {

        static string vertexShader =
@"#version 330
layout (location = 0) in vec3 vert;
layout (location = 1) in vec3 _uv;
uniform mat4 transform;
out vec2 uv;
void main()
{
    uv = _uv;
    gl_Position = vec4(vert.x / 720.0 - 1.0, vert.y / 405.0 - 1.0, 0.0, 1.0);
}";

        static string fragmentShader =
@"#version 330
out vec4 color;
in vec2 uv;
uniform sampler2D tex;

void main()
{
    color = texture(tex, uv);
}";

    }
}
