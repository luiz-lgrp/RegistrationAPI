using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ResgistrationAPI.Models
{
    //Classe clientes
    public class Customer
    {
        //Estou indicando que essa propriedade é uma chave primaria que vai ser gerada automaticamente
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        //Nome que vai estar na coluna
        [BsonElement("Nome")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("CPF")]
        public string Cpf { get; set; } = string.Empty;

        [BsonElement("Endereço")]
        public string Adress { get; set; } = string.Empty;

        [BsonElement("Telefone")]
        public string Phone { get; set; } = string.Empty;

        [BsonElement("Ativo")]
        public bool Active { get; set; } = true;
    }
}
