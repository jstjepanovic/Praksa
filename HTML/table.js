function SubmitFunction(){
    var tbl = document.getElementById("table1Id");
    var row = tbl.insertRow();
    var cell1 = row.insertCell();
    var cell2 = row.insertCell();
    var cell3 = row.insertCell();
    cell1.innerHTML = document.getElementById("fnId").value;
    cell2.innerHTML = document.getElementById("lnId").value;
    cell3.innerHTML = document.getElementById("unId").value;
}

