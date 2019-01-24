$(document).ready(function () {
    var oTableInit = new TableInit();
    oTableInit.Init();

    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});

var TableInit = function () {
    var oTableInit = new Object();

    oTableInit.Init = function () {
        $('#Viewallcatalogueitems').bootstrapTable({
            method: 'get',
            url: 'https://inventorywebapi2019.azurewebsites.net/api/Catalogue',
            //toolbar: '#toolbar',                
            striped: true,
            cache: false,
            pagination: true,
            sortable: true,
            sortOrder: "asc",
            queryParams: oTableInit.queryParams,
            sidePagination: "client",
            pageNumber: 1,
            pageSize: 7,
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
                title: 'ItemID',
                sortable: true,
                sortable: true,
                field: 'ItemID'
            }, {
                align: "center",
                title: 'Description',
                sortable: true,
                sortable: true,
                field: 'Description',
                //events: operateEvents,
                // formatter: InputTextBox
            }, {
                align: "center",
                title: 'Reorder Level',
                sortable: true,
                sortable: true,
                field: 'ReorderLevel',
                //events: operateEvents,
                // formatter: InputTextBox
            },
            {
                align: "center",
                title: 'Reorder Quantity',
                sortable: true,
                sortable: true,
                field: 'ReorderQuantity',
                //events: operateEvents,
                // formatter: InputTextBox
            }, {
                align: "center",
                title: 'Unit of Measure',
                sortable: true,
                sortable: true,
                field: 'MeasureUnit',
                //events: operateEvents,
                // formatter: InputTextBox
            }, {
                align: "center",
                title: 'Price',
                sortable: true,
                sortable: true,
                field: 'Price',
                //events: operateEvents,
                // formatter: InputTextBox
            }, {
                align: "center",
                title: 'Edit',
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


    function selectItem() {
        return [
            '<input type="button" id="view" value="Edit"  class="btn btn-primary" />',
        ].join('');
    }



    operateEvents = {
        'click #view': function (e, value, row, index) {
            $("#EditInventoryModal").modal('show');

            $("#idedit").val(row.ItemID);

            $("#descriptionedit").empty();
            $("#descriptionedit").append(row.Description);

            $("#editreorderlevel").val(row.ReorderLevel);

            $("#editreorderquantity").val(row.ReorderQuantity);


            $("#measureunit").empty();
            $("#measureunit").append(row.MeasureUnit);

            $("#editprice").val(row.Price);

            //var citem = {
            //    itemID: row.ItemID,
            //    description: row.Description,
            //    reorderlevel: val(row.ReorderLevel),
            //    reorderquantity: val(row.ReorderQuantity),
            //    measureunit: row.MeasureUnit,
            //    price: val(row.Price)
            //};

            //$.ajax({
            //    url: SERVER_NAME + "/StoreSupervisor/Save",
            //    type: "post",
            //    dataType: "text",
            //    async: false,
            //    data: JSON.stringify(citem),
            //    success: function (data) {
            //        data ? alert("It worked!") : alert("It didn't work.");
            //        //$('#successModal').modal('show');
            //    }
            //});
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