using System.Web.Http;
using CSSTDSolution.Models;
using CSSTDModels;
using CSSTDEvaluation;
using System.Configuration;

namespace CSSTDSolution.Controllers
{
    [RoutePrefix("evaluate")]
    public class EvaluationController : ApiController
    {
        private string baseFolder = System.Web.HttpContext.Current.Server.MapPath("~/SampleData/");

        [Route("blobupload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> BlobUploadTest(string storageAccount, string storageKey,string containerName,bool isPrivate, string encryptionKey)
        {
            storageAccount = storageAccount == "-1" ? ConfigurationManager.AppSettings["storageAccount"] : storageAccount;
            storageKey = storageKey == "-1" ? ConfigurationManager.AppSettings["storageKey"] : storageKey;
            return new BlobEvaluationProcessor(baseFolder, encryptionKey).BlobUpload(new StorageContext(storageAccount, storageKey),containerName,isPrivate);
        }

        [Route("blobdownload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> BlobDownloadTest(string storageAccount, string storageKey, string containerName, bool isPrivate, string encryptionKey)
        {
            storageAccount = storageAccount == "-1" ? ConfigurationManager.AppSettings["storageAccount"] : storageAccount;
            storageKey = storageKey == "-1" ? ConfigurationManager.AppSettings["storageKey"] : storageKey;
            return new BlobEvaluationProcessor(baseFolder, encryptionKey).BlobDownload(new StorageContext(storageAccount, storageKey),containerName,isPrivate);
        }


        [Route("blobsas")]
        [HttpGet]
        public EvaluationResult<string> BlobSASTest(string storageAccount, string storageKey, string containerName, string encryptionKey)
        {
            storageAccount = storageAccount == "-1" ? ConfigurationManager.AppSettings["storageAccount"] : storageAccount;
            storageKey = storageKey == "-1" ? ConfigurationManager.AppSettings["storageKey"] : storageKey;
            return new BlobEvaluationProcessor(baseFolder, encryptionKey).ContainerSAS(new StorageContext(storageAccount, storageKey),containerName);
        }

        [Route("sqlupload")]
        [HttpGet]
        public EvaluationResult<CustomerData> SQLServerUploadTest(string connectionString,string tableName, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["SQLConnection"] : connectionString;
            return new RelationalEvaluationProcessor(baseFolder, encryptionKey).SQLServerUpload(new SQLServerContext(connectionString), tableName);
        }

        [Route("sqldownload")]
        [HttpGet]
        public EvaluationResult<CustomerData> SQLServerDownloadTest(string connectionString, string tableName, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["SQLConnection"] : connectionString;
            return new RelationalEvaluationProcessor(baseFolder, encryptionKey).SQLServerDownload(new SQLServerContext(connectionString), tableName);
        }

        [Route("mysqlupload")]
        [HttpGet]
        public EvaluationResult<VendorData> MySQLUploadTest(string connectionString, string tableName, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["MySQLConnection"] : connectionString;
            return new RelationalEvaluationProcessor(baseFolder, encryptionKey).MySQLUpload(new MySQLContext(connectionString), tableName);
        }

        [Route("mysqldownload")]
        [HttpGet]
        public EvaluationResult<VendorData> MySQLDownloadTest(string connectionString,string tableName, string encryptionKey)
        {
            connectionString = connectionString == "-1" ? ConfigurationManager.AppSettings["MySQLConnection"] : connectionString;
            return new RelationalEvaluationProcessor(baseFolder, encryptionKey).MySQLDownload(new MySQLContext(connectionString), tableName);
        }

        [Route("cosmossqlupload")]
        [HttpGet]
        public EvaluationResult<ProductDocument> CosmosDBSQLUploadTest(string uri, string key,string collectionName, string encryptionKey)
        {
            uri = uri == "-1" ? ConfigurationManager.AppSettings["CosmosDBSQLUri"] : uri;
            key = key == "-1" ? ConfigurationManager.AppSettings["CosmosDBSQLKey"] : key;
            return new NoSQLEvaluationProcessor(baseFolder, encryptionKey).CosmosDBSQLUpload(new CosmosDBSQLContext(uri, key), collectionName);
        }

        [Route("cosmossqldownload")]
        [HttpGet]
        public EvaluationResult<ProductDocument> CosmosDBSQLDownloadTest(string uri, string key, string collectionName, string encryptionKey)
        {
            uri = uri == "-1" ? ConfigurationManager.AppSettings["CosmosDBSQLUri"] : uri;
            key = key == "-1" ? ConfigurationManager.AppSettings["CosmosDBSQLKey"] : key;
            return new NoSQLEvaluationProcessor(baseFolder, encryptionKey).CosmosDBSQLDownload(new CosmosDBSQLContext(uri, key), collectionName);
        }

        [Route("cosmostableupload")]
        [HttpGet]
        public EvaluationResult<IProductMention> CosmosDBTableUploadTest(string accountName, string key,string tableName, string encryptionKey)
        {
            accountName = accountName == "-1" ? ConfigurationManager.AppSettings["CosmosDBTableAccount"] : accountName;
            key = key == "-1" ? ConfigurationManager.AppSettings["CosmosDBTableKey"] : key;
            return new NoSQLEvaluationProcessor(baseFolder, encryptionKey).CosmosDBTableUpload(new CosmosDBTableContext(accountName, key), tableName);
        }

        [Route("cosmostabledownload")]
        [HttpGet]
        public EvaluationResult<IProductMention> CosmosDBTableDownloadTest(string accountName, string key, string tableName, string encryptionKey)
        {

            accountName = accountName == "-1" ? ConfigurationManager.AppSettings["CosmosDBTableAccount"] : accountName;
            key = key == "-1" ? ConfigurationManager.AppSettings["CosmosDBTableKey"] : key;
            return new NoSQLEvaluationProcessor(baseFolder, encryptionKey).CosmosDBTableDownload(new CosmosDBTableContext(accountName, key), tableName);

        }

    }
}
