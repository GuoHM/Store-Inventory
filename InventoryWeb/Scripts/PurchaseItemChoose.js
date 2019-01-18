var index = 1;
function selectItem(obj) {
    
    var ItemAddedTable = document.getElementById("ItemAddedTable");
    var SearchItemTable = document.getElementById("SearchItemTable");
    var node = ItemAddedTable.rows[1];
    if (node && node.cells[0].innerHTML == "No matching records found") {
        node.parentNode.removeChild(node);
    }
    var rows = obj.parentNode.parentNode.rowIndex;
    var objInput = SearchItemTable.getElementsByClassName("form-control");
    var orderQuantity = objInput[rows - 1].value;
    if (orderQuantity == "") {
        alert("Please input quantity!");
    } else {
        var itemCode = SearchItemTable.rows[rows].cells[0].innerHTML;
        var Description = SearchItemTable.rows[rows].cells[1].innerHTML;
        var Quantity = SearchItemTable.rows[rows].cells[2].innerHTML;
        var ReorderQuantity = SearchItemTable.rows[rows].cells[3].innerHTML;
        var price = SearchItemTable.rows[rows].cells[5].innerHTML;
        var supplier = SearchItemTable.rows[rows].cells[6].innerHTML;
        var totalprice = price * orderQuantity;
        $("#ItemAddedTable").append("<tr><td><input class='checkbox'  name='btSelectItem' type='checkbox'></td><td>" + itemCode + "</td><td>" + Description + "</td><td>" + Quantity + "</td><td>" + ReorderQuantity + "</td><td>" + orderQuantity + "</td><td>" + totalprice + "</td><td>" + supplier + "</td><td><input type='button'  value='remove' class='btn btn-danger' onclick='remove(this)'/></td></tr>");
        $(obj).parents("tr").remove();
    }
}
function remove(obj) {
    var rows = obj.parentNode.parentNode.rowIndex;
    var ItemAddedTable = document.getElementById("ItemAddedTable");
    var itemCode = ItemAddedTable.rows[rows].cells[1].innerHTML;
    var Description = ItemAddedTable.rows[rows].cells[2].innerHTML;
    var Quantity = ItemAddedTable.rows[rows].cells[3].innerHTML;
    var ReorderQuantity = ItemAddedTable.rows[rows].cells[4].innerHTML;
    var orderQuantity = ItemAddedTable.rows[rows].cells[5].innerHTML;
    var totalprice = ItemAddedTable.rows[rows].cells[6].innerHTML;
    var supplier = ItemAddedTable.rows[rows].cells[7].innerHTML;
    var price = totalprice / orderQuantity;
    $("#SearchItemTable").append("<tr><td>" + itemCode + "</td><td>" + Description + "</td><td>" + Quantity + "</td><td>" + ReorderQuantity + "</td><td><input type='number' class='form-control' placeholder='Quantity'></td><td>" + price + "</td><td>" + supplier + "</td><td><input type='button'  value='Select' class='btn btn-primary' onclick='selectItem(this)'/></td></tr>");
     $(obj).parents("tr").remove();
}
function postData() {
    var tab = document.getElementById("ItemAddedTable");
    var rows = tab.rows;
    debugger;
    var objCheckBox = tab.getElementsByClassName('checkbox');
    var jsonlist = new Array();
    for (var i = 0; i < objCheckBox.length;i++) {
        if (objCheckBox[i].checked) {
            var jsonObj = { "itemID": rows[i + 1].cells[1].innerHTML, "quantity": rows[i + 1].cells[5].innerHTML, "totalPrice": rows[i + 1].cells[6].innerHTML };  
            jsonlist.push(jsonObj);
        }
    }
    alert(JSON.stringify(jsonlist));
    //$.ajax({
    //    url: "/StoreClerk/SavePurchaseOrder",
    //    type: "post",
    //    dataType: "text",
    //    async: true,
    //    data: JSON.stringify(jsonlist),
    //    success: function (data) {
    //        $('#successModal').modal('show');
    //    },
    //    error: function (XMLHttpRequest, textStatus, errorThrown) {
    //        alert(XMLHttpRequest.status);
    //        alert(XMLHttpRequest.readyState);
    //        alert(textStatus);
    //    },

    //});
}