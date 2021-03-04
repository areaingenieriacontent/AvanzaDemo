document.oncontextmenu = function () {
    return false
}

function miFuncion() {

    alert("hola");
    var disabled = getElementByClassName("DisabledButton");

    disabled.document.hidden;
}



function right(e) {
    var msg = "Accion bloqueada";
    if (navigator.appName == 'Netscape' && e.which == 3) {
        // alert(msg); //- Si no quieres asustar a tu usuario entonces quita esta linea...
        return false;
    }
    else if (navigator.appName == 'Microsoft Internet Explorer' && event.button == 2) {
        // alert(msg); //- Si no quieres asustar al usuario que utiliza IE,  entonces quita esta linea...
        //- Aunque realmente se lo merezca...
        return false;
    }
    return true;
}
document.onmousedown = right;

window.onload = function () {
    $(window).ready(function () {
        $('h2').click(function () {
            if ($(this).next().hasClass('desplegado')) {
                $(this).next().removeClass('desplegado');
            } else {
                $(this).next().addClass('desplegado');
            }
        })
    })
    var myInput = document.getElementById('login');
    var myInput1 = document.getElementById('password');
    myInput.onpaste = function (e) {
        e.preventDefault();
        alert("esta acción está prohibida");
    }


    myInput1.onpaste = function (e) {
        e.preventDefault();
        alert("esta acción está prohibida");
    }

    myInput1.oncopy = function (e) {
        e.preventDefault();
        alert("esta acción está prohibida");
    }


}
jQuery('input[type=file]').change(function () {
    var filename = jQuery(this).val().split('\\').pop();
    var idname = jQuery(this).attr('id');
    console.log(jQuery(this));
    console.log(filename);
    console.log(idname);
    jQuery('span.' + idname).next().find('span').html(filename);
}); 
