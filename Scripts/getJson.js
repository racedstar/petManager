let getVariety = () => {
    var kindSN = $('#kindSN').val();

    if ($('#kindSN').val() == '') {
        kindSN = 0;
    }

    $('#varietySN').empty();

    fetch('../Variety/getVarietyList?kindSN=' + kindSN)
        .then((rs) => rs.json())
        .then((data => {
            $.each(data, function (key, variety) {
                $('#varietySN').append($('<option></option>').attr('value', variety.SN).text(variety.varietyName));
            });
        }));
};

let getCosmetologyPrice = (myObj) => {
    let SN = $(myObj).val();    
    
    if (SN != '') {
        fetch('../Cosmetology/getCosmetologyPrice?SN=' + SN)
            .then((rs) => rs.text())
            .then((data => {
                if (myObj.id.split('-')[1] != null) {                    
                    $('#price-' + myObj.id.split('-')[1]).html(data);                    
                }
                else {
                    $('#price').html(data);                                       
                }

                sumPrice();
            }));
    }
};

let customerSearch = () => {
    let keyWord = $('#txtKeyWord').val();

    fetch('Index?keyWord=' + keyWord, {
        method: 'POST'
    });
};
