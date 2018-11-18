var Index = {
    init: function (Controller) {
        Index.RegisterEvent(Controller);

    },
    RegisterEvent: function (Controller) {

        changes(Controller);
        function changes(Controller) {
            // event.preventDefault();
       
    
         
            $(".wrapper_Loader").show();
            //$(document).ajaxStart(function () {
            //    $("#loading").show();
            //});
            //$(document).ajaxComplete(function () {
            //    $("#loading").fadeOut("slow");
            //});
            //var Data_href = $(this).attr("href").split("/");
            $.ajax({
                url: "/Home/" + Controller,
                data: { },
                dataType: "html",
                type: "POST",
                error: function(xhr, status, error){
                    $('#page-wrapperichild').html("<strong><h2>ERROR</h2> <h1>Nội dung không tồn tại</h1>.......xin vui lòng back lại trang trước....</strong?");
                    $(".wrapper_Loader").fadeOut(400);
                  

                },
                success: function (response) {
                    $('.tab_change').html(response);
                    $(".wrapper_Loader").fadeOut(600);
                    homeController.init();
                    SettingController.init();
                    HistoryController.init();
                    if (Controller == 'Report')
                    ReportController.init();
                }


            })


        }


    }
}


$('.menubar li:first-child').addClass('active').addClass('current');
$('.menubar li').not('.menubar li:first-child').click(function () {
    $('#nav ul li').removeClass('active').removeClass('current');
    $(this).addClass('active').addClass('current');
    var id = $(this).find('a').attr("Controller");
    $('.tab-it').hide();

    Index.init(id);

});
$('.menubar li:first-child').click(function () {
    $('#nav ul li').removeClass('active').removeClass('current');
    $(this).addClass('active').addClass('current');
    $('.tab-it').show();
    $('.tab_change').empty();
})