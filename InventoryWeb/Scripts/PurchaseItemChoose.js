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
    if (orderQuantity == "" || orderQuantity <= 0 || orderQuantity % 1 !=0 ) {
        alert("Please input valid quantity!");
    } else {
        var itemCode = SearchItemTable.rows[rows].cells[0].innerHTML;
        var Description = SearchItemTable.rows[rows].cells[1].innerHTML;
        var Quantity = SearchItemTable.rows[rows].cells[2].innerHTML;
        var ReorderQuantity = SearchItemTable.rows[rows].cells[3].innerHTML;
        var price = SearchItemTable.rows[rows].cells[5].innerHTML;
        var uom = SearchItemTable.rows[rows].cells[6].innerHTML;
        var supplier = SearchItemTable.rows[rows].cells[7].innerHTML;
        debugger;
        if (price.charAt(price.length - 2) != '0' && price.charAt(price.length - 1) != '0') {
            var totalprice = '$' + parseFloat(price.substr(1, price.length)) * orderQuantity;
        } else {
            var totalprice = '$' + parseFloat(price.substr(1, price.length)) * orderQuantity + '.00';
        }
        $("#ItemAddedTable").append("<tr align='center'><td><input class='checkbox' checked='checked' type='checkbox'></td><td>" + itemCode + "</td><td>" + Description + "</td><td>" + Quantity + "</td><td>" + ReorderQuantity + "</td><td>" + orderQuantity + "</td><td>" + totalprice + "</td><td>" + uom + "</td><td>" + supplier + "</td><td><input type='button'  value='remove' class='btn btn-danger' onclick='remove(this)'/></td></tr>");
        $(obj).parents("tr").remove();
        $("#btnConfirm").attr("disabled", false);
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
    var uom = ItemAddedTable.rows[rows].cells[7].innerHTML;
    var supplier = ItemAddedTable.rows[rows].cells[8].innerHTML;
    var price = "" + totalprice.substr(1, totalprice.length) / orderQuantity;
    if (price.length>1) {
        var price = '$' + price;
    } else {
        var price = '$' + price + '.00';
    }
    if (!hasItemAlreadyExist(itemCode)) {
        $("#SearchItemTable").append("<tr align='center'><td>" + itemCode + "</td><td>" + Description + "</td><td>" + Quantity + "</td><td>" + ReorderQuantity + "</td><td><input type='number' class='form-control' placeholder='Quantity'></td><td>" + price + "</td><td>" + uom + "</td><td>" + supplier + "</td><td><input type='button'  value='Select' class='btn btn-primary' onclick='selectItem(this)'/></td></tr>");
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
var json;
function confirm() {
    var tab = document.getElementById("ItemAddedTable");
    var rows = tab.rows;
    $("#btnConfirm").attr("disabled", true);
    var objCheckBox = tab.getElementsByClassName('checkbox');
    var jsonlist = new Array();
    var supplierlist = new Array();
    for (var i = 0; i < objCheckBox.length; i++) {
        if (objCheckBox[i].checked) {
            var jsonObj = { "itemID": rows[i + 1].cells[1].innerHTML, "quantity": rows[i + 1].cells[5].innerHTML, "totalPrice": rows[i + 1].cells[6].innerHTML, "supplier": rows[i + 1].cells[8].innerHTML, "description": rows[i + 1].cells[2].innerHTML };  
            jsonlist.push(jsonObj);
            supplierlist.push(rows[i + 1].cells[8].innerHTML);
        }
    }
    if (jsonlist.length == 0) {
        alert("Please select item to purchase");
        $("#btnConfirm").attr("disabled", false);
    } else {
        if (isAllSame(supplierlist) && !hasDuplicated(jsonlist)) {
            $.ajax({
                url: "/StoreClerk/ConfirmOrder",
                type: "post",
                dataType: "text",
                async: true,
                data: JSON.stringify(jsonlist),
                success: function (data) {
                    $("#btnConfirm").attr("disabled", false);
                    $("#confirmTable").empty();
                    $("#confirmTable").append("<thead><tr>"
                        + "<th>Item Code</th>"
                        + "<th>Description</th>"
                        + "<th>Quantity</th>"
                        + "<th>Total Price</th>"
                        +"</tr></thead><tbody>");
                    json = JSON.parse(data);
                    $('#supplierAddress').val(json.supplierAddress);
                    $('#attentionTo').val(json.attentionTo);
                    $('#delieverTo').val("Logic University");
                    var now = new Date();
                    for (var i = 0; i < 3; i++) {
                        now.setDate(now.getDate() + 1);
                        while (now.getDay() == '6' || now.getDay() == '0') {
                            now.setDate(now.getDate() + 1);
                        }
                    }
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var date = now.getFullYear() + "-" + (month) + "-" + (day);
                    $('#dateToDeliver').val(date);
                    var totalPrice=0;
                    for (var i = 0; i < json.tablelist.length;i++ ) {
                        $("#confirmTable").append("<tr><td>" + json.tablelist[i].itemID
                            + "</td><td>" + json.tablelist[i].description
                            + "</td><td>" + json.tablelist[i].quantity
                            + "</td><td>" + json.tablelist[i].totalPrice + "</td></tr>");
                        totalPrice += parseFloat(json.tablelist[i].totalPrice.substr(1, json.tablelist[i].totalPrice.length));
                    }
                    document.getElementById("totalPrice").innerHTML = 'Total Price:$'+totalPrice;
                    $("#confirmTable").append("</tbody>");
                    $('#confirmModal').modal('show');
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });
        } else {
            alert("Invalid selction,cannot choose duplicated items or cannot choose from different supplier!");
            $("#btnConfirm").attr("disabled", false);
        }
    }    
}

function savePurchaseOrder() {
    json['delieverTo'] = $('#delieverTo').val();
    json['attentionTo'] = $('#attentionTo').val();
    json['dateToDeliver'] = $('#dateToDeliver').val();
    $("#btnSave").attr("disabled", true);
    $.ajax({
        url: "/StoreClerk/SavePurchaseOrder",
        type: "post",
        dataType: "text",
        async: true,
        data: JSON.stringify(json),
        success: function (data) {
            $('#confirmModal').modal('hide');
            $('#successModal').modal('show');
            $("#btnSave").attr("disabled", false);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        }
    });
}

function lowStock() {
    $("#btnLowStock").attr("disabled", true);
    $("#btnConfirm").attr("disabled", false);
    $.ajax({
        url: "/StoreClerk/LowStock",
        type: "get",
        dataType: "text",
        async: true,
        success: function (data) {
            var json = JSON.parse(data);
            var ItemAddedTable = document.getElementById("ItemAddedTable");
            for (var i = 1; i < ItemAddedTable.rows.length; i++) {
                ItemAddedTable.rows[i].parentNode.removeChild(ItemAddedTable.rows[i]);
                i--;
            }
            for (var i = 0; i < json.length; i++) {
                $("#ItemAddedTable").append("<tr align='center'><td><input class='checkbox' checked='checked' type='checkbox'></td><td>" + json[i].ItemID + "</td><td>" + json[i].Description + "</td><td>" + json[i].Quantity + "</td><td>" + json[i].ReorderQuantity + "</td><td>" + json[i].ReorderQuantity + "</td><td>$" + parseFloat(json[i].ReorderQuantity) * parseFloat(json[i].Price) + ".00</td><td>" + json[i].MeasureUnit + "</td><td>" + json[i].Supplier1 + "</td><td><input type='button'  value='remove' class='btn btn-danger' onclick='remove(this)'/></td></tr>");
            }
            $("#btnLowStock").attr("disabled", false);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        }
    });
}

function isAllSame(arr) {
    for (var i = 0; i < arr.length-1; i++) {
        if (arr[i] != arr[i + 1]) {
            return false;
        }
    }
    return true;
}  

//function hasDuplicated(arr) {
//    for (var i = 0; i + 1 < arr.length; i++) {
//        if (arr[i].itemID == arr[i + 1].itemID) {
//            return true;
//        }
//    }
//    return false;
//}

function hasDuplicated(arr) {

    for (var i = 0; i < arr.length - 1; i++) {
        for (var j = 0; j < arr.length; j++) {
            if (i != j) {
                if (arr[i].itemID == arr[j].itemID) {
                    return true;
                }
            }
        }

    }
    return false;
}