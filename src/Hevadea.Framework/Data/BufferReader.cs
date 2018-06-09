using System;
using System.Text;

namespace Hevadea.Framework.Data
{
    public class BufferReader
    {
        public int Offset { get; private set; }
        public byte[] Buffer { get; private set; }

        public BufferReader(byte[] data)
        {
            Buffer = data;
            Offset = 0;
        }

        public BufferReader Ignore(int count)
        {
            Offset += count;
            return this;
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

        public void Begin()
        {
            Offset = 0;
        }

        public BufferReader ReadBool(out bool outValue)
        {
            outValue = (ReadByte() == 1);

            return this;
        }

        public bool ReadBool()
        {
            return (ReadByte() == 1);
        }

        public BufferReader ReadByte(out byte outValue)
        {
            outValue = Buffer[Offset++];
            return this;
        }

        public byte ReadByte()
        {
            return Buffer[Offset++];
        }

        public BufferReader ReadShort(out short outValue)
        {
            outValue = (short)(Buffer[Offset++] | Buffer[Offset++] << 8);

            return this;
        }

        public short ReadShort()
        {
            return (short)(Buffer[Offset++] | Buffer[Offset++] << 8);
        }

        public BufferReader ReadInteger(out int outValue)
        {
            outValue = (int)(Buffer[Offset++] | Buffer[Offset++] << 8 | Buffer[Offset++] << 16 | Buffer[Offset++] << 24);

            return this;
        }

        public int ReadInteger()
        {
            return (int)(Buffer[Offset++] | Buffer[Offset++] << 8 | Buffer[Offset++] << 16 | Buffer[Offset++] << 24);
        }

        public BufferReader ReadLong(out long outValue)
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

        public BufferReader ReadStringASCII(out string outValue) => ReadString(Encoding.ASCII, out outValue);

        public BufferReader ReadStringUTF8(out string outValue) => ReadString(Encoding.UTF8, out outValue);

        public BufferReader ReadString(Encoding encoding, out string outValue)
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