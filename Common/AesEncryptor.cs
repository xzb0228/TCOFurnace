using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    internal class AesEncryptor
    {
        private static readonly string salt = "FangYuanKeYi2025";
        private static readonly byte[] IV = new byte[] {
         0x01, 0x08, 0x53, 0x04, 0x05, 0x56, 0x77, 0x38,
         0x49, 0x0A, 0x0B, 0x02, 0x2D, 0x0E, 0x32, 0x11
        };

        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="Estr"></param>
        /// <returns></returns>
        public static string EncryptStr(string Estr)
        {
            byte[] key = Get16ByteKey(salt);
            return Encrypt(Estr, key, IV);
        }
        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="Dstr"></param>
        /// <returns></returns>
        public static string DecryptStr(string Dstr)
        {

            byte[] key = Get16ByteKey(salt);
            return Decrypt(Dstr, key, IV);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="key">密钥(16/24/32字节对应128/192/256位)</param>
        /// <param name="iv">初始化向量(16字节)</param>
        /// <returns>加密后的Base64字符串</returns>
        public static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            try {
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // 创建加密器
                    using (var encryptor = aes.CreateEncryptor())
                    {
                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                            {
                                byte[] data = Encoding.UTF8.GetBytes(plainText);
                                cs.Write(data, 0, data.Length);
                                cs.FlushFinalBlock();
                                return Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Log.Error($"加密字符串报错[{plainText}]" + ex.Message);
                return plainText;
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">加密后的Base64字符串</param>
        /// <param name="key">密钥(与加密时相同)</param>
        /// <param name="iv">初始化向量(与加密时相同)</param>
        /// <returns>解密后的明文</returns>
        public static string Decrypt(string cipherText, byte[] key, byte[] iv)
        {
            try {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // 创建解密器
                    using (var decryptor = aes.CreateDecryptor())
                    {
                        using (var ms = new MemoryStream(cipherBytes))
                        {
                            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                            {
                                using (var sr = new StreamReader(cs, Encoding.UTF8))
                                {
                                    return sr.ReadToEnd();
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                Log.Error($"解密字符串报错[{cipherText}]" + ex.Message);
                return cipherText;
            }
        }

        public static byte[] Get16ByteKey(string keyStr)
        {
            // 1. 将字符串转换为UTF8字节数组
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyStr);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(keyBytes);
                // 3. 截取前16字节作为AES-128密钥
                byte[] aesKey = new byte[16];
                Array.Copy(hashBytes, aesKey, 16);
                return aesKey;
            }

        }
    }
}
