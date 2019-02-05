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
    if (quantity > 9999 || quantity <= 0 || quantity == "" || quantity % 1!=0) {
        alert("Invalid quantity!");
    }
    else {
        var itemName = SearchItemTable.rows[rows].cells[0].innerHTML;
        var uom = SearchItemTable.rows[rows].cells[1].innerHTML;
        $("#ItemAddedTable").append("<tr><td>" + itemName + "</td><td>" + uom + "</td><td>" + quantity + "</td><td><input type='button'  value='remove' class='btn btn-danger' onclick='remove(this)'/></td></tr>");
        $(obj).parents("tr").remove();
        $("#btnConfirm").attr("disabled", false);
    }
}
function remove(obj) {
    var rows = obj.parentNode.parentNode.rowIndex;
    var itemName = document.getElementById("ItemAddedTable").rows[rows].cells[0].innerHTML;
    var uom = document.getElementById("ItemAddedTable").rows[rows].cells[1].innerHTML;
    if (!hasItemAlreadyExist(itemName)) {
        $("#SearchItemTable").append("<tr align='center'><td>" + itemName + "</td><td>" + uom + "</td><td><input type='number' max='9999' min='0' class='form-control' placeholder='Quant'></td><td><input type='button'  value='Add' class='btn btn-primary' onclick='selectItem(this)'/></td></tr>");
    }
    $(obj).parents("tr").remove();
    var tab = document.getElementById("ItemAddedTable");
    var rows = tab.rows;    
    if (rows.length == 1) {
        $("#btnConfirm").attr("disabled", true);
    }
}
function hasItemAlreadyExist(itemDescription) {
    var tab = document.getElementById("SearchItemTable");
    var rows = tab.rows;
    for (var i = 0; i < rows.length; i++) {
        if (rows[i].cells[0].innerHTML == itemDescription) {
            return true;
        }
    }
    return false;
}
function postData() {
    $("#btnConfirm").attr("disabled", true);
    var tab = document.getElementById("ItemAddedTable");
    var rows = tab.rows;
  
        if (ItemAddedTable.rows[1].cells[0].innerHTML == "No matching records found") {
            $("#btnConfirm").attr("disabled", false);
            alert("Please select a item!");
            return;
        }
        var jsonlist = new Array(rows.length - 1);
        for (var i = 1; i < rows.length; i++) {
            var jsonObj = { "description": rows[i].cells[0].innerHTML, "quantity": rows[i].cells[2].innerHTML };
            jsonlist[i - 1] = jsonObj;
        }
        if (hasDuplicated(jsonlist)) {
            alert('Cannot select duplicated item!');
            $("#btnConfirm").attr("disabled", false);
            return;
        } else {
            $.ajax({
                url: "/Request/SaveRequest",
                type: "post",
                dataType: "text",
                async: true,
                data: JSON.stringify(jsonlist),
                success: function (data) {
                    $('#successModal').modal('show');
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                },

            });
        }
    
   
}

function hasDuplicated(arr) {
 
    for (var i = 0; i < arr.length - 1; i++) {
        for (var j = 0; j < arr.length; j++) {
            if (i != j) {
                if (arr[i].description == arr[j].description) {
                    return true;
                }
            }
        }
       
    }
    return false;
}
