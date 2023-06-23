public static class ApiUrlHelper
{
    public static string GetBaseApiUrl()
    {
        return DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:7040" : "http://localhost:7040";        
    }
}
