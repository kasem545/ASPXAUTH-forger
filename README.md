---
# ASPXAUTH Ticket Forger

🛠️ A powerful tool for decrypting, modifying, and forging ASP.NET `.ASPXAUTH` cookies — useful for penetration testing, red teaming, and bug bounty research on ASP.NET applications using Forms Authentication.

---

## 🚀 What It Does

This tool allows you to:

- 🔓 **Decrypt** an existing ASPXAUTH cookie (captured from browser, Burp, etc.)
- ✏️ **Modify** the username and/or roles (`UserData`) inside the ticket
- 🔐 **Re-encrypt** the forged ticket using the same `machineKey`
- 🎯 **Replay** the forged ASPXAUTH cookie to impersonate or escalate privileges

---

## 📦 How It Works

ASP.NET Forms Authentication tickets (`FormsAuthenticationTicket`) are encrypted using a shared `<machineKey>` defined in `web.config`. This tool uses:

- `FormsAuthentication.Decrypt()` to parse a captured cookie
- Custom logic to change `Name` (username) and `UserData` (roles, claims, etc.)
- `FormsAuthentication.Encrypt()` to generate a valid forged cookie

---

## 🧠 When This Works

✅ This tool will work **if the target app**:

- Uses **Forms Authentication** (`<authentication mode="Forms">`)
- Stores **roles or privileges in the `UserData` field** of the ticket
- Relies on the `.ASPXAUTH` cookie for authentication/authorization
- Shares the known `machineKey` (extracted from `web.config` or guessed)

❌ This tool **will not work** if:

- The app uses **Windows Authentication**, **OpenID**, or **JWT**
- Roles are validated via **external databases** or **server-side session only**
- The `machineKey` is unknown or randomly generated

---

## ⚙️ Usage

1. **Capture a `.ASPXAUTH` cookie** using Burp, browser dev tools, etc.
2. **Paste it into `Program.cs`** in the `encryptedTicket` variable.
3. **Edit the `newUsername` and `newRoles`** to spoof or escalate.
4. **Compile and run the tool.**
5. **Copy the new forged cookie** into your browser or attack tool.

---

## 🔐 Example

```csharp
string newUsername = "user1";
string newRoles = "admin";
