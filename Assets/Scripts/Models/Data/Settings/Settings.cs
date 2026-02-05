namespace Models.Data.Settings
{
    public interface ISettings
    {
        CameraSettings CameraSettings { get; set; }
    }
    
    public class Settings
    {
        public CameraSettings CameraSettings { get; set; }

        public Settings(ISettings settings)
        {
            CameraSettings = settings.CameraSettings;
        }
    }
}
