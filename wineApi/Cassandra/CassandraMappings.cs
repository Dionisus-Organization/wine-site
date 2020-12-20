using System;

using Cassandra.Mapping;

namespace wineApi.Cassandra
{
    public class AllMappings : Mappings
    {
        public AllMappings()
        {
            For<UserModel>()
                .KeyspaceName( Environment.GetEnvironmentVariable( "KEYSPACE_NAME" ) )
                .TableName("user")
                .PartitionKey(u => u.Id)
                .Column(u => u.Id, cm => cm.WithName("id"))
                .Column(u => u.Name, cm => cm.WithName("name"))
                .Column(u => u.LastName, cm => cm.WithName("last_name"));

            For<WineModel>()
                .KeyspaceName( Environment.GetEnvironmentVariable( "KEYSPACE_NAME" ) )
                .TableName("wine")
                .PartitionKey(u => u.Id)
                .Column(u => u.Id, cm => cm.WithName("id"))
                .Column(u => u.Wine, cm => cm.WithName("wine"))
                .Column(u => u.WineSlug, cm => cm.WithName("wine_slug"))
                .Column(u => u.Appellation, cm => cm.WithName("appellation"))
                .Column(u => u.AppellationSlug, cm => cm.WithName("appellation_slug"))
                .Column(u => u.Color, cm => cm.WithName("color"))
                .Column(u => u.WineType, cm => cm.WithName("wine_type"))
                .Column(u => u.Region, cm => cm.WithName("region"))
                .Column(u => u.Country, cm => cm.WithName("country"))
                .Column(u => u.Classification, cm => cm.WithName("classification"))
                .Column(u => u.Vintage, cm => cm.WithName("vintage"))
                .Column(u => u.Date, cm => cm.WithName("date"))
                .Column(u => u.IsPrimeurs, cm => cm.WithName("is_primeurs"))
                .Column(u => u.Score, cm => cm.WithName("score"))
                .Column(u => u.ConfidenceIndex, cm => cm.WithName("confidence_index"))
                .Column(u => u.JournalistCount, cm => cm.WithName("journalist_count"))
                .Column(u => u.Lwin, cm => cm.WithName("lwin"))
                .Column(u => u.Lwin11, cm => cm.WithName("Llin11"));

            For<RatingModel>()
                .KeyspaceName( Environment.GetEnvironmentVariable( "KEYSPACE_NAME" ) )
                .TableName( "wine" )
                .PartitionKey( u => u.UserId )
                .Column( u => u.UserId, cm => cm.WithName( "userid" ) )
                .Column( u => u.WineId, cm => cm.WithName( "wineid" ) )
                .Column( u => u.Rating, cm => cm.WithName( "rating" ) );
        }
    }
}
