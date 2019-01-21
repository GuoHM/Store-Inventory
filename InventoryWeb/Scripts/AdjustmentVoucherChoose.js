function selectItem(obj) {
    var ItemAddedTable = document.getElementById("ItemAddedTable");
    var SearchItemTable = document.getElementById("SearchItemTable");
    var node = ItemAddedTable.rows[1];
    if (node && node.cells[0].innerHTML == "No matching records found") {
        node.parentNode.removeChild(node);
    }
    var rows = obj.parentNode.parentNode.rowIndex;
    var objQuantity = SearchItemTable.getElementsByClassName("quantity");
    var quantity = objQuantity[rows - 1].value;
    var objReason = SearchItemTable.getElementsByClassName("reason");
    var reason = objReason[rows - 1].value;
    if (reason == "" || quantity == "") {
        alert("Please input reason and quantity!");
    } else {
        var itemCode = SearchItemTable.rows[rows].cells[0].innerHTML;
        var Description = SearchItemTable.rows[rows].cells[1].innerHTML;   
        $("#ItemAddedTable").append("<tr><td>" + itemCode + "</td><td>" + Description + "</td><td>" + quantity + "</td><td>" + reason + "</td><td><input type='button'  value='remove' class='btn btn-danger' onclick='remove(this)'/></td></tr>");
        $(obj).parents("tr").remove();
    }
}
function remove(obj) {
    var rows = obj.parentNode.parentNode.rowIndex;
    var ItemAddedTable = document.getElementById("ItemAddedTable");
    var itemCode = ItemAddedTable.rows[rows].cells[0].innerHTML;
    var Description = ItemAddedTable.rows[rows].cells[1].innerHTML;
    $("#SearchItemTable").append("<tr><td>" + itemCode + "</td><td>" + Description + "</td><td><input type='number' class='form-control quantity' placeholder='Quantity'></td><td><input type='text' class='form-control reason' placeholder='Reason'></td><td><input type='button'  value='Select' class='btn btn-primary' onclick='selectItem(this)'/></td></tr>");
    $(obj).parents("tr").remove();
}