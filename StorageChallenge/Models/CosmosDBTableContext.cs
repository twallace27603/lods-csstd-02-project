using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CSSTDModels;

namespace CSSTDSolution.Models
{
    public class CosmosDBTableContext : ICosmosDBTableContext
    {
        public string ConnectionString { get; set; }

        public Task CreateTable()
        {
            throw new NotImplementedException();
        }

        public List<IProductMention> GetMentions()
        {
            throw new NotImplementedException();
        }

        public List<IProductMention> GetMentions(string product, string platform)
        {
            throw new NotImplementedException();
        }

        public Task LoadMentions(List<IProductMention> mentions)
        {
            throw new NotImplementedException();
        }
    }
}