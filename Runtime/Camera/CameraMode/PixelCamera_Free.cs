using UnityEngine;

namespace PixelGameLibrary.Runtime
{
    public class PixelCamera_Free : IPixelCameraController
    {
        public LayerMask RaycastMask { get; set; }
        public float MinPitch { get; set; }
        public float MaxPitch { get; set; }
        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public Vector3 TargetPosition { get; set; }
        public Vector3 GetLookXZ()
        {
            return new Vector3(Pitch, Yaw, 0);
        }

        public void Update(out Vector3 pos, out Vector3 rot)
        {
            pos = TargetPosition;
            rot = new Vector3(Pitch, Yaw, 0);
        }

        public void FixedUpdate(out Vector3 pos, out Vector3 rot)
        {
            pos = TargetPosition;
            rot = new Vector3(Pitch, Yaw, 0);
        }
    }
}