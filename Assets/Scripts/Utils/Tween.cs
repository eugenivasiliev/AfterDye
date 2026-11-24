using System;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Tween
{
    [Serializable]
    public abstract class Tween<Type>
    {
        public abstract float Value { get; }
        public float T => t;
        [SerializeField] protected Type startValue;
        [SerializeField] protected Type endValue;
        [SerializeField, Range(0 , 1)] protected float t;
        [SerializeField] protected float duration;
        [SerializeField] protected bool clamped;
        [SerializeField] protected bool reversing;

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
    }

    [Serializable]
    public class Tweenf : Tween<float>
    {
        public override float Value => (1 - t) * startValue + t * endValue;
    }
}
