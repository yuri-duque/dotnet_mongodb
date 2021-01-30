using Domain.Enums;
using Infra.Schemas;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infra.Mappings.Schemas
{
    public class RestauranteMapping
    {
        public RestauranteMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(RestauranteSchema)))
            {
                BsonClassMap.RegisterClassMap<RestauranteSchema>(i =>
                {
                    i.AutoMap();
                    i.MapIdMember(c => c.Id);
                    i.MapMember(c => c.Cozinha).SetSerializer(new EnumSerializer<ECozinha>(BsonType.Int32)); // serializando Enum
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
