using Runtime.Setting;
using UnityEngine;

namespace PixelGameLibrary.Runtime
{
    public class CameraSettings : GameSettingBase
    {
        [Header("General")]
        public float distanceCorrection = 0.05f;

        [Header("Orbit Camera")]
        public Vector3 offset;
        public Vector3 lookOffset;
        public float distance = 5.59f;
        public float lerpTime = 0.1f;
        public float rotLerpTime = 0.1f;
        public LayerMask rayIgnoreLayers;
    }
}