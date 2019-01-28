$(document).ready(function () {
    var oTableInit = new TableInit();
    oTableInit.Init();

    var oButtonInit = new ButtonInit();
    oButtonInit.Init();
    var oButtonInit2 = new TableInit2();
    oButtonInit2.Init(); 

});


var orderid = "";
//var requestedDate = "";
var reqesterName = "";
var jsonlist1 = [];
var orderlist = [];
var requestId = "";
var binNumber = "";
var needed = "";
var actual = "";
var itemDescription = "";
var jsonObj = null;

var TableInit = function () {
    var oTableInit = new Object();

    oTableInit.Init = function () {
        $('#SearchItemTable').bootstrapTable({
            method: 'get',
            url: 'https://inventorywebapi2019.azurewebsites.net//api/Orders/',
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
                onSelectAll: this.onSelectAll
                
               // sortable: true,
              //  field: 'OrderID',
                //formatter: checkboxItem,
               // events: selectAll,

            },{
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
    function selectAll() {
        var checkboxes = document.getElementById("selectedOrders");
        if (checkboxes.checked) {
            for (var i = 0; i < checkboxes.length; i++) {
                
                    checkboxes[i].checked = true;
            }
        }
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

            var date = row.OrderDate;
          
            //d = row.RequestDate
            var requestedDate = new Date(date).toLocaleDateString();
                
           
            reqesterName = row.Department.DepartmentName
            document.getElementById('requestDate').innerHTML = requestedDate;
            document.getElementById('requestedBy').innerHTML = reqesterName;


            var oTableInit = new TableInit1();
            oTableInit.Init();

            $('#requests').bootstrapTable('refreshOptions', { url: 'https://inventorywebapi2019.azurewebsites.net//api/Request/' + orderid });
            $('#requests').bootstrapTable('refresh', { url: 'https://inventorywebapi2019.azurewebsites.net//api/Request/' + orderid });
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


var TableInit1 = function () {
    var oTableInit = new Object();

    oTableInit.Init = function () {
        $('#requests').bootstrapTable({
            method: 'get',
            url: 'https://inventorywebapi2019.azurewebsites.net//api/Request/' + orderid,
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
                title: 'Item Name',
                sortable: true,

                field: 'Catalogue.Description'
            },
            {
                align: "center",
                title: 'Requested Quantity',
                sortable: true,
                field: 'Needed'
            },
            {
                align: "center",
                title: 'Price',
                sortable: true,
                field: 'Catalogue.Price'
            },

            {
                align: "center",
                title: 'Available Quantity',
                sortable: true,
                // field: 'Catalogue.Price * Needed'
                field: 'Catalogue.Quantity'

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
function selectedItems() {
  
   var tab = document.getElementById("SearchItemTable");
    var rows = tab.rows;

    var objCheckBox = tab.getElementsByClassName('bs-checkbox ');
    
    var supplierlist = new Array();
    var orders = "";
    for (var i = 1; i < objCheckBox.length; i++) {
        
        if (objCheckBox[i].parentElement.className === "selected") {
            var OrderId = rows[i].cells[1].innerHTML;
           // orders = { "orderid": OrderId };
            $.ajax({
                url: 'https://inventorywebapi2019.azurewebsites.net//api/Request/' + OrderId,
                data: "",
                dataType: 'json',
                async: false,
                success: function (data) {
                    $.each(data, function (i, data) {
                        //console.log(data);
                        var existingItem = false;
                        var remarks = "";
                        requestId = data.Catalogue.ItemID;
                        binNumber = data.Catalogue.BinNumber;
                        needed = data.Needed;
                        actual = data.Catalogue.Quantity;
                        itemDescription = data.Catalogue.Description;
                        if (needed > actual) {
                            remarks = "Not enough Stock";
                          //  alert(remarks);
                        }
                        else {
                            remarks = "";
                        }
                        
                        jsonObj = { "RequestID": requestId, "ItemDescription": itemDescription, "neededQuantity": needed, "availableQuantity": actual, "binNumber": binNumber, "remarks": remarks, "orderid": OrderId };
                     
                        for (var j in jsonlist1) {
                            if (jsonlist1[j].RequestID === requestId) {
                                jsonlist1[j].neededQuantity += needed;
                                existingItem = true;

                                if (jsonlist1[j].neededQuantity > jsonlist1.availableQuantity) {
                                    remarks = "Not enough Stock";
                                }
                                break;
                            }                          

                        }

                        if (!existingItem) {
                            jsonlist1.push(jsonObj);
                        }
                        //orderlist.push(orders)
                        
                    });
                }
            });
            
                }
           
                }

   
    if (jsonlist1.length === 0) {
      alert("Please select item");
    }
   
    else {
        //var jsonlist2 = JSON.stringify(jsonlist1);
        //var orderlist2 = JSON.stringify(orderlist)
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            url: '/StoreClerk/GetRetrievalData',
            type: "post",
            dataType: "json",
            async: true,
            data: JSON.stringify(jsonlist1),               
            
            success: function (data) {
                window.location.href = data.redirecturl;
            }

            
        });
        var oButtonInit2 = new TableInit2();
        oButtonInit2.Init();
    }

};

function postData(approvalStatus) {
    var tab = document.getElementById("requests");
    var rows = tab.rows;
    var remarks = document.getElementById('remarks').value;
    var jsonlist = new Array(rows.length - 1);
    for (var i = 1; i < rows.length; i++) {
        var jsonObj = { "orderId": orderid, "requestStatus": approvalStatus, "remarks": remarks };
        jsonlist[i - 1] = jsonObj;
    }
    //alert(JSON.stringify(jsonlist));
    $.ajax({
        url: "/DepManager/SaveRequestStatus",
        type: "post",
        dataType: "text",
        async: true,
        data: JSON.stringify(jsonlist),
        success: function (data) {
            //  $('#successModal').modal('show');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        },

    });
};

function getRetrivalList() {
    var url = '/StoreClerk/GetRetrievals';
    $.ajax({
        url: url,
        data: {}, //parameters go here in object literal form
        type: 'GET',
        datatype: 'json',
        success: function (data) { alert('got here with data'); },
        error: function () { alert('something bad happened'); }
    });
}

var TableInit2 = function () {
    var oTableInit = new Object();
    
    oTableInit.Init = function () {
        $('#RetrievalTable').bootstrapTable({
            method: 'get',
            url: '/StoreClerk/GetRetrievals',
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
                title: 'Request ID',
                sortable: true,

                field: 'requestId'
            },
            {
                align: "center",
                title: 'Item Name',
                sortable: true,
                field: 'itemDescription'
            },
            {
                align: "center",
                title: 'Requested Quantity',
                sortable: true,
                field: 'neededQuantity'
            },

            {
                align: "center",
                title: 'Available Quantity',
                sortable: true,
                // field: 'Catalogue.Price * Needed'
                field: 'availableQuantity'

                }, {
                    align: "center",
                    title: 'Quantity Picked',
                    sortable: true,

                    formatter: InputTextBox
                },
          

                {
                    align: "center",
                    title: 'Bin#',
                    sortable: true,
                    // field: 'Catalogue.Price * Needed'
                    field: 'binNumber'

                },

                {
                    align: "center",
                    title: 'Remarks',
                    sortable: true,
                    // field: 'Catalogue.Price * Needed'
                    field: 'remarks'

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
     function InputTextBox(value, row, index) {
        return ['<input type="number" class="form-control" placeholder="Picked Quantity" id="quantity">'].join('');
    }

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

function openDisbursementList() {
    window.location.href = '/StoreClerk/DisbursementList';
}

function UpdateInventory() {
    var tab = document.getElementById("RetrievalTable");
    var rows = tab.rows;
    var objInput = tab.getElementsByClassName("form-control");
    //var quantity = objInput[rows - 1].value;
   var jsonlist = new Array(rows.length - 1);
    for (var i = 1; i < rows.length; i++) {
    
        if (objInput[i-1].value != null && objInput[i-1].value != "") {
            var jsonObj = { "itemDescription": rows[i].cells[1].innerHTML, "quantityPicked": objInput[i-1].value };
        }
            jsonlist[i - 1] = jsonObj;
    }

    $.ajax({
        url: "/StoreClerk/UpdateInventory",
        type: "post",
        dataType: "text",
        async: true,
        data: JSON.stringify(jsonlist),
        success: function (data) {
            alert("Updated Successfully");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        },

    });
};



