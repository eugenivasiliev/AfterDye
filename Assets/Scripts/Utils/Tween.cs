using System;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Tween
{
    [Serializable]
    public abstract class Tween<Type>
    {
        public abstract Type Value { get; }
        [SerializeField] protected Type startValue;
        public Type StartValue { get => startValue; set => startValue = value; }
        [SerializeField] protected Type endValue;
        public Type EndValue { get => endValue; set => endValue = value; }
        [SerializeField, Range(0 , 1)] protected float t;
        public float T { get => t; set => t = value; }
        [SerializeField] protected float duration;
        public float Duration { get => duration; set => duration = value; }
        [SerializeField] protected bool clamped;
        public bool Clamped { get => clamped; set => clamped = value; }
        [SerializeField] protected bool reversing;
        public bool Reversing { get => reversing; set => reversing = value; }

        public void Init(Type startValue, Type endValue, float t = 0, float duration = 1, bool clamped = false, bool reversing = false)
        {
            this.startValue = startValue;
            this.endValue = endValue;
            this.t = t;
            this.duration = duration;
            this.clamped = clamped;
            this.reversing = reversing;
        }

        public void Update(float dt)
        {
            t += ( reversing ? -1 : 1 ) * dt / duration;
            if(clamped) t = Mathf.Clamp01(t);
        }

        public void Reverse() => reversing = !reversing;
        public void Reverse(bool state) => reversing = state;
    }

    [Serializable]
    public class Tweenf : Tween<float>
    {
        public override float Value => (1 - t) * startValue + t * endValue;
    }

    [Serializable]
    public class TweenCWValues : Tween<ColourWheelUI.UIValues>
    {
        public override ColourWheelUI.UIValues Value => ColourWheelUI.UIValues.Lerp(startValue, endValue, t);
    }
}
