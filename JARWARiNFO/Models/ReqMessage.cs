namespace JARWARiNFO.Models
{
    public class ReqMessage
    {
        public string Msg { get; set; }
        public bool State { get; set; }
        public IEnumerable<HWDataModel> HardwareInfo { get; set; }
    }
}
