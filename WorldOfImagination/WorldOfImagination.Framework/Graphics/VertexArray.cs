using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace WorldOfImagination.Framework.Graphics
{
    public class VertexArray
    {
        public readonly int handle;
        private List<int> VBOHandles;
        private List<int> Attributes;
        private Vertex[] vertecies;
        int[] vertexIndices;
        public VertexArray(int size)
        {
            handle = GL.GenVertexArray();   
            vertecies = new Vertex[size];
            vertexIndices = new int[size];
        }

        public Vertex this[int i]
        {
            get{ return vertecies[i];}
            set{ vertecies[i] = value;}
        }

        public void SetIndecesBuffer(int[] indeces)
        {
            vertexIndices = indeces;
            GL.BindVertexArray(handle);

            int IndecesBufferHandle = GL.GenBuffer();
            VBOHandles.Add(IndecesBufferHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndecesBufferHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indeces.Length * sizeof(int), indeces, BufferUsageHint.StaticCopy);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindVertexArray(0);
        }
        private void StoreAttribute(int AttributeIndex, float[] data, int size = 3)
        {
            Attributes.Add(AttributeIndex);
            GL.BindVertexArray(handle);
            int VBO = GL.GenBuffer();
            VBOHandles.Add(VBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(AttributeIndex, size, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }
        
        public void Flush()
        {
            List<float> vertexPositionData = new List<float>();
            List<float> vertexTextureCoordinate = new List<float>();

            foreach (var v in vertecies)
            {
                vertexPositionData.Add(v.Position.X);
                vertexPositionData.Add(v.Position.Y);
                vertexPositionData.Add(v.Position.Z);

                vertexTextureCoordinate.Add(v.Texture.X);
                vertexTextureCoordinate.Add(v.Texture.Y);
            }

            SetIndecesBuffer(vertexIndices);
            StoreAttribute(0, vertexPositionData.ToArray(), 3);
            StoreAttribute(1, vertexTextureCoordinate.ToArray(), 2);
        }
        public void Bind()
        {
            GL.BindVertexArray(handle);
            foreach (var a in Attributes)
            {
                GL.EnableVertexAttribArray(a);
            }
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
            foreach (var a in Attributes)
            {
                GL.DisableVertexAttribArray(a);
            }
        }

        public void Destroy()
        {
            foreach (var vbo in VBOHandles)
            {
                GL.DeleteBuffer(vbo);
            }

            GL.DeleteVertexArray(handle);
        }
    }
}