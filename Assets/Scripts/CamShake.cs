using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VFX
{
    public class CamShake : MonoBehaviour
    {
        public static CamShake instance;


        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        private void OnShake(float duration, float strength)
        {
            transform.DOShakePosition(duration, strength);
            transform.DOShakeRotation(duration, strength);
        }

        public static void Shake(float duration, float strength)
        {
            instance.OnShake(duration, strength);
        }
    }
}

