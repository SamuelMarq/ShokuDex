

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


$(document).ready(function () {
    $('#myTable').DataTable();
});


$(document).ready(function () {
    $('#foodTable').on('click', function () {
        location.href = 'Food_Table'
    });
});

$(document).ready(function () {
    $('#insertFood').on('click', function () {
        location.href = 'Food_Table/Insert_food'
    });
});

$(document).ready(function () {
    $('#foodDiary').on('click', function () {
        location.href = '/Food_Diary'
    });
});

