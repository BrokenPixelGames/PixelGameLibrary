namespace PixelGameLibrary.Runtime
{
    public enum PixelCameraMode
    {
        /// <summary>
        /// Disables automatic camera movement and allows user to control position and orientation
        /// </summary>
        None,
        
        /// <summary>
        /// Orbits a an object or positon (vector3) while using pitch and yaw to determine spherical position
        /// </summary>
        Orbit,
        
        /// <summary>
        /// FPS-like control, useful for flying around an environment
        /// </summary>
        FreeCam
    }
}