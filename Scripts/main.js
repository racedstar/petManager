$(document).ready(function () {
    $('[data-fancybox]').fancybox({
        afterClose: function () {
            history.go(0);
        },
        iframe: {
            css: {
                width: '1600px',
                height: '800px'
            }
        }

    });    
});