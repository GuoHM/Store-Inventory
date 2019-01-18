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
            url: 'http://inventorywebapi2019.azurewebsites.net/api/request',
            //toolbar: '#toolbar',               
            striped: true, 
            cache: false, 
            pagination: true, 
            sortable: true, 
            sortOrder: "asc", 
            queryParams: oTableInit.queryParams,
            sidePagination: "client", 
            pageNumber: 1,
            pageSize: 10, 
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
                title: 'ItemCode',
                sortable: true,
                sortable: true,
                field: 'Description'
            }, {
                align: "center",
                title: 'Description',
                sortable: true,
                sortable: true,
                //field : 'ID',
                //events: operateEvents,
                //formatter: InputTextBox
            }, {
                align: "center",
                title: 'Quantity',
                sortable: true,
                sortable: true,
                //field : 'ID',
                //events: operateEvents,
                //formatter: InputTextBox
            }, {
                align: "center",
                title: 'Reason',
                sortable: true,
                sortable: true,
                //field : 'ID',
                //events: operateEvents,
                //formatter: selectItem
            }, {
                align: "center",
                title: 'Select',
                sortable: true,
                sortable: true,
                //field : 'ID',
                //events: operateEvents,
                //formatter: selectItem
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
    function InputTextBox(value, row, index) {
        return [
            '<input type="text" maxlength="5" class="form-control" placeholder="CourseID" id="quantity">'
        ].join('');
    }
    function selectItem(value, row, index) {
        return [
            '<input type="button" value="Select" onclick="selectItem(this)" class="btn btn-primary" />',
        ].join('');
    }

    operateEvents = {
        'click .like': function (e, value, row, index) {
            $("#editEnrollmentModal").modal('show');
            $("#studentnameEdit").val(row.account.name);
            $("#useridEdit").val(row.id.userid);
            $("#coursenameEdit").val(row.course.courseName);
            $("#courseidEdit").val(row.id.courseid);
            $("#enrollmentDateEdit").val(row.enrollmentDate);
            $("#gradesEdit").val(row.grades);
            var date = document.getElementById("enrollmentDateEdit").value;
            var grades = document.getElementById("gradesEdit").value;
            var url = 'editErollment/' + row.id.userid + '/' + date + '/' + grades + '/' + row.id.courseid;
            $("#editForm").attr('action', url);
        },
        'click .remove': function (e, value, row, index) {
            $("#deleteEnrollmentModal").modal('show');
            var url = 'deleteErollment/' + row.id.userid + '/' + row.id.courseid;
            $("#deleteForm").attr('action', url);
        }
    };


    return oTableInit;
};

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        // button
        $('#btn_add').click(function () {
            $("#addEnrollmentModal").modal('show')
        })


    };

    return oInit;
};

function editFunction() {
    var stu = document.getElementById("useridEdit").value;
    var course = document.getElementById("courseidEdit").value;
    var date = document.getElementById("enrollmentDateEdit").value;
    var grades = document.getElementById("gradesEdit").value;
    var url = 'editErollment/' + stu + '/' + date + '/' + grades + '/' + course;
    $("#editForm").attr('action', url);
}