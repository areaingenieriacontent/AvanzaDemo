$(document).ready(function () {
    $('.menu li:has(ul)').click(function (e) {
        e.preventDefault();

        if ($(this).hasClass('activado')) {
            $(this).removeClass('activado');
            $(this).children('.subMenu').slideUp(500);
        } else {
            $('.menu li .subMenu').slideUp(1000);
            $('.menu li').removeClass('activado');
            $(this).addClass('activado');
            $(this).children('.subMenu').slideDown(500);
        }
    });

    $('.subMenu li:has(ul)').click(function (e) {
        e.preventDefault();

        if ($(this).hasClass('activado1')) {
            $(this).removeClass('activado1');
            $(this).children('ul').slideUp(500);
        } else {
            $('.subMenu li .subMenu1').slideUp(500);
            $('.subMenu li').removeClass('activado1');
            $(this).addClass('activado1');
            $(this).children('.subMenu1').slideDown(500);
        }
    });

    $('.subMenu1 li:has(.subMenu2)').click(function (e) {
        e.preventDefault();

        if ($(this).hasClass('activado2')) {
            $(this).removeClass('activado2');
            $(this).children('ul').slideUp(500);
        } else {
            $('.subMenu1 li .subMenu2').slideUp(500);
            $('.subMenu1 li').removeClass('activado2');
            $(this).addClass('activado2');
            $(this).children('.subMenu2').slideDown(500);
        }
    });

    $('.btn-menu').click(function () {
        $('.contenedor-menu .menu').slideToggle();
    });

    $(window).resize(function () {
        if ($(document).width() > 450) {
            $('.contenedor-menu .menu').css({ 'display': 'block' });
        }

        if ($(document).width() < 450) {
            $('.contenedor-menu .menu').css({ 'display': 'none' });
            $('.menu li ul').slideUp();
            $('.menu li').removeClass('activado');
        }
    });

    $('.menu li ul li a').click(function () {
        window.location.href = $(this).attr("href");
    });
});

/*Barra Progreso*/



