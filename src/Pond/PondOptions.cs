namespace Pond
{
    public class PondOptions
    {
        public FilePoolConfigurations FilePools { get; }
        public PondOptions()
        {
            FilePools = new FilePoolConfigurations();
        }
    }
}
