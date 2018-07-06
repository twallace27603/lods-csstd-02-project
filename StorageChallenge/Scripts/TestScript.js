$(document).ready(function () {
    var testType = new Number($('#testType').val());

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
        var storageConnection = $('#storageConnection').val();
        if ((testType & 1) == 1) {
            runTest("publicupload", storageConnection, "Public Blob Upload", null, null, processStorageResults, false);
            runTest("publicdownload", storageConnection, "Public Blob Download", null, null, processStorageResults, true);

        }
        if ((testType & 2) == 2) {
            runTest("privateupload", storageConnection, "Private Blob Upload", null, null, processStorageResults, false);
            runTest("privatedownload", storageConnection, "Private Blob Download", null, null, processStorageResults, true);
            runTest("privatesas", storageConnection, "Private Blob Download", null, null, processStorageResults, false);

        }
    }
    function sqlTest() {
        clearResults();
        var sqlConnection = $('#sqlConnection').val();
        var mysqlConnection = $('#mysqlConnection').val();
        if ((testType & 4) == 4) {
            runTest("sqlupload", storageConnection, "SQL Server Upload", null, null, processDataResults, false);
            runTest("sqldownload", storageConnection, "SQL Server Download", ["ID", "Name", "PostalCode"], ["ID", "Customer Name", "Postal Code"], processDataResults, true);

        }
        if ((testType & 8) == 8) {
            runTest("mysqlupload", storageConnection, "MySQL Upload", null, null, processDataResults, false);
            runTest("mysqldownload", storageConnection, "MySQL Download", ["ID", "Name", "Industry"], ["ID", "Vendor Name", "Industry"], processDataResults, true);
        }
    }
    function nosqlTest() {
        clearResults();
        var cosmosdbConnection = $('#cosmosdbConnection').val();
        var searchConnection = $('#searchConnection').val();
        if ((testType & 16) == 16) {
            runTest("sqlupload", storageConnection, "SQL Server Upload", null, null, processDataResults, false);
            runTest("sqldownload", storageConnection, "SQL Server Download", ["ID", "Name", "PostalCode"], ["ID", "Customer Name", "Postal Code"], processDataResults, true);

        }
        if ((testType & 32) == 32) {
            runTest("mysqlupload", storageConnection, "MySQL Upload", null, null, processDataResults, false);
            runTest("mysqldownload", storageConnection, "MySQL Download", ["ID", "Name", "Industry"], ["ID", "Vendor Name", "Industry"], processDataResults, true);
        }
    }

    function processStorageResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var status = resultsData.Code == 0 ? "Success" : "Failed";
        var result = "<tr><td>" + status + "</td><td><h3>" + title + "</h3><p>" + resultsData.Text + "</p></td></tr>";
        addResult(result);
    }
    function processDataResults(resultsData, title, fieldNames, fieldTitles, showData) {
        var status = resultsData.Code == 0 ? "Success" : "Failed";
        var result = "<tr><td>" + status + "</td><td><h3>" + title + "</h3><p>" + resultsData.Text + "</p></td></tr>";
        addResult(result);

    }
    function processError(errorInfo, title) {
        var result = "<tr><td>Error</td><td><h3>" + title + "</h3><p>" + errorInfo + "</p></td></tr>";
        addResult(result);
    }
    function runTest(operation, connection, title, fieldNames, fieldTitles, callBack, showData) {
        var url = "/evaluate/" + operation + "?connectionString=" + connection;
        $.get(url).done(function (data) {
            callBack(data, title, fieldNames, fieldTitles, callBack);
        }).fail(function (error) {
            processError(error, title);
        });
    }
    function clearResults() {
        $('#results').text("");
    }
    function addResult(result) {
        $('#results').append(result);
    }

});