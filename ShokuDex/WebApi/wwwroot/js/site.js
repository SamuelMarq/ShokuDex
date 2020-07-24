

$(document).ready(function () {


    $('#sidebarCollapse').on('click', function () {

        var active = $('#sidebar').hasClass('active');
        if (active) document.cookie = 'sidebar=on; path=/';
        else document.cookie = 'sidebar=off; path=/';

        $('#sidebar').toggleClass('active');
        $('#content').toggleClass('active');
        $('.collapse.in').toggleClass('in');

        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

});

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie()
{
    var sidebar = getCookie("sidebar");
    if (sidebar == 'on') {
        $('#sidebar').removeClass('active');
        $('#content').removeClass('active');
    } else {
        $('#sidebar').addClass('active');
        $('#content').addClass('active');
        
    }
}



