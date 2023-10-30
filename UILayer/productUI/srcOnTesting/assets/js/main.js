/*!
 * Author:Chandradhar S G
 * Bangalore,India
 * Date: 3-2-2017
 */

$(document).ready(function () {
    /*----------------------------Dynamic theme color change -------------------------------*/
    var c;
    var c2;
    $("#default_1").click(function () {
        $(".navbar-default").css("background", "#0bb7db");
        $(".navbar-default .navbar-nav > li > a").css("color", "white");
        localStorage.setItem("themeColor", "#0bb7db");
        localStorage.setItem("txtColor", "white");
        localStorage.setItem("eCommerce", "white");
        location.reload();
    });
    $("#default_2").click(function () {
        $(".navbar-default").css("background", "#3B5999");
        $(".navbar-default .navbar-nav > li > a").css("color", "white");
        localStorage.setItem("themeColor", "#3B5999");
        localStorage.setItem("txtColor", "white");
        localStorage.setItem("eCommerce", "white");
        location.reload();
    });
    $("#default_3").click(function () {
        $(".navbar-default").css("background", "red");
        $(".navbar-default .navbar-nav > li > a").css("color", "white");
        localStorage.setItem("themeColor", "red");
        localStorage.setItem("txtColor", "white");
        localStorage.setItem("eCommerce", "white");
        location.reload();
    });
    $("#pickColorTheme").click(function () {
        c = $("#pickColorTheme").val();
        //alert(c);
        localStorage.setItem("selectThemeColor", c);
        $("#pickColorTheme").val(localStorage.selectThemeColor);
    });

    $("#apply").click(function () {
        c2 = $("#pickColorTheme").val();
        alert(c2);
        localStorage.setItem("themeColor", c2);
        c = localStorage.themeColor;
        location.reload();

    });
    $("#default_1").css("background", "#0bb7db");
    $("#default_2").css("background", "#3B5999");
    $("#default_3").css("background", "red");
    $(".navbar-default").css({ "background-image": "url(images/header-bg.png)", "background-color": localStorage.themeColor });
    $(".navbar-default .navbar-nav > li > a").css("color", localStorage.txtColor);
    $(".navbar-default .navbar-nav > .active > a").css("color", "gray");
    $(".navbar-default .navbar-brand").css({ "color": localStorage.eCommerce, "font-weight": "bold", "font-family": "cursive" });
    $("#pickColorTheme").val(localStorage.themeColor);

    $(".settings").mouseover(function () {
        $(".settings").rotate({ animateTo: 360, duration: 1500 });
    });
    $(".settings").click(function () {
        $("#mySidenav").css("width", "250px");
        $(".settings").css("display", "none");
        $(".closebtn1").css("display", "block");
        $(".sidenav a").css("display", "block");
    });
    $(".closebtn1").click(function () {
        $(".closebtn1").css("display", "none");
        $("#mySidenav").css("width", "0px");
        $(".settings").css("display", "block");
        $(".sidenav a").css("display", "none");
    });

    /* $(".settings").click(function () {
    $("#mySidenav").toggle(100, function () {
    $("#mySidenav").css("width", "250px");    
    });
    });*/
    /*----------------------------fetchcing image in pop up -------------------------------*/
    $(".dress").click(function () {
        //alert($(this).attr("src"));
        var targetId = $(this).attr("src");
        $(".dressimg img").attr({ "src": targetId, "data-origin": targetId });
        $(this).attr({ "data-target": "#myModal", "data-toggle": "modal" });
    });

});