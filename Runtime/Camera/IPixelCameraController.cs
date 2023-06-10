using UnityEngine;

namespace PixelGameLibrary.Runtime
{
    public interface IPixelCameraController
    {
        public LayerMask RaycastMask { get; set; }
        
        public float MinPitch { set; }
        public float MaxPitch { set; }
        public float Pitch { get; set; }
        
        public float Yaw { get; set; }
        public Vector3 TargetPosition { get; set; }
        
        public Vector3 GetLookXZ();

        public void Update(out Vector3 pos, out Vector3 rot);
        public void FixedUpdate(out Vector3 pos, out Vector3 rot);
    }
}