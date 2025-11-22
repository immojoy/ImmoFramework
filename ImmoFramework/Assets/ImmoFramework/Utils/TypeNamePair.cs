
using System;
using System.Runtime.InteropServices;


namespace Immo.Framework.Utils
{
    [StructLayout(LayoutKind.Auto)]
    internal struct TypeNamePair : IEquatable<TypeNamePair>
    {
        private readonly Type m_Type;
        private readonly string m_Name;

        public TypeNamePair(Type type) : this(type, string.Empty)
        {
        }

        public TypeNamePair(Type type, string name)
        {
            if (type == null)
            {
                throw new Exception("Type cannot be null.");
            }

            m_Type = type;
            m_Name = name ?? string.Empty;
        }

        public Type Type => m_Type;

        public string Name => m_Name;

        public override string ToString()
        {
            return string.IsNullOrEmpty(m_Name) ? m_Type.FullName : $"{m_Type.FullName}.{m_Name}";
        }

        public override int GetHashCode()
        {
            return m_Type.GetHashCode() ^ m_Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is TypeNamePair other && Equals((TypeNamePair) other);
        }

        public bool Equals(TypeNamePair other)
        {
            return m_Type == other.m_Type && m_Name == other.m_Name;
        }

        public static bool operator ==(TypeNamePair a, TypeNamePair b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(TypeNamePair a, TypeNamePair b)
        {
            return !a.Equals(b);
        }
    }
}