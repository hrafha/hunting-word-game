using System;
using System.Collections;
using UnityEngine;

namespace Utility
{
    public static class AnimationUtility
    {
        public static Coroutine PopAnimation(this MonoBehaviour monoBehaviour, AnimationCurve animation, Action callback = null)
        {
            return PopAnimation(monoBehaviour, monoBehaviour.transform, animation, callback);
        }


        public static Coroutine PopAnimation(this MonoBehaviour monoBehaviour, Transform transform, AnimationCurve animation, Action callback = null)
        {
            if (monoBehaviour != null && monoBehaviour.isActiveAndEnabled)
            {
                Coroutine coroutine = null;
                coroutine = monoBehaviour.StartCoroutine(PopAnimate(transform, animation,
                    () =>
                    {
                        coroutine = null;
                        callback?.Invoke();
                    }));

                return coroutine;
            }

            return null;
        }


        public static IEnumerator PopAnimate(this Transform transform, AnimationCurve curve, Action callback)
        {
            if (transform == null || curve == null || curve.length == 0)
            {
                callback?.Invoke();
                yield break;
            }

            Keyframe lastKeyframe = curve.keys[curve.length - 1];
            float t = 0;

            while (t < lastKeyframe.time)
            {
                if (transform == null)
                {
                    callback?.Invoke();
                    yield break;
                }

                t += Time.deltaTime;
                if (t > lastKeyframe.time)
                    break;

                float scale = Mathf.Max(curve.Evaluate(t), 0);

                transform.SetUniformLocalScale(scale);
                yield return new WaitForEndOfFrame();
            }

            transform.SetUniformLocalScale(lastKeyframe.value);
            callback?.Invoke();
        }


        public static void SetUniformLocalScale(this Transform transform, float scale)
        { transform.localScale = new Vector3(scale, scale, scale); }
    }
}
