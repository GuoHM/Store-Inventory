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
        var totalprice = '$'+parseFloat(price.substr(1, price.length)) * orderQuantity+'.00';
        $("#ItemAddedTable").append("<tr align='center'><td><input class='checkbox' checked='checked' type='checkbox'></td><td>" + itemCode + "</td><td>" + Description + "</td><td>" + Quantity + "</td><td>" + ReorderQuantity + "</td><td>" + orderQuantity + "</td><td>" + totalprice + "</td><td>" + supplier + "</td><td><input type='button'  value='remove' class='btn btn-danger' onclick='remove(this)'/></td></tr>");
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
    $("#SearchItemTable").append("<tr align='center'><td>" + itemCode + "</td><td>" + Description + "</td><td>" + Quantity + "</td><td>" + ReorderQuantity + "</td><td><input type='number' class='form-control' placeholder='Quantity'></td><td>" + price + "</td><td>" + supplier + "</td><td><input type='button'  value='Select' class='btn btn-primary' onclick='selectItem(this)'/></td></tr>");
     $(obj).parents("tr").remove();
}
var json;
function confirm() {
    var tab = document.getElementById("ItemAddedTable");
    var rows = tab.rows;
    
    var objCheckBox = tab.getElementsByClassName('checkbox');
    var jsonlist = new Array();
    var supplierlist = new Array();
    for (var i = 0; i < objCheckBox.length; i++) {
        if (objCheckBox[i].checked) {
            var jsonObj = { "itemID": rows[i + 1].cells[1].innerHTML, "quantity": rows[i + 1].cells[5].innerHTML, "totalPrice": rows[i + 1].cells[6].innerHTML, "supplier": rows[i + 1].cells[7].innerHTML, "description": rows[i + 1].cells[2].innerHTML };  
            jsonlist.push(jsonObj);
            supplierlist.push(rows[i + 1].cells[7].innerHTML);
        }
    }
    if (jsonlist.length == 0) {
        alert("Please select item to purchase");
    } else {
        if (isAllSame(supplierlist)) {
            $.ajax({
                url: "/StoreClerk/ConfirmOrder",
                type: "post",
                dataType: "text",
                async: true,
                data: JSON.stringify(jsonlist),
                success: function (data) {
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
                    now.setDate(now.getDate() + 3);
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
                    document.getElementById("totalPrice").innerHTML = 'Total Price:$'+totalPrice+".00";
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
            alert("Cannot choose item from different supplier!");
        }
    }    
}

function savePurchaseOrder() {
    json['delieverTo'] = $('#delieverTo').val();
    json['attentionTo'] = $('#attentionTo').val();
    json['dateToDeliver'] = $('#dateToDeliver').val();
    $("#btnSave").attr("disabled", true);
    $("#btnConfirm").attr("disabled", true);
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
            $("#btnConfirm").attr("disabled", false);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        }
    });
}

function lowStock() {
    $.ajax({
        url: "/StoreClerk/LowStock",
        type: "get",
        dataType: "text",
        async: true,
        success: function (data) {
            var json = JSON.parse(data);
            var ItemAddedTable = document.getElementById("ItemAddedTable");
            var node = ItemAddedTable.rows[1];
            if (node && node.cells[0].innerHTML == "No matching records found") {
                node.parentNode.removeChild(node);
            }
            for (var i = 0; i < json.length; i++) {
                $("#ItemAddedTable").append("<tr align='center'><td><input class='checkbox' checked='checked' type='checkbox'></td><td>" + json[i].ItemID + "</td><td>" + json[i].Description + "</td><td>" + json[i].Quantity + "</td><td>" + json[i].ReorderQuantity + "</td><td>" + json[i].ReorderLevel + "</td><td>$" + parseFloat(json[i].ReorderQuantity) * parseFloat(json[i].Price) + ".00</td><td>" + json[i].Supplier1 + "</td><td><input type='button'  value='remove' class='btn btn-danger' onclick='remove(this)'/></td></tr>");
            }
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