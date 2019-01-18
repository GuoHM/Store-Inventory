$(document).ready(function () {
    var oTableInit = new TableInit();
    oTableInit.Init();

    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});

var TableInit = function () {
    var oTableInit = new Object();

    oTableInit.Init = function () {
        $('#SearchItemTable').bootstrapTable({
            method: 'get',
            url: 'http://inventorywebapi2019.azurewebsites.net/api/Request',
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
            clickToSelect: false, 
            height: 500, 
            // uniqueId: "ID", 
            showToggle: true, 
            cardView: false, 
            detailView: false, 
            showExport: false,                     
            exportDataType: "basic",              //basic', 'all', 'selected'.
            showColumns: true,
            columns: [{
                align: "center",
                title: 'Request ID',
                sortable: true,
                sortable: true,
                field: 'RequestID'
            }, {
                align: "center",
                title: 'OrderID',
                sortable: true,
                sortable: true,
                field: 'OrderID',
                //events: operateEvents,
               // formatter: InputTextBox
                }, {
                    align: "center",
                    title: 'ItemID',
                    sortable: true,
                    sortable: true,
                    field: 'ItemID',
                    //events: operateEvents,
                    // formatter: InputTextBox
                },
                {
                    align: "center",
                    title: 'Needed',
                    sortable: true,
                    sortable: true,
                    field: 'Needed',
                    //events: operateEvents,
                    // formatter: InputTextBox
                },{
                align: "center",
                title: 'Select',
                sortable: true,
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
