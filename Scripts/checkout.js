$(document).ready(function () {
    $('#btnCheckout').click(function () {
        let count = $('.ddlCosmetology').length;
        let result = [];

        if ($('#cosmetologySN').val() == '') {
            alert('請選擇美容項目');
            return;
        }

        for (let i = 0; i < count; i++) {
            let checkout = {
                cosmetologySN: '',
                price: ''
            };
            let price = $('#' + $('.price')[i].id).html();
            if (price != '') {
                checkout.cosmetologySN = $('#' + $('.form-control.ddlCosmetology')[i].id).val();
                checkout.price = price;
                result.push(checkout);
            }
        }        
        createCheckout(result);                
    });
});

let createCheckout = (result) => {
    let petSN = $('#petSN').html();    
    fetch('../Log/checkout?petSN=' + petSN + '&result=' + JSON.stringify(result),
        {
            method: 'POST'
        })
        .then((rs) => rs.text())
        .then((data) => {
            alert(data);            
            parent.$.fancybox.close();
        });
};

let addRow = () => {
    $('#billTable tbody').append($('#item').clone());
    $('.ddlCosmetology')[$('.ddlCosmetology').length - 1].id = 'cosmetologySN-' + ($('.ddlCosmetology').length - 1);
    $('.price')[$('.price').length - 1].id = 'price-' + ($('.price').length - 1);
    $('#' + $('.price')[$('.price').length - 1].id).html('');
}

let sumPrice = () => {
    let count = $('.ddlCosmetology').length;
    let sum = 0;
    for (let i = 0; i < count; i++) {
        let price = parseInt($('#' + $('.price')[i].id).html(), 10);
        if (!isNaN(price)) {
            sum += price;
        }        
    }

    $('#sumPrice').html(sum);
}