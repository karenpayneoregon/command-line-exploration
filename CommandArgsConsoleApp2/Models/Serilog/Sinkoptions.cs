namespace CommandArgsConsoleApp2.Models.Serilog;

public class Sinkoptions
{
    public int batchPostingLimit { get; set; }
    public string batchPeriod { get; set; }
    public bool eagerlyEmitFirstEvent { get; set; }
}