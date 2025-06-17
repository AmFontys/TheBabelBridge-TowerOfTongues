using System.Security.Cryptography;

namespace BBTT.AuthCore;

public class EncryptionConfig
{
    public string Salt()
    {
        // Generate a random salt  
        byte[] salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        return Convert.ToBase64String(salt);
    }

    public string Hash (string saltedPassword)
    {
        HashAlgorithm hashAlgorithm = SHA256.Create();
        byte [] saltedPasswordBytes = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
        byte [] hashBytes = hashAlgorithm.ComputeHash(saltedPasswordBytes);
        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyHash (string password, string salt, string hash)
    {
        // Hash the password with the same salt
        string saltedPassword = password + salt;
        string hashedPassword = Hash(saltedPassword);
        return hashedPassword == hash;
    }

    public string Encrypt (string plainText, string key)
    {
        // Convert the plain text to bytes
        byte [] plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        // Create a new Aes object
        using (Aes aes = Aes.Create())
        {
            aes.Key = System.Text.Encoding.UTF8.GetBytes(key);
            aes.GenerateIV();
            // Create an encryptor
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            // Create a memory stream to hold the encrypted data
            using (MemoryStream ms = new MemoryStream())
            {
                // Write the IV to the stream
                ms.Write(aes.IV, 0, aes.IV.Length);
                // Create a crypto stream to encrypt the data
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public string Decrypt (string cipherText, string key)
    {
        // Convert the cipher text to bytes
        byte [] cipherBytes = Convert.FromBase64String(cipherText);
        // Create a new Aes object
        using (Aes aes = Aes.Create())
        {
            aes.Key = System.Text.Encoding.UTF8.GetBytes(key);
            // Extract the IV from the cipher text
            byte [] iv = new byte [ aes.BlockSize / 8 ];
            Array.Copy(cipherBytes, iv, iv.Length);
            aes.IV = iv;
            // Create a decryptor
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            // Create a memory stream to hold the decrypted data
            using (MemoryStream ms = new MemoryStream(cipherBytes, iv.Length, cipherBytes.Length - iv.Length))
            {
                // Create a crypto stream to decrypt the data
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
