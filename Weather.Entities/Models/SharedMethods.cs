namespace Weather.Entities.Models;
public static class SharedMethods
{
    /// <summary>
    /// Base64s the encode.
    /// </summary>
    /// <param name="plainText">The plain text.</param>
    /// <returns></returns>
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    /// <summary>
    /// Base64s the decode.
    /// </summary>
    /// <param name="base64EncodedData">The base64 encoded data.</param>
    /// <returns></returns>
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
