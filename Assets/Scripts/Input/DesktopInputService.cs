using System;
using Infrastructure.Services;
using UnityEngine;

namespace Input
{
    public class DesktopInputService : IInputService
    {
        public event Action<Vector2> PlayerInputUpdated;

        private const string VerticalAxisName = "Vertical";
        private const string HorizontalAxisName = "Horizontal";
        
        private readonly MonoService _monoService;

        public DesktopInputService(MonoService monoService) => 
            _monoService = monoService;

        public void Enable() => 
            _monoService.UpdateTick += OnTickUpdate;

        public void Disable() => 
            _monoService.UpdateTick -= OnTickUpdate;

        private void OnTickUpdate() =>
            PlayerInputUpdated?.Invoke(new Vector2(UnityEngine.Input.GetAxis(HorizontalAxisName), UnityEngine.Input.GetAxis(VerticalAxisName)));
    }
}