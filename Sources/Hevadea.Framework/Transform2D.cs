using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Hevadea.Framework
{
    public class Transform2D
    {
        private Transform2D _parent;
        private List<Transform2D> _children = new List<Transform2D>();
        private Matrix _absolute, _invertAbsolute, _local;
        private float _localRotation, _absoluteRotation;
        private Vector2 _localScale, _absoluteScale, _localPosition, _absolutePosition;
        private bool _needsAbsoluteUpdate = true, _needsLocalUpdate = true;
        
        public Transform2D Parent
        {
            get => _parent;
            set
            {
                if (_parent == value) return;
                
                _parent?._children.Remove(this);
                _parent = value;
                _parent?._children.Add(this);
                
                SetNeedsAbsoluteUpdate();
            }
        }

        public List<Transform2D> Children => _children;
        public Matrix Local => UpdateLocalAndGet(ref _absolute);
        public Matrix Absolute => UpdateAbsoluteAndGet(ref _absolute);
        public Matrix InvertAbsolute => UpdateAbsoluteAndGet(ref _invertAbsolute);
        public float AbsoluteRotation => UpdateAbsoluteAndGet(ref _absoluteRotation);
        public Vector2 AbsoluteScale => UpdateAbsoluteAndGet(ref _absoluteScale);
        public Vector2 AbsolutePosition => UpdateAbsoluteAndGet(ref _absolutePosition);
        
        public float Rotation
        {
            get => _localRotation;
            set
            {
                if (_localRotation != value)
                {
                    _localRotation = value;
                    SetNeedsLocalUpdate();
                }
            }
        }

        public Vector2 Position
        {
            get => _localPosition;
            set
            {
                if(_localPosition != value)
                {
                    _localPosition = value;
                    SetNeedsLocalUpdate();
                }
            }
        }
        
        public Vector2 Scale
        {
            get => _localScale;
            set
            {
                if (_localScale != value)
                {
                    _localScale = value;
                    SetNeedsLocalUpdate();
                }
            }
        }

        
        public Transform2D()
        {
            Position = Vector2.Zero;
            Rotation = 0;
            Scale = Vector2.One;
        }

        public Transform2D(Vector2 position, float rotation, Vector2 scale)
        {
            Position = position;
            Rotation = Rotation;
            Scale = scale;
        }

        public void ToLocalPosition(ref Vector2 absolute, out Vector2 local)
        {
            Vector2.Transform(ref absolute, ref _invertAbsolute, out local);
        }

        public void ToAbsolutePosition(ref Vector2 local, out Vector2 absolute)
        {
            Vector2.Transform(ref local, ref _absolute, out absolute);
        }

        public Vector2 ToLocalPosition(Vector2 absolute)
        {
            ToLocalPosition(ref absolute, out var result);
            return result;
        }

        public Vector2 ToAbsolutePosition(Vector2 local)
        {
            ToAbsolutePosition(ref local, out var result);
            return result;
        }

        private void SetNeedsLocalUpdate()
        {
            _needsLocalUpdate = true;
            SetNeedsAbsoluteUpdate();
        }

        private void SetNeedsAbsoluteUpdate()
        {
            _needsAbsoluteUpdate = true;

            foreach (var child in _children)
            {
                child.SetNeedsAbsoluteUpdate();
            }
        }

        private void UpdateLocal()
        {
            var result = Matrix.CreateScale(Scale.X, Scale.Y, 1);
            result *= Matrix.CreateRotationZ(Rotation);
            result *= Matrix.CreateTranslation(Position.X, Position.Y, 0);
            _local = result;

            _needsLocalUpdate = false;
        }

        private void UpdateAbsolute()
        {
            if (Parent == null)
            {
                _absolute = _local;
                _absoluteScale = _localScale;
                _absoluteRotation = _localRotation;
                _absolutePosition = _localPosition;
            }
            else
            {
                var parentAbsolute = Parent.Absolute;
                Matrix.Multiply(ref _local, ref parentAbsolute, out _absolute);
                _absoluteScale = Parent.AbsoluteScale * Scale;
                _absoluteRotation = Parent.AbsoluteRotation + Rotation;
                _absolutePosition = Vector2.Zero;
                ToAbsolutePosition(ref _absolutePosition, out _absolutePosition);
            }

            Matrix.Invert(ref _absolute, out _invertAbsolute);

            _needsAbsoluteUpdate = false;
        }

        private T UpdateLocalAndGet<T>(ref T field)
        {
            if (_needsLocalUpdate)
            {
                UpdateLocal();
            }

            return field;
        }

        private T UpdateAbsoluteAndGet<T>(ref T field)
        {
            if (_needsLocalUpdate)
            {
                UpdateLocal();
            }

            if (_needsAbsoluteUpdate)
            {
                UpdateAbsolute();
            }

            return field;
        }
    }
}