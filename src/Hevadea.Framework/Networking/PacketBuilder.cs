using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Hevadea.Framework.Networking
{
    internal enum TypeId
    {
        IntType,
        FloatType,
        StringType,
        BoolType,
    }

    public sealed class PacketBuilder
    {
        public int Offset { get; private set; }
        public byte[] Buffer { get; private set; }

        public PacketBuilder(int preAllocatedSize = 2)
        {
            PreAllocate(preAllocatedSize);
            Offset = 0;
        }

        public PacketBuilder(byte[] data)
        {
            Buffer = data;
            Offset = 0;
        }

        public PacketBuilder PreAllocate(int size)
        {
            Buffer = new byte[size];
            return this;
        }

        private void Resize(int newSize)
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

        public PacketBuilder Ignore(int count)
        {
            Offset += count;
            return this;
        }

        public void Flush()
        {
            PreAllocate(2);
            Offset = 0;
        }

        public byte[] ReadBytes()
        {
            if (Buffer.Length > Offset)
            {
                var tmp = new byte[Offset];

                for (int i = 0; i < Offset; i++)
                {
                    tmp[i] = Buffer[i];
                }

                return tmp;
            }

            return Buffer;
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

        public PacketBuilder WriteByte(byte value)
        {
            Resize(Offset + 1);

            Buffer[Offset++] = value;

            return this;
        }

        public PacketBuilder WriteLong(long value)
        {
            byte[] tmp = BitConverter.GetBytes(value);
            Resize(Offset + tmp.Length);

            for (int i = 0; i < 8; i++)
            {
                Buffer[Offset++] = tmp[i];
            }

            return this;
        }

        public PacketBuilder WriteInteger(int value)
        {
            Resize(Offset + 4);

            Buffer[Offset++] = (byte)(value);
            Buffer[Offset++] = (byte)(value >> 8);
            Buffer[Offset++] = (byte)(value >> 16);
            Buffer[Offset++] = (byte)(value >> 24);

            return this;
        }

        public PacketBuilder WriteShort(short value)
        {
            Resize(Offset + 2);

            Buffer[Offset++] = (byte)(value);
            Buffer[Offset++] = (byte)(value >> 8);

            return this;
        }

        public PacketBuilder WriteStringASCII(string value) => WriteString(value, Encoding.ASCII);

        public PacketBuilder WriteStringUTF8(string value) => WriteString(value, Encoding.UTF8);

        public PacketBuilder WriteString(string value, Encoding encoding)
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

        public PacketBuilder WriteBool(bool value)
        {
            WriteByte(value ? (byte)1 : (byte)0);

            return this;
        }

        public PacketBuilder WriteFloat(Single value)
        {
            BitConverter.GetBytes(value);
            return this;
        }

        public PacketBuilder WriteBuffer(PacketBuilder builder)
        {
            WriteBytes(builder.Buffer);
            return this;
        }

        public PacketBuilder WriteBuffer(PacketBuilder builder, int begin, int end)
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

        public PacketBuilder WriteBuffer(Dictionary<string, object> dic)
        {
            return this;
        }

        public PacketBuilder ReadBool(out bool outValue)
        {
            outValue = (ReadByte() == 1);

            return this;
        }

        public bool ReadBool()
        {
            return (ReadByte() == 1);
        }

        public PacketBuilder ReadByte(out byte outValue)
        {
            outValue = Buffer[Offset++];
            return this;
        }

        public byte ReadByte()
        {
            return Buffer[Offset++];
        }

        public PacketBuilder ReadShort(out short outValue)
        {
            outValue = (short)(Buffer[Offset++] | Buffer[Offset++] << 8);

            return this;
        }

        public short ReadShort()
        {
            return (short)(Buffer[Offset++] | Buffer[Offset++] << 8);
        }

        public PacketBuilder ReadInteger(out int outValue)
        {
            outValue = (int)(Buffer[Offset++] | Buffer[Offset++] << 8 | Buffer[Offset++] << 16 | Buffer[Offset++] << 24);

            return this;
        }

        public int ReadInteger()
        {
            return (int)(Buffer[Offset++] | Buffer[Offset++] << 8 | Buffer[Offset++] << 16 | Buffer[Offset++] << 24);
        }

        public PacketBuilder ReadLong(out long outValue)
        {
            outValue = BitConverter.ToInt64(Buffer, Offset);

            Offset += 8;

            return this;
        }

        public long ReadLong()
        {
            var value = BitConverter.ToInt64(Buffer, Offset);

            Offset += 8;

            return value;
        }

        public PacketBuilder ReadStringASCII(out string outValue) => ReadString(Encoding.ASCII, out outValue);
        public PacketBuilder ReadStringUTF8(out string outValue) => ReadString(Encoding.UTF8, out outValue);

        public PacketBuilder ReadString(Encoding encoding, out string outValue)
        {
            var size = ReadInteger();
            var tmpData = new byte[size];

            for (int i = 0; i < size; i++)
            {
                tmpData[i] = Buffer[Offset++];
            }

            outValue = encoding.GetString(tmpData).TrimEnd('\0').TrimStart('\0');

            return this;
        }

        public string ReadStringASCII() => ReadString(Encoding.ASCII);
        public string ReadStringUTF8() => ReadString(Encoding.UTF8);

        public string ReadString(Encoding encoding)
        {
            var size = ReadInteger();
            var tmpData = new byte[size];

            for (int i = 0; i < size; i++)
            {
                tmpData[i] = Buffer[Offset++];
            }

            return encoding.GetString(tmpData).TrimEnd('\0').TrimStart('\0');
        }
    }
}