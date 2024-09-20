using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ExtensionMethods {

        public static float Remap(this float value, float fromin, float toin, float fromout = 0, float toout = 1) {
                float remapped;

                remapped = (value - fromin) / (toin - fromin) * (toout - fromout) + fromout;

                if (remapped < fromout)
                {
                        return fromout;
                }
                else if (remapped > toout)
                {
                        return toout;
                }
                else
                {
                        return remapped;
                }
        }

        public static float EaseInOut(this float value, float startvalue = 0, float endvalue = 1, float starttime = 0, float endtime = 1) {
                AnimationCurve curve = AnimationCurve.EaseInOut(starttime, startvalue, endtime, endvalue);

                return curve.Evaluate(value);
        }
}
