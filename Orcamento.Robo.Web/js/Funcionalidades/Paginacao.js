function GetMore(element, pagina) {

    $.ajax({
        url: "http://localhost:52485/Carga/MaisResultados", //+ window.location.host + url,
        type: "GET",
        data: { paginaAtual: pagina },
        cache: false,
        success: function (resultado) {
          //  $(element).parent().append(resultado);
            $("#bodyCarga").append(resultado);
            $("#ExibeMaisH4").append('<a class="accordion-toggle" onclick="GetMore(this, ' + (pagina + 1) + ')" data-toggle="collapse" data-parent=".accordion" href="#collapseOne">Exibir Mais</a>')
            $(element).hide();

            //  $(element).hide();
            //if (r.length > 10) {
            //    $(element).parent().prev().append(r);
            //} else {
            //    $(element).hide();
            //    $(element).parent().append('<br/><b style="font-size:12px;">Sem mais resultados</b><br/><br/>');
            //}
        }
    });
}