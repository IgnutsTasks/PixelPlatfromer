using System;
using System.Collections;
using UnityEngine;

namespace Common
{
    public static class Utils
    {
        public static IEnumerator AttackDelay(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
    }
}