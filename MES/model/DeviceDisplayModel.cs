namespace MES.model
{
    internal class DeviceDisplayModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Duration { get; set; }
        public required bool Online { get; set; }
        public required DateTime OnlineTime { get; set; }
        public required DateTime FirstTime { get; set; }

    }
}
