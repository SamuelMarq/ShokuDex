

$(document).ready(function () {


    $('#sidebarCollapse').on('click', function () {
        
        $('#sidebar').toggleClass('active');

        $('.collapse.in').toggleClass('in');

        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

});



$(document).ready(function () {


    $('#sidebarCollapse').on('click', function () {

        $('#content').toggleClass('active');

        $('.collapse.in').toggleClass('in');

        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

});






