
(function () {

    var burgerMenuOpen = false;
    var count = 1;

    $(".burgerMenu").on("click", function (ev) {
        ev.preventDefault();
        $(this).toggleClass("animateBurger");
        $("nav").slideToggle("fast");
        count++;
        if (count % 2 == 0) {
            burgerMenuOpen = true;
        } else {
            burgerMenuOpen = false;
        }
    });

    $(window).on("resize", function (ev) {
        if (window.innerWidth > 720) {
            $("nav").attr("style", "display: block");
            if (burgerMenuOpen) {
                $(".burgerMenu").toggleClass("animateBurger");
                burgerMenuOpen = false;
            }
            
        }
    });

    $(window).on("resize", function (ev) {
        if (window.innerWidth < 720) {
            if (!burgerMenuOpen) {
                $("nav").attr("style", "display: none");
            }
        }
    });

})();