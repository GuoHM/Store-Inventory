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
	var userid = document.getElementById('userid').textContent;

	oTableInit.Init = function () {
		$('#SearchItemTable').bootstrapTable({
			method: 'get',
			url: 'http://inventorywebapi2019.azurewebsites.net/api/AdjustmentItems/' + userid,
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
			columns: [
				{
					align: "center",
					title: 'AdjustmentID',
					sortable: true,

					field: 'AdjustmentID'
				}, {
					align: "center",
					title: 'Submitted To',
					sortable: true,

					field: 'AspNetUsers.UserName'
				}, {
					align: "center",
					title: 'Date Submitted',
					sortable: true,
					field: 'Date'
					//events: operateEvents,
					// formatter: InputTextBox
				}, {
					align: "center",
					title: 'Status',
					sortable: true,
					field: 'AdjustmentStatus'
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



			adjustmentID = row.AdjustmentID;
			requestedDate = row.Date;

			submittedTo= row.AspNetUsers.UserName;
			document.getElementById('requestDate').innerHTML = requestedDate;
			document.getElementById('requestedBy').innerHTML = submittedTo;


			var oTableInit = new TableInit1();
			oTableInit.Init();

			$('#requests').bootstrapTable('refreshOptions', { url: 'http://inventorywebapi2019.azurewebsites.net/api/AdjustmentItem/' + adjustmentID });
			$('#requests').bootstrapTable('refresh', { url: 'http://inventorywebapi2019.azurewebsites.net/api/AdjustmentItem/' + adjustmentID });

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
			url: 'http://inventorywebapi2019.azurewebsites.net/api/AdjustmentItem/' + adjustmentID,
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
				title: 'Item Name',
				sortable: true,

				field: 'description'
			},
			{
				align: "center",
				title: 'Quantity Adjusted',
				sortable: true,
				field: 'quantity'
			},
			{
				align: "center",
				title: 'CostPerUnit',
				sortable: true,
				field: 'cost'
			},

			{
				align: "center",
				title: 'Total Cost',
				sortable: true,
				field: 'totalcost'
				//field: selectItem

			},
			{
				align: "center",
				title: 'Reason',
				sortable: true,
				field: 'reason'
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

		return row.Catalogue.Price * Quantity;

	}


	operateEvents = {
		'click #view': function (e, value, row, index) {
			$("#ApproveRequestModal").modal('show');
		}
	};


	return oTableInit;
};




