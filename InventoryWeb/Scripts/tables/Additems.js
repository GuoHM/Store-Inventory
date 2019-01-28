$(document).ready(function () {
	var oTableInit = new TableInit();
	oTableInit.Init();

	var oButtonInit = new ButtonInit();
	oButtonInit.Init();

});

var TableInit = function () {
	var oTableInit = new Object();

	oTableInit.Init = function () {
		$('#AddItems').bootstrapTable({
			method: 'get',
			url: 'https://inventorywebapi2019.azurewebsites.net/api/PurchaseOrder',
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
			columns: [
				{
					align: "center",
					title: 'Select',
					sortable: true,
					//sortable: true,
					formatter: ChooseOrder
					//field: 'RequestID'
				}, {
					align: "center",
					title: 'Date',
					sortable: true,
					//sortable: true,
					field: 'PurchaseDate'
				}, {
					align: "center",
					title: 'Purchase Order',
					sortable: true,
					//sortable: true,
					field: 'PurchaseOrderID',
					//events: operateEvents,
					// formatter: InputTextBox
				}, {
					align: "center",
					title: 'Supplier',
					sortable: true,
					// sortable: true,
					field: 'Supplier.SupplierName',
					//events: operateEvents,
					// formatter: InputTextBox
				},
				{
					align: "center",
					title: 'Expected',
					sortable: true,
					//sortable: true,
					field: 'ExpectedDate',
					//events: operateEvents,
					// formatter: InputTextBox
				}, {
					align: "center",
					title: 'Status',
					sortable: true,
					//sortable: true,
					field: 'PurchaseOrderStatus',
					//events: operateEvents,
					// formatter: InputTextBox
				}, {
					align: "center",
					title: 'Action',
					sortable: true,
					// sortable: true,
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
	//oTableInit.queryParams = function (params) {

	//    var temp = {
	//        Purchase: $("#").val()
	//    };
	//    return temp;
	//}; 
	function ChooseOrder() {
		return [
			'<input type="checkbox" name ="choose" onclick = "choose(this);"   class="checkbox" />',
		].join('');
	}


	function selectItem() {
		
			return [
				'<input type="button" id="view" value="View Details"onclick="ShowPurchaseDetails(this)"  class="btn btn-primary" />',

				'<input type="button" id="view1" value="Cancel PurchaseOrder"onclick="Cancel PO(this)"  class="btn btn-info" />'
			].join('');
		
	}


	function openPopup() {
		$("#ApproveRequestModal").modal('show');
	}
	operateEvents = {
		'click #view': function (e, value, row, index) {

			$("#ApproveRequestModal").modal('show');
			orderid = row.PurchaseOrderID;
		},

		'click #view1': function (e, value, row, index) {
			

			orderid = row.PurchaseOrderID;
			supplierID = row.SupplierID;
			totalPrice = row.TotalPrice;
			purchaseDate = row.PurchaseDate;
			deliverAddress = row.DeliverAddress;
			orderBy = row.OrderBy;
			expectedDate = row.ExpectedDate;
			purchaseOrderStatus = row.PurchaseOrderStatus;

			PurchaseOrder1 = [];

			

			var jsonobj = {
				"orderid": orderid,
				"supplierID": supplierID,
				"totalPrice": totalPrice,
				"purchaseDate": purchaseDate,
				"deliverAddress": deliverAddress,
				"orderBy": orderBy,
				"expectedDate": expectedDate,
				"purchaseOrderStatus": purchaseOrderStatus,

			};
			if (purchaseOrderStatus === "Fulfilled           ") {
				alert("Cannot cancel fulfilled request");
			}
			else {
				PurchaseOrder1.push(jsonobj);

				$.ajax({
					contentType: 'application/json; charset=utf-8',
					url: '/StoreClerk/UpdatePOStatusToCancel',
					type: 'post',
					dataType: 'json',
					async: true,
					data: JSON.stringify(PurchaseOrder1),
					//data: jsonobj
					success: function (data) {
						alert(data);
					},
					error: function (result) {
						alert(result);
					}

				});
			}

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






