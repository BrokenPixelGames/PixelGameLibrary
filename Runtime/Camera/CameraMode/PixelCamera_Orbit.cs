using UnityEngine;
using Runtime.Setting;

namespace PixelGameLibrary.Runtime
{
    public class PixelCamera_Orbit : IPixelCameraController
    {
        public LayerMask RaycastMask { get; set; }

        public float MinPitch { get; set; } = 0.4f;
        public float MaxPitch { get; set; } = 1.8f;

        private float _pitch;
        public float Pitch
        {
            get => _pitch;
            set => _pitch = Mathf.Clamp(value, MinPitch, MaxPitch);
        }

        private float _yaw;
        public float Yaw
        {
            get => _yaw;
            set => _yaw = value % (Mathf.PI * 2);
        }

        private Vector3 _targetPosition;
        public Vector3 TargetPosition
        {
            get => _targetPosition;
            set => _targetPosition = value;
        }

        private Vector3 _p, _lastP, _d, _lastD, _od;

        public Vector3 GetLookXZ()
        {
            return new Vector3(_od.x, 0, _od.z);
        }
        
        public void Update(out Vector3 pos, out Vector3 rot)
        {
            _od = new Vector3
            {
                x = Mathf.Sin(Pitch) * Mathf.Cos(Yaw),
                y = Mathf.Cos(Pitch),
                z = Mathf.Sin(Pitch) * Mathf.Sin(Yaw)
            };

            Vector3 offset = GameSettings.Get<CameraSettings>().offset;
            float distance = GameSettings.Get<CameraSettings>().distance;
            _p.x = TargetPosition.x + offset.x + (distance * _od.x);
            _p.y = TargetPosition.y + offset.y + (distance * _od.y);
            _p.z = TargetPosition.z + offset.z + (distance * _od.z);

            Vector3 lookOffset = GameSettings.Get<CameraSettings>().lookOffset;
            _d = (TargetPosition + lookOffset) - _p;
            
            pos = _p;
            rot = _d;
        }

        public void FixedUpdate(out Vector3 pos, out Vector3 rot)
        {
            RaycastHit hit = default(RaycastHit);

            Ray r = new Ray(_p, _p - _od);
            LayerMask rayIgnoreLayers = GameSettings.Get<CameraSettings>().rayIgnoreLayers;
            if (Physics.Raycast(r, out hit, _d.magnitude, ~rayIgnoreLayers))
            {
                float distCorrection = GameSettings.Get<CameraSettings>().distanceCorrection;
                if (Physics.CheckSphere(r.GetPoint(hit.distance), distCorrection, ~rayIgnoreLayers))
                {
                    distCorrection *= distCorrection;
                }

                _p = r.GetPoint(hit.distance - distCorrection);
            }

            pos = _p;
            rot = _d;
            _lastP = _p;
            _lastD = _d;
        }
    }
}