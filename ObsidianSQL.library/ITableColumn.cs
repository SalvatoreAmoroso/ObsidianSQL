namespace ObsidianSQL.library
{
    public interface ITableColumn
    {
        public string Name { get; set; }
        public string Datatype { get; set; } //TODO: sollte in ein enum statt string geändert werden
    }
}