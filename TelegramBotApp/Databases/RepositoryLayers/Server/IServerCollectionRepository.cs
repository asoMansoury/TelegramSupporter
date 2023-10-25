using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeelgramBotSupporter.Databases.Models;

namespace TeelgramBotSupporter.Databases.RepositoryLayers
{
    public interface IServerCollectionRepository
    {
        Task<List<ServerEntity>> GetAllDocuments();
        Task<List<ServerEntity>> GetFilteredTypeDocument( string type);
        Task<ServerEntity> GetFilteredTypeDocument(string type,string code);


    }
}
