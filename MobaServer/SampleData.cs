namespace MobaServer
{
    public static class SampleData
    {
        public static readonly Player Player = new Player()
        {
            Id = 1,
            Name = "Test",
            Avatar = "I11000301",
            FrameId = "1",
            Vip = 1,
            Level = 1,
            GradeName = "",
            GuildName = "Staff",
            GuildPosition = 1,
            Status = 1
        };
        
        public static readonly Role Role = new Role()
        {
            Id = "1",
            Name = "Test",
            Icon = "1",
            Desc = "Testing",
            Portrait = "1"
        };
    }
}