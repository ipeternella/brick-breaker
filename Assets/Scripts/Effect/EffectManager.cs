using System;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class EffectManager : MonoBehaviour
    {
        private static EffectManager _instance;

        public static EffectManager Instance => _instance;

        private readonly List<Effect> _effects = new List<Effect>();

        private void Awake()
        {
            if (_instance != null) return;
            _instance = this;
        }

        private void Update()
        {
            for (int i = 0; i < _effects.Count; i++)
            {
                var effect = _effects[i];
                if (effect.times <= 0)
                {
                    RemoveEffect(effect, i);
                    i--;
                    continue;
                }

                effect.times -= Time.deltaTime;
                effect.OnUpdate();
            }
        }

        public void AddEffect(Effect effect, EffectType effectType)
        {
            int index = FindSameEffectIndex(effect.name);
            float times = effect.times;
            float value = effect.value;
            switch (effectType)
            {
                case EffectType.Stack:
                {
                    value += _effects[index].value;
                    times += _effects[index].times;
                    break;
                }
                case EffectType.Adding:
                {
                    _effects.Add(effect);
                    effect.OnStart();
                    return;
                }
                case EffectType.AddingTime:
                {
                    times += _effects[index].times;
                    break;
                }
                case EffectType.Replace:
                {

                    break;
                }
            }

            if (index == -1)
            {
                _effects.Add(effect);
                effect.OnStart();
            }
            else
            {
                _effects[index].times = times;
                _effects[index].value = value;
                _effects[index].OnEnd();
                _effects[index].OnStart();
            }
        }

        private void RemoveEffect(Effect effect, int index)
        {
            effect.OnEnd();
            _effects.RemoveAt(index);
        }

        private int FindSameEffectIndex(string effectName)
        {
            for (int i = 0; i < _effects.Count; i++)
            {
                if (_effects[i].name.Equals(effectName)) return i;
            }

            return -1;
        }

        public void Clear()
        {
            while(_effects.Count != 0)
            {
                var effect = _effects[0];
                RemoveEffect(effect, 0);
            }
        }
    }

    public enum EffectType
    {
        Stack,
        Adding,
        AddingTime,
        Replace,
    }
}