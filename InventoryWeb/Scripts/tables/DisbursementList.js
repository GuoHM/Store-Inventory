$(document).ready(function () {
    var oTableInit = new TableInit();
    oTableInit.Init();
})

var deptName = "";
var department = [];
var jsonlist = "";
var TableInit = function () {
    var oTableInit = new Object();

    oTableInit.Init = function () {
        $('#SearchItemTable').bootstrapTable({
            method: 'get',
            url: '/StoreClerk/GetDisbursementList',
            //toolbar: '#toolbar',    
            //data: '/StoreClerk/GetRetrievals',
            striped: true,
            cache: false,
            pagination: true,
            sortable: true,
            sortOrder: "asc",
            queryParams: oTableInit.queryParams,
            sidePagination: "client",
            pageNumber: 1,
            pageSize: 5,
            pageList: [10, 25, 50, 100],
            search: false,
            strictSearch: false,
            queryParamsType: "",
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: false,
            height: 400,
            // uniqueId: "ID", 
            showToggle: false,
            cardView: false,
            detailView: false,
            showExport: false,
            // exportDataType: "basic",              //basic', 'all', 'selected'.
            showColumns: false,
            columns: [{
                align: "center",
                title: 'Department Name',
                sortable: true,

                field: 'departmentName'
            },
            {
                align: "center",
                title: 'Representative',
                sortable: true,
                field: 'representative'
            },
            {
                align: "center",
                title: 'Collection Point',
                sortable: true,
                field: 'collectionPoint'
                },
                
                {
                    align: "center",
                    title: 'Action',
                    sortable: true,

                    //field : 'ID',
                    events: operateEvents,
                    formatter: selectItem
                }

            ],
            formatLoadingMessage: function () {
                return "loading...";
            }
        });

    };

    oTableInit.queryParams = function (params) {

        var id = {
            id: $("#RequestID").val()
        };
        return id;
    };


    function selectItem() {
        return [
            '<input type="button" id="view" value="View Details"  class="btn btn-primary" />',
        ].join('');
    }


    operateEvents = {
        'click #view': function (e, value, row, index) {
            $("#DisbursementModal").modal('show');
    
            department = [];
            deptName1 = row.departmentName;
            var jsonobj = { "deptName": deptName1}
            department.push(jsonobj);
            orderid = row.orderid;
          
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: '/StoreClerk/GetDisbursementItems',
                type: 'post',
                dataType: 'json',
                async: false,               
                data: JSON.stringify(department),

                success: function (data) {
                    jsonlist = data;
                }


            });
            var oTableInit = new TableInit1();
            oTableInit.Init();

            $('#requests').bootstrapTable('refreshOptions', { data: jsonlist });
            $('#requests').bootstrapTable('refresh', { data: jsonlist});


            //$('#requests').bootstrapTable('refreshOptions', { url: '/StoreClerk/GetDisbursementItems/' + deptName });
            //$('#requests').bootstrapTable('refresh', { url: '/StoreClerk/GetDisbursementItems/' + deptName });

        }
    };


    return oTableInit;
};

var TableInit1 = function () {
    var oTableInit = new Object();

    oTableInit.Init = function () {
        $('#requests').bootstrapTable({
            method: 'get',
            //url: '/StoreClerk/GetDisbursementItems',
            data: jsonlist,
            //toolbar: '#toolbar',                
            striped: true,
            cache: false,
            pagination: true,
            sortable: true,
            sortOrder: "asc",
            queryParams: oTableInit.queryParams,
            sidePagination: "client",
            pageNumber: 1,
            pageSize: 5,
            pageList: [10, 25, 50, 100],
            search: false,
            strictSearch: false,
            queryParamsType: "",
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: false,
            height: 250,
            // uniqueId: "ID", 
            //showToggle: true,
            //cardView: false,
            // detailView: false,
            // showExport: false,
            exportDataType: "basic",              //basic', 'all', 'selected'.
            showColumns: false,
            columns: [{
                align: "center",
                title: 'Item Name',
                sortable: true,

                field: 'itemDescription'
            },
            {
                align: "center",
                title: 'Quantity',
                sortable: true,
                field: 'quantity'
            },
            {
                align: "center",
                title: 'UOM',
                sortable: true,
                field: 'uom'
            }
            ],
            formatLoadingMessage: function () {
                return "loading...";
            }
        });

    };

    oTableInit.queryParams = function (params) {

        var id = {
            id: $("#RequestID").val()
        };
        return id;
    };

    selectItem = function (e, value, row, index) {

        return row.Price;

    }


    operateEvents = {
        'click #view': function (e, value, row, index) {
            $("#ApproveRequestModal").modal('show');
        }
    };


    return oTableInit;
};

function closeWindow() {
    var myWindow = document.getElementById("DisbursementModal");
    myWindow.close();
}