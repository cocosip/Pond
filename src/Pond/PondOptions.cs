using System.Collections.Generic;

namespace Pond
{
    public class PondOptions
    {
        public FilePoolConfigurations FilePools { get; }
        public PondOptions()
        {
            FilePools = new FilePoolConfigurations();
        }
        
        public PondOptions Configure(Dictionary<string, FilePoolConfiguration> configurations)
        {
            //TOTO
            return this;
        }
    }
}
