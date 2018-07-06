using System.Web.Http;
using CSSTDSolution.Models;
using CSSTDModels;
using CSSTDEvaluation;

namespace CSSTDSolution.Controllers
{
    [RoutePrefix("evaluate")]
    public class EvaluationController : ApiController
    {
        private string baseFolder = System.Web.HttpContext.Current.Server.MapPath("~/SampleData/");

        [Route("publicupload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> PublicBlobUploadTest(string connectionString)
        {
            return new BlobEvaluationProcessor(baseFolder).PublicBlobUpload(new StorageContext(connectionString));
        }

        [Route("publicdownload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> PublicBlobDownloadTest(string connectionString)
        {
            return new BlobEvaluationProcessor(baseFolder).PublicBlobDownload(new StorageContext(connectionString));
        }

        [Route("privateupload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> PrivateBlobUploadTest(string connectionString)
        {
            return new BlobEvaluationProcessor(baseFolder).PrivateBlobUpload(new StorageContext(connectionString));
        }

        [Route("privatedownload")]
        [HttpGet]
        public EvaluationResult<BlobFileData> PrivateBlobDownloadTest(string connectionString)
        {
            return new BlobEvaluationProcessor(baseFolder).PrivateBlobDownload(new StorageContext(connectionString));

        }

        [Route("privatesas")]
        [HttpGet]
        public EvaluationResult<string> PrivateBlobSASTest(string connectionString)
        {
            return new BlobEvaluationProcessor(baseFolder).PrivateContainerSAS(new StorageContext(connectionString));
        }

        [Route("sqlupload")]
        [HttpGet]
        public EvaluationResult<CustomerData> SQLServerUploadTest(string connectionString)
        {
            return new RelationalEvaluationProcessor(baseFolder).SQLServerUpload(new SQLServerContext(connectionString));
        }

        [Route("sqldownload")]
        [HttpGet]
        public EvaluationResult<CustomerData> SQLServerDownloadTest(string connectionString)
        {
            return new RelationalEvaluationProcessor(baseFolder).SQLServerDownload(new SQLServerContext(connectionString));
        }

        [Route("mysqlupload")]
        [HttpGet]
        public EvaluationResult<VendorData> MySQLUploadTest(string connectionString)
        {
            return new RelationalEvaluationProcessor(baseFolder).MySQLUpload(new MySQLContext(connectionString));
        }

        [Route("mysqldownload")]
        [HttpGet]
        public EvaluationResult<VendorData> MySQLDownloadTest(string connectionString)
        {
            return new RelationalEvaluationProcessor(baseFolder).MySQLDownload(new MySQLContext(connectionString));
        }

        [Route("cosmosupload")]
        [HttpGet]
        public EvaluationResult<ProductDocument> CosmosDBUploadTest(string connectionString)
        {
            return new NoSQLEvaluationProcessor(baseFolder).CosmosDBUpload(new CosmosDBContext(connectionString));
        }

        [Route("cosmosdownload")]
        [HttpGet]
        public EvaluationResult<ProductDocument> CosmosDBDownloadTest(string connectionString)
        {
            return new NoSQLEvaluationProcessor(baseFolder).CosmosDBDownload(new CosmosDBContext(connectionString));
        }

        [Route("searchindex")]
        [HttpGet]
        public EvaluationResult<ProductDocument> SearchIndexTest(string connectionString)
        {
            return new NoSQLEvaluationProcessor(baseFolder).SearchIndex(new SearchContext(connectionString));
        }

        [Route("searchdownload")]
        [HttpGet]
        public EvaluationResult<ProductDocument> SearchDownloadTest(string connectionString)
        {
            return new NoSQLEvaluationProcessor(baseFolder).SearchDownload(new SearchContext(connectionString));
        }
    }
}
