using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerDASample.Database.Configuration
{
    public class DatabaseOptions
    {
        public string SelectedOptions { get; set; }
        public List<CredentialContainer> CredentialSet { get; set; }
    }
    public class CredentialContainer
    {
        public string Key { get; set; }
        public string Credential { get; set; }
    }
}
