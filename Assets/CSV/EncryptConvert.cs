using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// 工具类：文件与二进制流间的转换
/// 修改密码在base64时候，由于长度或字符问题，造成的解码失败。
/// </summary>
public class FileEncryptConvertHelper
{  
    #region Encrypt
    public static void EncryptFile(string inputFile, string outputFile)
    {
        try
        {   
            string password = @"cartzhang01"; // Your Key Here
            #region use this may be error for encrypt.
            //UnicodeEncoding UE = new UnicodeEncoding();
            //byte[] key = UE.GetBytes(password);
            #endregion
            
            using (Rijndael myRijndael = Rijndael.Create())
            {
                //byte[] key = EncryptStringToBytes(password, myRijndael.Key, myRijndael.IV);
                SymmetricAlgorithm algorithm = getAlgorithm(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    //RMCrypto.CreateEncryptor(key, key),
                    algorithm.CreateEncryptor(),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
        }
        catch
        {
            Debug.Log("Encryption failed!" + "Error");
        }
    }
    ///<summary>
    /// Steve Lydford - 12/05/2008.
    ///
    /// Decrypts a file using Rijndael algorithm.
    ///</summary>
    ///<param name="inputFile"></param>
    ///<param name="outputFile"></param>
    public static void DecryptFile(string inputFile, string outputFile)
    {
        if (!File.Exists(inputFile))
        {
            return;
        }
        {
            string password = @"cartzhang01"; // Your Key Here
            #region use this may be error for encrypt.
            //UnicodeEncoding UE = new UnicodeEncoding();
            //byte[] key = UE.GetBytes(password);
            #endregion

            using (Rijndael myRijndael = Rijndael.Create())
            {
                byte[] keyTmp = EncryptStringToBytes(password, myRijndael.Key, myRijndael.IV);
                string tpkey = DecryptStringFromBytes(keyTmp, myRijndael.Key, myRijndael.IV);
                byte[] key = Encoding.ASCII.GetBytes(password);
                key = keyTmp;

                SymmetricAlgorithm algorithm = getAlgorithm(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(fsCrypt,
                //RMCrypto.CreateDecryptor(key, key),
                algorithm.CreateDecryptor(),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();
            }

        }
    }
    #endregion

    #region 0
    static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;
        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        // Return the encrypted bytes from the memory stream.
        return encrypted;

    }

    static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

        }

        return plaintext;

    }
    #endregion

    // create and initialize a crypto algorithm
    public static SymmetricAlgorithm getAlgorithm(string password)
    {
        SymmetricAlgorithm algorithm = Rijndael.Create();
        Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(
            password, new byte[] {
            0x53,0x6f,0x64,0x69,0x75,0x6d,0x20,             // salty goodness
            0x43,0x68,0x6c,0x6f,0x72,0x69,0x64,0x65
        }
        );
        algorithm.Padding = PaddingMode.ISO10126;
        algorithm.Key = rdb.GetBytes(32);
        algorithm.IV = rdb.GetBytes(16);
        return algorithm;
    }
}
