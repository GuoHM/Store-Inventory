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
				field: 'ItemID'
			}, {
				align: "center",
				title: 'Description',
				sortable: true,
				//sortable: true,
				field: 'Description',
				//events: operateEvents,
				// formatter: InputTextBox
			}, {
				align: "center",
				title: 'Reorder Level',
				sortable: true,
				//sortable: true,
				field: 'ReorderLevel',
				//events: operateEvents,
				// formatter: InputTextBox
			},
			{
				align: "center",
				title: 'Reorder Quantity',
				sortable: true,
				//sortable: true,
				field: 'ReorderQuantity',
				//events: operateEvents,
				// formatter: InputTextBox
			}, {
				align: "center",
				title: 'Unit of Measure',
				sortable: true,
				//sortable: true,
				field: 'MeasureUnit',
				//events: operateEvents,
				// formatter: InputTextBox
			}, {
				align: "center",
				title: 'Bin Number',
				sortable: true,
				//sortable: true,
				field: 'BinNumber',
				//events: operateEvents,
				// formatter: InputTextBox
			}, {
				align: "center",
				title: 'Quantity Left',
				sortable: true,
				//sortable: true,
				field: 'Quantity',
				//events: operateEvents,
				// formatter: InputTextBox
			}
			],
			formatLoadingMessage: function () {
				return "loading...";
			}
		});

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