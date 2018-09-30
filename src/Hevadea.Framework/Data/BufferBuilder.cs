using System;
using System.Collections.Generic;
using System.Text;

namespace Hevadea.Framework.Data
{
    public class BufferBuilder
    {
        public int Offset { get; private set; }
        public byte[] Buffer { get; private set; }

        public BufferBuilder(int preAllocatedSize = 2)
        {
            PreAllocate(preAllocatedSize);
            Offset = 0;
        }

        public BufferBuilder(byte[] data)
        {
            Buffer = data;
            Offset = 0;
        }

        public BufferBuilder PreAllocate(int size)
        {
            Buffer = new byte[size];
            return this;
        }

        void Resize(int newSize)
        {
            if (newSize < Buffer.Length) return;

            var tmp = new byte[Buffer.Length];

            while (newSize >= tmp.Length)
                tmp = new byte[(tmp.Length << 1)];

            for (int i = 0; i < Buffer.Length; i++)
            {
                tmp[i] = Buffer[i];
            }

            Buffer = tmp;
        }

        public void Flush()
        {
            PreAllocate(2);
            Offset = 0;
        }

        public void FillBuffer(byte[] bytes)
        {
            Buffer = bytes;
            Offset = 0;
        }

        public void Begin()
        {
            Offset = 0;
        }

        public void WriteBytes(byte[] bytes, int destOffset)
        {
            Resize(Offset + bytes.Length);

            foreach (byte t in bytes)
                Buffer[destOffset++] = t;
        }

        public void WriteBytes(byte[] bytes)
        {
            Resize(Offset + bytes.Length);

            foreach (byte t in bytes)
                Buffer[Offset++] = t;
        }

        public BufferBuilder WriteByte(byte value)
        {
            Resize(Offset + 1);

            Buffer[Offset++] = value;

            return this;
        }

        public BufferBuilder WriteLong(long value)
        {
            byte[] tmp = BitConverter.GetBytes(value);
            Resize(Offset + tmp.Length);

            for (int i = 0; i < 8; i++)
            {
                Buffer[Offset++] = tmp[i];
            }

            return this;
        }

        public BufferBuilder WriteInteger(int value)
        {
            Resize(Offset + 4);

            Buffer[Offset++] = (byte)(value);
            Buffer[Offset++] = (byte)(value >> 8);
            Buffer[Offset++] = (byte)(value >> 16);
            Buffer[Offset++] = (byte)(value >> 24);

            return this;
        }

        public BufferBuilder WriteShort(short value)
        {
            Resize(Offset + 2);

            Buffer[Offset++] = (byte)(value);
            Buffer[Offset++] = (byte)(value >> 8);

            return this;
        }

        public BufferBuilder WriteStringASCII(string value) => WriteString(value, Encoding.ASCII);

        public BufferBuilder WriteStringUTF8(string value) => WriteString(value, Encoding.UTF8);

        public BufferBuilder WriteString(string value, Encoding encoding)
        {
            byte[] tmp = encoding.GetBytes(value);
            WriteInteger(tmp.Length);
            Resize(Offset + tmp.Length);

            foreach (byte t in tmp)
            {
                Buffer[Offset++] = t;
            }

            return this;
        }

        public BufferBuilder WriteBool(bool value)
        {
            WriteByte(value ? (byte)1 : (byte)0);

            return this;
        }

        public BufferBuilder WriteFloat(Single value)
        {
            BitConverter.GetBytes(value);
            return this;
        }

        public BufferBuilder WriteBuffer(BufferBuilder builder)
        {
            WriteBytes(builder.Buffer);
            return this;
        }

        public BufferBuilder WriteBuffer(BufferBuilder builder, int begin, int end)
        {
            var bufferData = builder.Buffer;
            end = Math.Min(end, bufferData.Length);

            Resize(Offset + (end - begin));

            for (int i = begin; i < end; i++)
            {
                Buffer[Offset++] = bufferData[i];
            }

            return this;
        }

        public BufferBuilder WriteBuffer(Dictionary<string, object> dic)
        {
            return this;
        }
    }
}