using System;

namespace Sm.Share.Entities.bases
{
    public class CreatorEntity : Entity
    {
        public DateTime CreateTime { get; set; }

        public string Creator { get; set; }
    }
}
