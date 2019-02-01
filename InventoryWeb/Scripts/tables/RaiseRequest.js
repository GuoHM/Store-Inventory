$(document).ready(function() {
	var oTableInit = new TableInit();
	oTableInit.Init();
	
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});

var TableInit = function() {
	var oTableInit = new Object();
	oTableInit.Init = function() {
        $('#SearchItemTable').bootstrapTable({
            method: 'get',
            url: 'https://inventorywebapi2019.azurewebsites.net/api/Catalogue',
            //toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true, // 是否显示行间隔色
            cache: false, // 是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true, // 是否显示分页（*）
            sortable: true, // 是否启用排序
            sortOrder: "asc", // 排序方式
            queryParams: oTableInit.queryParams,// 传递参数（*）
            sidePagination: "client", // 分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1, // 初始化加载第一页，默认第一页
            pageSize: 5, // 每页的记录行数（*）
            pageList: [10, 25, 50, 100], // 可供选择的每页的行数（*）
            search: true, //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: false,
            queryParamsType: "",
            showRefresh: true, // 是否显示刷新按钮
            minimumCountColumns: 2, // 最少允许的列数
            clickToSelect: false, // 是否启用点击选中行
            height: 500, // 行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            // uniqueId: "ID", //每一行的唯一标识，一般为主键列
            showToggle: false, // 是否显示详细视图和列表视图的切换按钮
            cardView: false, // 是否显示详细视图
            detailView: false, // 是否显示父子表
            showExport: false,                     //是否显示导出
            exportDataType: "basic",              //basic', 'all', 'selected'.
            showColumns: false,
            columns: [{
                align: "center",
                title: 'Item Name',
                sortable: true,
                
                field: 'Description'
            }, {
                    align: "center",
                    title: 'UOM',
                    sortable: true,

                    field: 'MeasureUnit'
                }, {
                align: "center",
                title: 'Quantity',
                sortable: true,
               
                formatter: InputTextBox
            }, {
                align: "center",
                title: 'Select',
                sortable: true,
              
                formatter: selectItem
            }
            ],
            formatLoadingMessage: function () {
                return "loading...";
            }
        });
        $('#ItemAddedTable').bootstrapTable({
            method: 'get',
            //url : "/admin/api/enrollment-student",
            //toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true, // 是否显示行间隔色
            cache: false, // 是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true, // 是否显示分页（*）
            sortable: false, // 是否启用排序
            sortOrder: "asc", // 排序方式
            queryParams: oTableInit.queryParams,// 传递参数（*）
            sidePagination: "client", // 分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1, // 初始化加载第一页，默认第一页
            pageSize: 10, // 每页的记录行数（*）
            pageList: [10, 25, 50, 100], // 可供选择的每页的行数（*）
            search: false, //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: false,
            queryParamsType: "",
            showRefresh: false, // 是否显示刷新按钮
            minimumCountColumns: 2, // 最少允许的列数
            clickToSelect: false, // 是否启用点击选中行
            height: 500, // 行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            // uniqueId: "ID", //每一行的唯一标识，一般为主键列
            showToggle: false, // 是否显示详细视图和列表视图的切换按钮
            cardView: false, // 是否显示详细视图
            detailView: false, // 是否显示父子表
            showExport: true,                     //是否显示导出
            exportDataType: "basic",              //basic', 'all', 'selected'.
            showColumns: false,
            columns: [{
                align: "center",
                title: 'Item Name',
                sortable: false
            }, {
                    align: "center",
                    title: 'UOM',
                    sortable: false
                },{
                align: "center",
                title: 'Quantity',
                sortable: false
            }, {
                align: "center",
                title: 'Remove',
                sortable: false
            }
            ],
            formatLoadingMessage: function () {
                return "loading...";
            }
        });
    };
	function InputTextBox(value, row, index) {
		return ['<input type="number" class="form-control" placeholder="Quantity" id="quantity">'].join('');
    }
    function selectItem(value, row, index) {
        return [
            '<input type="button" value="Add" onclick="selectItem(this)" class="btn btn-primary" />',
        ].join('');
    }
	return oTableInit;
};

var ButtonInit = function() {
	var oInit = new Object();
	var postdata = {};

	oInit.Init = function() {
		// button
		

	};

	return oInit;
};

