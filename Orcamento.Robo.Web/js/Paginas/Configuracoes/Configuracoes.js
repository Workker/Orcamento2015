$(function () {
    /*--------------------------------------------------
	Plugin: Msg Box
	--------------------------------------------------*/

    $('.btn-danger').live('click', function (e) {
        $.msgbox("Você tem certeza que deseja deletar toda a estrutura orçamentaria?", {
            type: "confirm",
            buttons: [
              { type: "submit", value: "Sim" },
              { type: "cancel", value: "Cancelar" }
            ]
        }, function (result) {
            if (resul == "Sim") {
            }
        });
    });
});

function DeltetarEstrtutura() {

    $.ajax({
        url: "http://localhost:52485/Configuracoes/Deletar", //+ window.location.host + url,
        type: "POST",
        cache: false,
        success: function (resultado) {
            alert(resultado);
        }
    });
}