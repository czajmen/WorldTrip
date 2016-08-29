

(function () {

   
    //var ele = $("#username");
    //ele.text("czajmen");

    //var main = $("main");

    //main.on("mouseenter" , function () {
    //    main.style.backgroundColor = "#888";
    //});
    //main.on("mouseleave" , function () {
    //    main.style.backgroundColor = "";
    //});

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    var me = $(this);

    //    alert(me.text());
    //});


    var $sideBarAndWrapper = $("#sidebar , #wrapper");
    var $icon = $("#sideBarToggle i.fa");

    $("#sideBarToggle").on("click", function () {
        $sideBarAndWrapper.toggleClass("hide-sidebar");

        if($sideBarAndWrapper.hasClass("hide-sidebar"))
        {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        }
        else {
            $icon.removeClass("fa-angle-right");
            $icon.addClass("fa-angle-left");
        
        }

    });
})();

