function sonumero() {

    $('.sonumero').focus(function () {

        this.value = this.value.replace(/\%|\,00|\+/g, '');

        var nosTotalizadores = $(this).parent().parent().children(":last").get(0).children;
        nosTotalizadores[1].value = this.value;

        this.select();
    });

    $(".sonumero").keyup(function (event) {
        //InsereMascaraDeMilhar(this);

        if (event.keyCode == 9) {
            this.select();
        }
    });

    $('.sonumero').blur(function () {

        var nosTotalizadores = $(this).parent().parent().children(":last").get(0).children;


        if (VerificarSeOCampoPossuiCaracteresInvalidosParaCalculo(this.value)) {

            var media = roundNumber((ObterASomaDeTodosOsCamposDeUmaLinhaDeUmaTabela(this) / 12), 2);

            if (media.toString().indexOf(".") > 0) {

                media = media.toString().split(".");

                if (media[0].indexOf("-") >= 0) {

                    media = "-" + AplicarMascaraDeMilhar(media[0]) + "," + media[1];
                }
                else {

                    media = AplicarMascaraDeMilhar(media[0]) + "," + media[1];
                }
            }
            else {
                media = AplicarMascaraDeMilhar(media);
            }

            if (event.srcElement.parentNode.parentNode.parentNode.parentNode.id == "complexidade") {

                if (RemoverTodosOsCaracteresNaoNumericos(this.value) > 100) {

                    this.value = "0%";
                    media = 0;
                }
                else {
                    this.value += "%";
                }

                media = media + "%";
                
            }

            if (this.value == "") {
                this.value = 0;
                media = 0;
            }
        }
        else {

            this.value = 0;
            media = 0;
        }

        //this.value = this.value + "%";
        nosTotalizadores[0].innerText = media;
    });
}

function VerificarSeOCampoPossuiCaracteresInvalidosParaCalculo(valor) {

    if (valor == "") {
        return true;
    }
    else {
        var valorSemMascara = RemoverTodosOsCaracteresNaoNumericos(valor);

        if (valorSemMascara == "") {

            alert("Algum valor dos campos é inválido. Por favor verifique.");
            return false;
        }
        else {
            return true;
        }
    }
}

function CalcularOQuantoFoiGastoComOPagamentoDeContasEmUmMes(object) {

    var numeroAtual = object.value;

    var numeroAnterior = object.parentNode.parentNode.lastChild.lastChild.previousSibling.value;

    if (numeroAtual != numeroAnterior) {

        var total = (object.parentNode.parentNode.lastChild.firstChild.firstChild.data.replace(/\,00|\+/g, '') * 1);

        total += ((numeroAtual * 1) - numeroAnterior);

        object.parentNode.parentNode.lastChild.firstChild.firstChild.data = "(" + total + ",00)";
    }
}


function ObterASomaDeTodosOsCamposDeUmaLinhaDeUmaTabela(campo) {

    var total = 0;

    $(campo).closest('tr').find("input.sonumero").each(function () {

        if ($(this).val().indexOf("-") >= 0) {

            total += parseInt("-" + RemoverTodosOsCaracteresNaoNumericos($(this).val()));
        }
        else {
            
            total += parseInt(RemoverTodosOsCaracteresNaoNumericos($(this).val()));
        }
    });

    return total.toString();
}

function roundNumber(num, dec) {
    var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
    return result;
}


function PermitirSomenteADigitacaoDeNumerosComCopiarEColar(event) {

    //pode: 188 - virgula
    //      109 - sinal de menos
    //      67  - C
    //      86  - V
    //110
    //194
    
    //event.srcElement.parentNode.parentNode.parentNode.parentNode

    // Allow: backspace, delete, tab, escape, and enter                                                                  // Allow: Ctrl+A                                   // Allow: home, end, left, right
    if ((event.keyCode == 67 && event.ctrlKey === true) || (event.keyCode == 86 && event.ctrlKey === true) || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
        return;
    } else {
        // Ensure that it is a number and stop the keypress
        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
            event.preventDefault ? event.preventDefault() : event.returnValue = false;
        }
    }
}

function PermitirSomenteADigitacaoDeNumerosComVirgulaESinalNegativo(event) {

    //pode: 188 - virgula
    //      109 - sinal de menos
    //      67  - C
    //      86  - V
    //110
    //194

    // Allow: backspace, delete, tab, escape, and enter                                                                  // Allow: Ctrl+A                                   // Allow: home, end, left, right
    if (event.keyCode == 189 || event.keyCode == 110 || event.keyCode == 194 || (event.keyCode == 67 && event.ctrlKey === true) || (event.keyCode == 86 && event.ctrlKey === true) || event.keyCode == 109 || event.keyCode == 188 || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {

        if (event.srcElement.parentNode.parentNode.parentNode.parentNode.id != "complexidade" && (event.keyCode == 109 || event.ketCode == 189)) {
            event.preventDefault ? event.preventDefault() : event.returnValue = false;
        }
        else {
            return;
        }
    } else {
        // Ensure that it is a number and stop the keypress
        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
            event.preventDefault ? event.preventDefault() : event.returnValue = false;
        }
    }
}