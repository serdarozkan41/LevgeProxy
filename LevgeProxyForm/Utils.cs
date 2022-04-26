namespace LevgeProxyForm
{
    public class Utils
    {
        static public string ToReadableByteArray(byte[] bytes)
        {
            return string.Join(", ", bytes);
        }
    }
}
