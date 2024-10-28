using System.Security.Cryptography;
using System.Text;

namespace XiHan.Utils.Encryptions
{
    /// <summary>
    /// Ecdsa 加密解密
    /// </summary>
    public static class EcdsaEncryptionHelper
    {
        public static string Encrypt(string input, string publicKey, string privateKey)
        {
            using ECDsa ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            ecdsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
            byte[] sign = ecdsa.SignData(Encoding.UTF8.GetBytes(input), HashAlgorithmName.SHA256);
            return Convert.ToHexString(sign);
        }
    }
}