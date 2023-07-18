using System;
using UnityEngine;

namespace Input
{
    public interface IInputService
    {
        public event Action<Vector2> PlayerInputUpdated;
        void Enable();
        void Disable();
    }
}