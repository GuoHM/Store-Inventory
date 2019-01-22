

function ShowPurchaseDetails(obj) {
    var tab = document.getElementById("AddItems");
    var rows = obj.parentNode.parentNode.rowIndex;
    var PurchaseOrderID = tab.rows[rows].cells[2].innerHTML;
  
    $.ajax({
        url: "/StoreClerk/ShowPurchasedetails",
        type: "post",
        dataType: "text",
        async: true,
        data: { purchaseID: PurchaseOrderID },

        success: function (data) {
         
            var ShowPurchaseDetailsTable = document.getElementById("ShowPurchaseDetails");
            var rowNum = ShowPurchaseDetailsTable.rows.length;
            for (var i = 1; i < rowNum; i++) {
                ShowPurchaseDetailsTable.deleteRow(i);
                rowNum = rowNum - 1;
                i = i - 1;
            }
            var node = ShowPurchaseDetailsTable.rows[1];
            if (node && node.cells[0].innerHTML == "No matching records found"){
                node.parentNode.removeChild(node);
    }

            var json = JSON.parse(data);
            for (var i = 0; i < json.length; i++) {
                $("#ShowPurchaseDetails").append("<tr align='center'><td>" + json[i].itemID + "</td><td>"
                    + json[i].description + "</td><td>" + json[i].quantity + "</td><td>" + json[i].price +
                    "</td><td>" + json[i].amount + "</td></tr>");
            }
            debugger;
         
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        }
    });
}
function UpdateQuantity() {
    var tab = document.getElementById("AddItems");
    var rows = tab.rows;
    var objCheckBox = tab.getElementsByClassName('checkbox');
    var purchaseIDList = new Array();
   
    for (var i = 0; i < objCheckBox.length; i++) {
        if (objCheckBox[i].checked) {
            var jsonObj = { "orderid": rows[i + 1].cells[2].innerHTML };
            var orderid = rows[i + 1].cells[2].innerHTML;
            purchaseIDList.push(jsonObj);
            
        }
    }
    alert(orderid)
 
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        url: "/StoreClerk/UpdateQuantity",
        type: "post",
        dataType: "text",
        async: true,
        data: JSON.stringify(purchaseIDList)
    
    });
}


