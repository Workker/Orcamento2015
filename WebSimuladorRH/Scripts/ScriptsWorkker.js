
function IniciarModal(document) {

    $('a[name=modal]').click(function (e) {

        e.preventDefault();

        var id = $(this).attr('href');

        //---test area---

        $("#idImagemMemoriaDeCalculo").val($(this).parent().find("img").get(0).id);

        //---keep out----

        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
        $('#mask').fadeIn(1000);
        $('#mask').fadeTo("slow", 0.8);

        var winH = $(window).height();
        var winW = $(window).width();

        $(id).css('top', winH / 2 - $(id).height() / 2);
        $(id).css('left', winW / 2 - $(id).width() / 2);
        $(id).fadeIn(2000);
    });

    $('.window .close').click(function (e) {
        e.preventDefault();
        $('#mask').hide();
        $('.window').hide();
    });

    $('#mask').click(function () {
        $(this).hide();
        $('.window').hide();
    });

    $(window).resize(function () {

        var box = $('#boxes .window');
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

        var winH = $(window).height();
        var winW = $(window).width();

        box.css('top', winH / 2 - box.height() / 2);
        box.css('left', winW / 2 - box.width() / 2);
    });
}

function PermitirSomenteADigitacaoDeNumerosComVirgula(event) {

    // Allow: backspace, delete, tab, escape, and enter                                                                  // Allow: Ctrl+A                                   // Allow: home, end, left, right
    if (event.keyCode == 188 || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
        return;
    } else {
        // Ensure that it is a number and stop the keypress
        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
            event.preventDefault();
        }
    }
}

function PermitirSomenteADigitacaoDeNumeros(event) {

    // Allow: backspace, delete, tab, escape, and enter                                                                  // Allow: Ctrl+A                                   // Allow: home, end, left, right
    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
        return;
    } else {
        // Ensure that it is a number and stop the keypress
        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
            event.preventDefault ? event.preventDefault() : event.returnValue = false;
        }
    }
}

function AplicarMascaraDePorcentagemNoValorDoCampo(campo) {

    if(campo.value > 100) {

        campo.value = 0;
    }

    campo.value += "%";
}

function RemoverTodosOsCaracteresNaoNumericos(valor) {

    return valor.toString().replace(/[^\d]/g, "");
}

function VerificarSeOCampoEstaVazio(campo) {
    if (campo.value == "")
        return false;
    else
        return true;
}

function AplicarMascaraDeMilhar(valor) {


    var contador = 0;
    var numeroComMascaraDeMilhar = "";
    valor = RemoverTodosOsCaracteresNaoNumericos(valor); //.toString();


        for (var i = valor.length; i > 0; i--) {

            if ((contador != 0) && (contador % 3 === 0)) {
                numeroComMascaraDeMilhar = valor.charAt(i - 1) + "." + numeroComMascaraDeMilhar;
            }
            else {
                numeroComMascaraDeMilhar = valor.charAt(i - 1) + numeroComMascaraDeMilhar;
            }

            contador++;
        }
    
    return numeroComMascaraDeMilhar;
}

function InsereMascaraDeMilhar(object) {

    var x = 0;
    var num = RemoverTodosOsCaracteresNaoNumericos(object.value);

    if (num.length == object.maxLength) {
        num = num.substring(0, (num.length - 2));
    }

    if (num < 0) {
        num = Math.abs(num);
        x = 1;
    }

    if (isNaN(num)) num = "";

    num = Math.floor((num * 100 + 0.5) / 100).toString();

    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)

        num = num.substring(0, num.length - (4 * i + 3)) + '.'
                    + num.substring(num.length - (4 * i + 3));
    var ret = num;

    if (x == 1) ret = ' - ' + ret;

    if (object.value == ""){
        ret = "";
    }

    object.value = ret;
}

function ValidarData(valor) {
    
    regularExpression = /^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;

    var resultado = regularExpression.test(valor);

    return resultado;
}