using System;

namespace MobaServer
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class PacketAttribute : Attribute
    {
        public PacketAttribute(string id)
        {
            Id = id;
        }
	
        public string Id { get; }
    }
}