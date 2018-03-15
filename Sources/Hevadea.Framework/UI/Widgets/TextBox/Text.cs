using System;

namespace Hevadea.Framework.UI.Widgets.TextBox
{
    public class Text
    {
        public string String
        {
            get { return new string(_char).Substring(0, Length); }
            set { ResetText(value); }
        }

        public char[] Characters
        {
            get { return _char; }
            set { ResetText(new string(value)); }
        }

        private int length;

        public int Length
        {
            get { return length; }
            private set
            {
                length = value;
                if (length < MaxLength)
                {
                    _char[length] = '\0';
                }
                IsDirty = true;
            }
        }

        public int MaxLength { get; }

        public bool IsDirty { get; set; }

        private readonly char[] _char;

        public Text(int maxLength)
        {
            MaxLength = maxLength;

            _char = new char[MaxLength];

            IsDirty = true;
        }

        public void InsertCharacter(int location, char character)
        {
            ValidateEditRange(location, location);
            ValidateLenght(location, location, 1);

            // Validation.
            if (!(Length < MaxLength))
            {
                return;
            }

            // Shift everything right once then insert the character into the gap.
            Array.Copy(
                _char, location,
                _char, location + 1,
                Length - location);

            _char[location] = character;
            Length++;
            IsDirty = true;
        }

        public void Replace(int start, int end, string replacement)
        {
            ValidateEditRange(start, end);
            ValidateLenght(start, end, replacement.Length);

            RemoveCharacters(start, end);
            foreach (char character in replacement)
            {
                InsertCharacter(start, character);
                start++;
            }

            IsDirty = true;
        }

        public void RemoveCharacters(int start, int end)
        {
            ValidateEditRange(start, end);

            Array.Copy(_char, end, _char, start, Length - end);
            Length -= end - start;
            IsDirty = true;
        }

        private void ResetText(string value)
        {
            Length = 0;
            ValidateLenght(0, 0, value.Length);

            int x = value.IndexOf('\0');
            if (x != -1)
            {
                value = value.Substring(0, x);
            }

            Length = value.Length;
            Array.Clear(_char, 0, _char.Length);
            value.ToCharArray().CopyTo(_char, 0);
            IsDirty = true;
        }

        // ReSharper disable UnusedParameter.Local
        private void ValidateEditRange(int start, int end)
        {
            if (end > Length || start < 0 || start > end)
            {
                throw new ArgumentException("Invalid character range");
            }
        }

        private void ValidateLenght(int start, int end, int added)
        {
            if (Length - (end - start) + added > MaxLength)
            {
                throw new ArgumentException("Character limit of " + MaxLength + " exceeded.");
            }
        }

        // ReSharper restore UnusedParameter.Local
}
}