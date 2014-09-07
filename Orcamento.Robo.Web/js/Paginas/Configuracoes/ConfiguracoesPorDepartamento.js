﻿function DeletarEstrutura() {

    $.msgbox("Você tem certeza que deseja deletar?", {
        type: "confirm",
        buttons: [
          { type: "submit", value: "Sim" },
          { type: "cancel", value: "Cancelar" }
        ]
    }, function (result) {
        if (result == "Sim") {

            Deletar();
        }
    });

}

function Deletar() {
    var idDepartamento = $("#IdDepartamento").val();
    $.ajax({
        url: "http://localhost:52485/ConfiguracoesPorDepartamento/Deletar", //+ window.location.host + url,
        type: "POST",
        cache: false,
        data: { departamentoId: idDepartamento },
        success: function (resultado) {
            // alert('Estrutura Deletada com sucesso!');
        }
    });
}