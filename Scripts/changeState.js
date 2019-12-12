let changeState = (SN) => {
    fetch('../Pet/changeState?SN=' + SN)
    .then((rs) => rs.text())
        .then((data => {            
            $('#btnState' + SN).val(data);            

            if ($('#btnState' + SN).attr('class') == 'btn btn-info') {
                $('#btnState' + SN).attr("class", 'btn btn-secondary');
            }
            else if ($('#btnState' + SN).attr('class') == 'btn btn-secondary'){
                $('#btnState' + SN).attr("class", 'btn btn-info');
            }
     }))
}

let removePetItem = (SN) => {
    fetch('Pet/changeState?SN=' + SN)
        .then((rs) => rs.text())
        .then((data => {
            $('#' + SN).remove();
        }))
    
}
