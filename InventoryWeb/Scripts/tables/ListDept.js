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
            url: 'http://inventorywebapi2019.azurewebsites.net/api/department',
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
                title: 'Department ID',
                sortable: true,
                sortable: true,
                field: 'DepartmentID'
            }, {
                    align: "center",
                    title: 'Department Name',
                    sortable: true,
                    sortable: true,
                    field: 'DepartmentName',
                    //events: operateEvents,
                    // formatter: InputTextBox
                }, {
                align: "center",
                title: 'Department Rep',
                sortable: true,
                sortable: true,
                    field: 'DepartmentRep',
                //events: operateEvents,
                // formatter: InputTextBox
            }, {
                align: "center",
                title: 'Collection Point',
                sortable: true,
                sortable: true,
                    field: 'CollectionPoint',
                //events: operateEvents,
                // formatter: InputTextBox
            },
            {
                align: "center",
                title: 'Department Head',
                sortable: true,
                sortable: true,
                field: 'DepartmentHead',
                //events: operateEvents,
                // formatter: InputTextBox
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
