$(document).ready(function () {
    var testType = new Number($('#testType').val());
    var key = "thisaintakey";

    if ((testType & 3) > 0) {
        $('#storageTest').click(storageTest);
    }
    if ((testType & 12) > 0) {
        $('#sqlTest').click(sqlTest);
    }
    if ((testType & 48) > 0) {
        $('#nosqlTest').click(nosqlTest);
    }
    function storageTest() {
        clearResults();
        var storageConnection = "storageAccount=" + $('#storageAccount').val() + "&storageKey=" + encodeURIComponent($('#storageKey').val());
        if ((testType & 1) === 1) {
            runTest("publicupload", storageConnection, "Public Blob Upload", null, null, processStoragePublicUploadResults, false);
        }
        if ((testType & 2) === 2) {
            runTest("privateupload", storageConnection, "Private Blob Upload", null, null, processStoragePrivateUploadResults, false);
        }
    }
    function processStoragePublicUploadResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var storageConnection = "storageAccount=" + $('#storageAccount').val() + "&storageKey=" + encodeURIComponent($('#storageKey').val());
        processStorageResults(resultsData, title, fieldNames, fieldTitles, showData);
        runTest("publicdownload", storageConnection, "Public Blob Download", null, null, processStorageResults, true);
    }
    function processStoragePrivateUploadResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var storageConnection = "storageAccount=" + $('#storageAccount').val() + "&storageKey=" + encodeURIComponent($('#storageKey').val());
        processStorageResults(resultsData, title, fieldNames, fieldTitles, showData);
        runTest("privatedownload", storageConnection, "Private Blob Download", null, null, processStorageResults, true);
        runTest("privatesas", storageConnection, "Private SAS Generation", null, null, processStorageResults, false);
    }

    function sqlTest() {
        clearResults();
        var sqlConnection = "connectionString=" + encodeURIComponent($('#sqlConnection').val());
        var mysqlConnection = "connectionString=" + encodeURIComponent($('#mysqlConnection').val());
           //Note: There was a race condition with Entity framework when running SQL and MySQL.  If both run (advanced or expert), they will run in series with MySQL running after SQL Server
        if ((testType & 4) === 4) {
            runTest("sqlupload", sqlConnection, "SQL Server Upload", null, null, processSQLUploadDataResults, false);
        }
        if ((testType & 8) === 8) {
             runTest("mysqlupload", mysqlConnection, "MySQL Upload", null, null, processMySQLUploadDataResults, false);
        }
    }
    function processSQLUploadDataResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var sqlConnection = "connectionString=" + encodeURIComponent($('#sqlConnection').val());
        processDataResults(resultsData, title, fieldNames, fieldTitles, showData);
        runTest("sqldownload", sqlConnection, "SQL Server Download", ["ID", "Name", "PostalCode"], ["ID", "Customer Name", "Postal Code"], processDataResults, true);
    }
 
    function processMySQLUploadDataResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var mysqlConnection = "connectionString=" + encodeURIComponent($('#mysqlConnection').val());
        processDataResults(resultsData, title, fieldNames, fieldTitles, showData);
        runTest("mysqldownload", mysqlConnection, "MySQL Download", ["ID", "Name", "Industry"], ["ID", "Vendor Name", "Industry"], processDataResults, true);

    }

    function nosqlTest() {
        clearResults();
        var cosmosdbSQLConnection = "connectionString=" + encodeURIComponent($('#cosmosdbSQLConnection').val());
        var cosmosdbTableConnection = "connectionString=" + encodeURIComponent($('#cosmosdbTableConnection').val());

        if ((testType & 16) === 16) {
            runTest("cosmossqlupload", cosmosdbSQLConnection, "Cosmos DB SQL Upload", null, null, processCosmosSQLUploadDataResults, false);

        }
        if ((testType & 32) === 32) {
            runTest("cosmostableupload", cosmosdbTableConnection, "Cosmos DB SQL Upload", null, null, processCosmosTableUploadDataResults, false);
        }
    }
    function processCosmosSQLUploadDataResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var cosmosdbConnection = "connectionString=" + encodeURIComponent($('#cosmosdbSQLConnection').val());
        processDataResults(resultsData, title, fieldNames, fieldTitles, showData);
        runTest("cosmossqldownload", cosmosdbConnection, "Cosmos DB SQL Download", ["Industry", "Name", "Tier"], ["Industry", "Name", "Tier"], processDataResults, true);

    }

    function processCosmosTableUploadDataResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var cosmosdbConnection = "connectionString=" + encodeURIComponent($('#cosmosdbTableConnection').val());
        processDataResults(resultsData, title, fieldNames, fieldTitles, showData);
        runTest("cosmostabledownload", cosmosdbConnection, "Cosmos DB Table Download", ["Industry", "Name", "Tier"], ["Industry", "Name", "Tier"], processDataResults, true);

    }

    function processStorageResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var status = resultsData.Code === 0 ? "Success" : "Failed";
        var result = "<tr><td>" + status + "</td><td><h3>" + title + "</h3><p>" + resultsData.Text + "</p></td></tr>";
        $('#results').append(result);
        if (showData) {
            var details = "<h2>" + title + "</h2><h3>Links</h3><ul class='list-inline'>";
            $(resultsData.Results).each(function (index, blobData) {
                details += "<li><img src='" + blobData.URL + blobData.SAS + "' heigh='200px' width='200px' /></li>";
            });
            details += "</ul>";
            $('#details').append(details);
        }
    }
    function processDataResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var status = resultsData.Code === 0 ? "Success" : "Failed";
        var result = "<tr><td>" + status + "</td><td><h3>" + title + "</h3><p>" + resultsData.Text + "</p></td></tr>";
        $('#results').append(result);
        if (showData) {
            //Add content
            var details = "<h3>" + title + "</h3><table><tr>";
            $(fieldTitles).each(function (index, ft) {
                details += "<th>" + ft + "</th>";
            });
            details += "</tr>";
            $(resultsData.Results).each(function (index, result) {
                details += "<tr>";
                $(fieldNames).each(function (index, fn) {
                    details += "<td>" + result[fn] + "</td>";
                });
                details += "</tr>";
            });
            details += "</table>";
            $('#details').append(details);
        }

    }
    function processError(errorInfo, title) {
        var result = "<tr><td>Error</td><td><h3>" + title + "</h3><p>" + errorInfo + "</p></td></tr>";
        addResult(result);
    }
    function runTest(operation, queryString, title, fieldNames, fieldTitles, callBack, showData) {
        var url = "/evaluate/" + operation + "?" + queryString + "&encryptionKey=" + encodeURIComponent(key);
        $.get(url).done(function (data) {
            callBack(data, title, fieldNames, fieldTitles, showData);
        }).fail(function (error) {
            processError(error, title);
        });
    }
    function clearResults() {
        $('#results').text("");
        $('#details').text('');
    }


});