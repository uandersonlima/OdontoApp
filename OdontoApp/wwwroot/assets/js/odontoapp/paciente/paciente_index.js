﻿let buscaHTML = $('input#buscarPaciente');
let registroPorPaginaHTML = $('select#RegistroPorPagina');
let url = '/Pacientes/ListaPacientes';
let numeroPaginaHTML = $('li.page-item.active');
let totalPagString = 'input[id="Pagination_TotalPages"]';
let totalRegString = 'input[id="Pagination_TotalRecords"]';
ConstructorUrl(url);
ConstructorAppView(buscaHTML, numeroPaginaHTML, registroPorPaginaHTML);
ConstructorPagination(totalPagString, totalRegString)

function Loadmodal() {
    $.ajax({
        type: 'GET',
        url: 'Pacientes/Create/',
        async: true,
        contentType: 'application/json',
        success: function (partial) {
            $('#modal').html(partial);
            $('#Tmodal').modal();
        },
        error: function (code) {
            $('#modal').html(`<div class="text-center"><h4>Algo deu errado, o erro ${code.status} aconteceu, se o problema persistir entre em contato</h4>
            <input type="button" class="btn btn-sm btn-outline-light text-muted font-weight-bold border-0" data-dismiss="modal" value="FECHAR" /></div>`);
            $('#Tmodal').modal();
        }
    })
}

$(".create").click(Loadmodal)

document.querySelectorAll("#carregarPaciente").forEach(el => el.onclick = function () {
    let pacienteId = this.getAttribute("data-id")
    window.location.href = `/Pacientes/PaginaPaciente/${pacienteId}`
})