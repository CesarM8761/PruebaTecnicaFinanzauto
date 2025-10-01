using System;

namespace BlogTecnicaAPI.Utils
{
    public static class ImagenUtils
    {
        public static byte[] Base64ToBytes(string base64)
        {
            return Convert.FromBase64String(base64);
        }

        public static string BytesToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
