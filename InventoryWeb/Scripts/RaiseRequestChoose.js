function selectItem(obj) {
    var ItemAddedTable = document.getElementById("ItemAddedTable");
    var SearchItemTable = document.getElementById("SearchItemTable");
    var node = ItemAddedTable.rows[1];
    if (node && node.cells[0].innerHTML == "No matching records found") {
        node.parentNode.removeChild(node);
    }
    var rows = obj.parentNode.parentNode.rowIndex;
    var objInput = SearchItemTable.getElementsByClassName("form-control");
    var quantity = objInput[rows - 1].value;
    if (quantity == "") {
        alert("Please input quantity!");
    } else {
        var itemName = SearchItemTable.rows[rows].cells[0].innerHTML;
        $("#ItemAddedTable").append("<tr><td>" + itemName + "</td><td>" + quantity + "</td><td><input type='button'  value='remove' class='btn btn-danger' onclick='remove(this)'/></td></tr>");
        $(obj).parents("tr").remove();
    }
}
function remove(obj) {
    var rows = obj.parentNode.parentNode.rowIndex;
    var itemName = document.getElementById("ItemAddedTable").rows[rows].cells[0].innerHTML;
    $("#SearchItemTable").append("<tr><td>" + itemName + "</td><td><input type='number' class='form-control' placeholder='Please input the quantity'></td><td><input type='button'  value='Select' class='btn btn-primary' onclick='selectItem(this)'/></td></tr>");
    $(obj).parents("tr").remove();
}