using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class MonoService : MonoBehaviour
    {
        public event Action UpdateTick;

        public Coroutine RunCoroutine(IEnumerator enumerator) =>
            StartCoroutine(enumerator);

        public Coroutine RunCoroutine<T>(IEnumerator<T> enumerator) =>
            StartCoroutine(enumerator);

        private void Update() =>
            UpdateTick?.Invoke();
    }
}