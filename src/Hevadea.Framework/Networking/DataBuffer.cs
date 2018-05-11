using System;
using System.Collections.Generic;
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

    public sealed class DataBuffer
    {
        private byte[] _buffer;
        private int _offset;

        public DataBuffer(int preAllocatedSize = 2)
        {
            PreAllocate(preAllocatedSize);
            _offset = 0;
        }

        public DataBuffer(byte[] data)
        {
            _buffer = data;
            _offset = 0;
        }

        public void PreAllocate(int size)
        {
            _buffer = new byte[size];
        }

        public byte[] GetBuffer()
        {
            return _buffer;
        }

        private void Resize(int newSize)
        {
            if (newSize < _buffer.Length) return;

            var tmp = new byte[_buffer.Length];

            while (newSize >= tmp.Length)
                tmp = new byte[(tmp.Length << 1)];

            for (int i = 0; i < _buffer.Length; i++)
            {
                tmp[i] = _buffer[i];
            }

            _buffer = tmp;
        }

        public void Flush()
        {
            PreAllocate(2);
            SetOffset(0);
        }

        public int GetOffset()
        {
            return _offset;
        }

        public void SetOffset(int value)
        {
            _offset = value;
        }

        public byte[] ReadBytes()
        {
            if (_buffer.Length > _offset)
            {
                var tmp = new byte[_offset];

                for (int i = 0; i < _offset; i++)
                {
                    tmp[i] = _buffer[i];
                }

                return tmp;
            }

            return _buffer;
        }

        public void FillBuffer(byte[] bytes)
        {
            _buffer = bytes;
            _offset = 0;
        }

        public void Begin()
        {
            _offset = 0;
        }

        public void WriteBytes(byte[] bytes, int destOffset)
        {
            Resize(_offset + bytes.Length);

            foreach (byte t in bytes)
                _buffer[destOffset++] = t;
        }

        public void WriteBytes(byte[] bytes)
        {
            Resize(_offset + bytes.Length);

            foreach (byte t in bytes)
                _buffer[_offset++] = t;
        }

        public DataBuffer WriteByte(byte value)
        {
            Resize(_offset + 1);

            _buffer[_offset++] = value;

            return this;
        }

        public DataBuffer WriteLong(long value)
        {
            byte[] tmp = BitConverter.GetBytes(value);
            Resize(_offset + tmp.Length);

            for (int i = 0; i < 8; i++)
            {
                _buffer[_offset++] = tmp[i];
            }

            return this;
        }

        public DataBuffer WriteInteger(int value)
        {
            Resize(_offset + 4);

            _buffer[_offset++] = (byte)(value);
            _buffer[_offset++] = (byte)(value >> 8);
            _buffer[_offset++] = (byte)(value >> 16);
            _buffer[_offset++] = (byte)(value >> 24);

            return this;
        }

        public DataBuffer WriteShort(short value)
        {
            Resize(_offset + 2);

            _buffer[_offset++] = (byte)(value);
            _buffer[_offset++] = (byte)(value >> 8);

            return this;
        }

        public DataBuffer WriteStringASCII(string value) => WriteString(value, Encoding.ASCII);

        public DataBuffer WriteStringUTF8(string value) => WriteString(value, Encoding.UTF8);

        public DataBuffer WriteString(string value, Encoding encoding)
        {
            byte[] tmp = encoding.GetBytes(value);
            WriteInteger(tmp.Length);
            Resize(_offset + tmp.Length);

            foreach (byte t in tmp)
            {
                _buffer[_offset++] = t;
            }

            return this;
        }

        public DataBuffer WriteBool(bool value)
        {
            WriteByte(value ? (byte)1 : (byte)0);

            return this;
        }

        public DataBuffer WriteFloat(Single value)
        {
            BitConverter.GetBytes(value);
            return this;
        }

        public DataBuffer WriteBuffer(DataBuffer buffer)
        {
            WriteBytes(buffer.GetBuffer());
            return this;
        }

        public DataBuffer WriteBuffer(DataBuffer buffer, int begin, int end)
        {
            var bufferData = buffer.GetBuffer();
            end = Math.Min(end, bufferData.Length);

            Resize(_offset + (end - begin));

            for (int i = begin; i < end; i++)
            {
                _buffer[_offset++] = bufferData[i];
            }

            return this;
        }

        public DataBuffer WriteBuffer(Dictionary<string, object> dic)
        {
            return this;
        }

        public DataBuffer ReadBool(out bool outValue)
        {
            outValue = (ReadByte() == 1);

            return this;
        }

        public bool ReadBool()
        {
            return (ReadByte() == 1);
        }

        public DataBuffer ReadByte(out byte outValue)
        {
            outValue = _buffer[_offset++];
            return this;
        }

        public byte ReadByte()
        {
            return _buffer[_offset++];
        }

        public DataBuffer ReadShort(out short outValue)
        {
            outValue = (short)(_buffer[_offset++] | _buffer[_offset++] << 8);

            return this;
        }

        public short ReadShort()
        {
            return (short)(_buffer[_offset++] | _buffer[_offset++] << 8);
        }

        public DataBuffer ReadInteger(out int outValue)
        {
            outValue = (int)(_buffer[_offset++] | _buffer[_offset++] << 8 | _buffer[_offset++] << 16 | _buffer[_offset++] << 24);

            return this;
        }

        public int ReadInteger()
        {
            return (int)(_buffer[_offset++] | _buffer[_offset++] << 8 | _buffer[_offset++] << 16 | _buffer[_offset++] << 24);
        }

        public DataBuffer ReadLong(out long outValue)
        {
            outValue = BitConverter.ToInt64(_buffer, _offset);

            _offset += 8;

            return this;
        }

        public long ReadLong()
        {
            var value = BitConverter.ToInt64(_buffer, _offset);

            _offset += 8;

            return value;
        }

        public DataBuffer ReadString(out string outValue)
        {
            var size = ReadInteger();
            var tmpData = new byte[size];

            for (int i = 0; i < size; i++)
            {
                tmpData[i] = _buffer[_offset++];
            }

            outValue = Encoding.ASCII.GetString(tmpData).TrimEnd('\0').TrimStart('\0');

            return this;
        }

        public string ReadString()
        {
            var size = ReadInteger();
            var tmpData = new byte[size];

            for (int i = 0; i < size; i++)
            {
                tmpData[i] = _buffer[_offset++];
            }

            return Encoding.ASCII.GetString(tmpData).TrimEnd('\0').TrimStart('\0');
        }
    }
}