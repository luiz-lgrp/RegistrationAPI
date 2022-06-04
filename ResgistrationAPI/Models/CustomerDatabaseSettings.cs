namespace ResgistrationAPI.Models
{
    //Configuração do meu Customer para o BD
    public class CustomerDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CustomerCollectionName { get; set; } = string.Empty;
    }
}
