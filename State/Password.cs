using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System;

namespace Imperit.State
{
    public readonly struct Password
    {
        private static SHA256 sha = SHA256Managed.Create();
        public readonly byte[] Hash;
        public Password(byte[] hash) => Hash = hash;
        public static Password FromString(string str) => new Password(sha.ComputeHash(Encoding.UTF8.GetBytes(str)));
        public static bool operator ==(Password x, Password y) => x.Hash.SequenceEqual(y.Hash);
        public static bool operator !=(Password x, Password y) => !(x == y);
        public override bool Equals(object? obj) => obj is Password pw && this == pw;
        public override int GetHashCode() => Hash.GetHashCode();
        public static Password Parse(string str) => new Password(Convert.FromBase64String(str));
        public override string ToString() => Convert.ToBase64String(Hash);
    }
}