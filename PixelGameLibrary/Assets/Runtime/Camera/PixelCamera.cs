using System.Collections.Generic;
using UnityEngine;
using Runtime.Setting;

namespace PixelGameLibrary.Runtime
{
    [RequireComponent(typeof(Camera))]
    public class PixelCamera : MonoBehaviour
    {
        private Dictionary<PixelCameraMode, IPixelCameraController> _modeControllers = new();

        public PixelCameraMode cameraMode;
        private IPixelCameraController Controller
        {
            get
            {
                if (!_modeControllers.ContainsKey(cameraMode))
                {
                    switch (cameraMode)
                    {
                        case PixelCameraMode.None:
                            _modeControllers.Add(cameraMode, new PixelCamera_None());
                            break;
                        case PixelCameraMode.Orbit:
                            _modeControllers.Add(cameraMode, new PixelCamera_Orbit());
                            break;
                        case PixelCameraMode.FreeCam:
                            _modeControllers.Add(cameraMode, new PixelCamera_Free());
                            break;
                    }
                }
                return _modeControllers[cameraMode];
            }
        }
        
        public float Pitch
        {
            get => Controller.Pitch;
            set => Controller.Pitch = value;
        }

        public float Yaw
        {
            get => Controller.Yaw;
            set => Controller.Yaw = value;
        }

        public Vector3 TargetPosition
        {
            get => Controller.TargetPosition;
            set => Controller.TargetPosition = value;
        }

        private Vector3 updatePosition;
        private Vector3 updateRotation;
        private Vector3 fixedUpdatePosition;
        private Vector3 fixedUpdateRotation;

        public Transform targetPosObj;

        private void Update()
        {
            Yaw += Time.deltaTime;
            Pitch = Mathf.Cos(Time.time) * Time.deltaTime;
            TargetPosition = targetPosObj.position;
            Controller.Update(out updatePosition, out updateRotation);
        }

        public Vector3 GetLookXZ()
        {
            return Controller.GetLookXZ();
        }

        private void FixedUpdate()
        {
            Controller.FixedUpdate(out fixedUpdatePosition, out fixedUpdateRotation);
            
            float lerpTime = GameSettings.Get<CameraSettings>().lerpTime;
            float rotLerpTime = GameSettings.Get<CameraSettings>().rotLerpTime;
            transform.position = Vector3.Lerp(transform.position, fixedUpdatePosition, lerpTime);
            transform.forward = Vector3.Slerp(transform.forward, fixedUpdateRotation, rotLerpTime);
        }
    }
}