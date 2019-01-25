$(document).ready(function () {
    var oTableInit = new TableInit();
    oTableInit.Init();

    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});
var orderid = "";
var requestedDate = "";
var reqesterName = "";
var TableInit = function () {
    var oTableInit = new Object();

    oTableInit.Init = function () {
        $('#SearchItemTable').bootstrapTable({
            method: 'get',
            url: '',
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
            search: true,
            strictSearch: false,
            queryParamsType: "",
            showRefresh: true,
            minimumCountColumns: 2,
            clickToSelect: true,
            height: 350,
            // uniqueId: "ID", 
            showToggle: true,
            cardView: false,
            detailView: false,
            showExport: false,
            exportDataType: "basic",              //basic', 'all', 'selected'.
            showColumns: true,
            columns: [{
                align: "center",
                // title: 'Order ID',
                checkbox: "true",
                // sortable: true,
                //  field: 'OrderID',
                //formatter: checkboxItem

            }, {
                align: "center",
                title: 'Order ID',
                sortable: true,

                field: 'OrderID'
            }
                , {
                align: "center",
                title: 'Department Name',
                sortable: true,
                field: 'Department.DepartmentName'
                //events: operateEvents,
                // formatter: InputTextBox
            }, {
                align: "center",
                title: 'Order Date',
                sortable: true,
                field: 'OrderDate'
                //events: operateEvents,
                // formatter: InputTextBox
            }, {
                align: "center",
                title: 'Status',
                sortable: true,
                field: 'OrderStatus'
                //events: operateEvents,
                // formatter: InputTextBox
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


    // params
    oTableInit.queryParams = function (params) {

        var temp = {
            courseid: $("#courseid").val()
        };
        return temp;
    };
    function checkboxItem() {
        return [
            '<input type="checkbox" id="selectedOrders" />',
        ].join('');
    }

    function selectItem() {
        return [
            '<input type="button" id="view" value="View Details"  class="btn btn-primary" />',
        ].join('');
    }

    function openPopup() {
        $("#ApproveRequestModal").modal('show');
    }
    operateEvents = {
        'click #view': function (e, value, row, index) {

            $("#ApproveRequestModal").modal('show');
            orderid = row.OrderID;
            requestedDate = row.RequestDate;
            reqesterName = row.Department.DepartmentName
            document.getElementById('requestDate').innerHTML = requestedDate;
            document.getElementById('requestedBy').innerHTML = reqesterName;


            var oTableInit = new TableInit1();
            oTableInit.Init();

            $('#requests').bootstrapTable('refreshOptions', { url: 'http://inventorywebapi2019.azurewebsites.net//api/Request/' + orderid });
            $('#requests').bootstrapTable('refresh', { url: 'http://inventorywebapi2019.azurewebsites.net//api/Request/' + orderid });
        }
    };

    return oTableInit;
};

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        // button


    };

    return oInit;
};


