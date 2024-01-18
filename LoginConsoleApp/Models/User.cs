using ProtoBuf;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace LoginConsoleApp.Models
{
    //[Serializable]
    [ProtoContract]
    public class User
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Password { get; set; }
        public override string ToString() => Name;
    }
}
