

function ShowPurchaseDetails(obj) {
    var tab = document.getElementById("AddItems");
    var rows = obj.parentNode.parentNode.rowIndex;
    var PurchaseOrderID = tab.rows[rows].cells[2].innerHTML;
    document.getElementById("date").innerHTML = tab.rows[rows].cells[1].innerHTML;
    document.getElementById("orderNo").innerHTML = PurchaseOrderID;
    document.getElementById("supplier").innerHTML = tab.rows[rows].cells[3].innerHTML;
    document.getElementById("expected").innerHTML = tab.rows[rows].cells[4].innerHTML;
    document.getElementById("status").innerHTML = tab.rows[rows].cells[5].innerHTML;
    $.ajax({
        url: "/StoreClerk/ShowPurchasedetails",
        type: "post",
        dataType: "text",
        async: true,
        data: { purchaseID: PurchaseOrderID },

        success: function (data) {    
            $("#ShowPurchaseDetails").empty();
            $("#ShowPurchaseDetails").append("<thead><tr>"
                + "<th>Item Code</th>"
                + "<th>Description</th>"
                + "<th>Quantity</th>"
                + "<th>Price</th>"
                + "<th>Amount</th>"
                + "</tr></thead><tbody>");
            var json = JSON.parse(data);
            for (var i = 0; i < json.length; i++) {
                $("#ShowPurchaseDetails").append("<tr align='center'><td>" + json[i].itemID + "</td><td>"
                    + json[i].description + "</td><td>" + json[i].quantity + "</td><td>" + json[i].price +
                    "</td><td>" + json[i].amount + "</td></tr>");
            }
            $("#ShowPurchaseDetails").append("</tbody>");        
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
    var isUnfulfill = false;            
   
            for (var j = 0; j < objCheckBox.length; j++) {
                if (objCheckBox[j].checked) {

                    var status = rows[j + 1].cells[5].innerHTML;
                    var jsonObj = { "orderid": rows[j + 1].cells[2].innerHTML };
                    var statustrim = myTrim(status);
                    if (statustrim != "Unfulfill") 
                        {
                        // 
                        isUnfulfill = true;
                            
                        } 
                        else {
                           
                        purchaseIDList.push(jsonObj);
                     

                        }

                    }

    }
    if (!isUnfulfill) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            url: "/StoreClerk/UpdateQuantity",
            type: "post",
            dataType: "text",
            async: true,
            data: JSON.stringify(purchaseIDList),
            success: function () {
                alert("Update Successful!")
            }

        });
    }
    else {
        alert("Please choose unfulfill order to update!") ;
    }


   
}

function myTrim(x) {
   
    return x.replace(/^\s+|\s+$/gm, '');
}

      
    


