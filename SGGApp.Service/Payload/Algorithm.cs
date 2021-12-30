using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SGGApp.Utilities;

namespace SGGApp.Service.Payload
{
    public static class Algorithm
    {
        public static string Encrypt(string text)
        {
            try
            {
                string keyText = PublicVariables.encryption_key;
                string iv = PublicVariables.initialization_vector;
                byte[] keyBytes = Convert.FromBase64String(keyText);
                byte[] ivBytes = Encoding.ASCII.GetBytes(iv.PadLeft(16));

                using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
                {
                    aesCryptoServiceProvider.Key = keyBytes;
                    aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
                    aesCryptoServiceProvider.IV = ivBytes;
                    aesCryptoServiceProvider.Mode = CipherMode.CBC;
                    ICryptoTransform encryptor = aesCryptoServiceProvider.CreateEncryptor();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(text);
                            }

                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public static string Decrypt(string text)
        {
            try
            {
                string result = "";
                string keyText = PublicVariables.encryption_key;
                string iv = PublicVariables.initialization_vector;
                byte[] keyBytes = Convert.FromBase64String(keyText);
                byte[] ivBytes = Encoding.ASCII.GetBytes(iv.PadLeft(16));

                using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
                {
                    aesCryptoServiceProvider.Key = keyBytes;
                    aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
                    aesCryptoServiceProvider.IV = ivBytes;
                    aesCryptoServiceProvider.Mode = CipherMode.CBC;
                    aesCryptoServiceProvider.FeedbackSize = 128;

                    ICryptoTransform decryptor = aesCryptoServiceProvider.CreateDecryptor(aesCryptoServiceProvider.Key, aesCryptoServiceProvider.IV);

                    byte[] buffer = Convert.FromBase64String(text);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                result = streamReader.ReadToEnd();
                            }
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
