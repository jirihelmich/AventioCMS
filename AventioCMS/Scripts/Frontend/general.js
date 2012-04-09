$(function () {
    $(".carousel-item").click(function () {

        var w = $(".selected.carousel-item").width();
        console.log(w);
        var i = $(this);

        $(".selected.carousel-item").animate({ width: "32px" }, function () {
            $(".selected.carousel-item").removeClass("selected");
            $(i).animate({ width: w + "px" }, function () { $(i).addClass("selected"); });
        });
    });


    $(".carousel-content a:has(img)").hover(function () {
        var link = $(this);

        var src = $("img", link).attr('src');
        var bigSrc = src.replace("-small", "");

        $("img", link.parents(".right")).eq(0).attr("src", bigSrc);

        $("a", $(this).parents(".right")).removeClass("selected");
        link.addClass("selected");
    });

    var defaultSearchText = $("#search-text").val();
    $("#search-text").focus(function () {
        if ($("#search-text").val() == defaultSearchText) {
            $("#search-text").val('');
        }
    });
    $("#search-text").blur(function () {
        if ($("#search-text").val() == "") {
            $("#search-text").val(defaultSearchText);
        }
    });
    
    $("a:has(img)").fancybox({
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'speedIn': 600,
        'speedOut': 200,
        'overlayShow': false
    });
});