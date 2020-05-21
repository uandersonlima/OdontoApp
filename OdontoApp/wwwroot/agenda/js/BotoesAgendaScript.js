/*---------------URLs-----------------*/
const urlAdd = '/Agendas/AddEvent'
const urlUpdate = '/Agendas/UpdateEvent'
const urlDelete = '/Agendas/DeleteEvent'

/*função responsável por atualizar o dado no banco de dados*/
function UpdateAgendaObject(data) {
    $.ajax({
        type: "PUT",
        url: urlUpdate,
        data: {
            __RequestVerificationToken: token,
            Agenda: data
        },
        success: function (dados) {
            if (dados.status) {
                objCalendar.refetchEvents()
                //Refresh the calender
                console.log('tudo certo graças a Deus')
                //fecha modal
                $('#modalAgenda').modal('hide')
                //atualiza eventos
                objCalendar.refetchEvents()
            }
        },
        error: function () {
            alert('falha na hora de atualizar')
        }
    })
}
/*função responsável por Adicionar o dado no banco de dados*/
function AddAgendaObject(data) {
    $.ajax({
        type: "POST",
        url: urlAdd,
        data: {
            __RequestVerificationToken: token,
            Agenda: data
        },
        success: function (dados) {
            if (dados.status) {
                objCalendar.refetchEvents()
                console.log("Adicionado tranquilamente")
                //fecha modal
                $('#modalAgenda').modal('hide')
                //atualiza eventos
                objCalendar.refetchEvents()
            }
        },
        error: function () {
            alert('falha na hora de adicionar')
        }
    })
}
/*função responsável por deletar o dado no banco de dados*/
function DeleteAgendaObject(id, titulo) {
    if (confirm(`Tem certeza que deseja excluir ${titulo}?`))
        console.log(id)
    $.ajax({
        type: "POST",
        url: urlDelete,
        data: {
            __RequestVerificationToken: token,
            id: id
        },
        success: function (dados) {
            if (dados.status) {
                console.log("Excluiu, safe")
                //fecha modal
                $('#modalAgenda').modal('hide')
                //atualiza eventos
                objCalendar.refetchEvents()
            }
        },
        error: function () {
            alert('falha na hora de deletar')
        }
    })
}
$('#diatodo').change(function verifica() {
    if ($(this).is(':checked')) {
        $('#divfim').hide()
    }
    else {
        $('#divfim').show()
    }
})
/*--função acionada no click salvar--*/
document.querySelector('.saveEvent').onclick = function () {
    //Validation/
    if ($('#titulo').val().trim() == "") {
        alert('Titulo necessário')
        return
    }
    if ($('#inicio').val().trim() == "") {
        alert('Data inicial necessária')
        return
    }
    if ($('#diatodo').is(':checked') == false && $('#fim').val().trim() == "") {
        alert('Data final necessária')
        return
    }
    else {
        let inicio = moment(document.querySelector('#modalAgenda input[id="inicio"]').value, "DD/MM/YYYY HH:mm:ss").format("YYYY/MM/DD HH:mm:ss")
        let fim = moment(document.querySelector('#modalAgenda input[id="fim"]').value, "DD/MM/YYYY HH:mm:ss").format("YYYY/MM/DD HH:mm:ss")
        if (inicio > fim) {
            alert('Data final não pode ser menor que a Data inicial');
            return
        }
    }

    let id = document.querySelector('#modalAgenda input[id="agendaId"]').value
    let clienteid = document.querySelector('#modalAgenda input[id="clienteId"]').value
    let pacienteid = document.querySelector('#modalAgenda select[id="NomePaciente"]').value
    let titulo = document.querySelector('#modalAgenda input[id="titulo"]').value
    let inicio = moment(document.querySelector('#modalAgenda input[id="inicio"]').value, "DD/MM/YYYY HH:mm:ss").format("YYYY/MM/DD HH:mm:ss")
    let fim = moment(document.querySelector('#modalAgenda input[id="fim"]').value, "DD/MM/YYYY HH:mm:ss").format("YYYY/MM/DD HH:mm:ss")
    let situacao = document.querySelector('#modalAgenda select[id="situacao"]').value
    let descricao = document.querySelector('#modalAgenda textarea[id="descricao"]').value
    let diatodo = document.querySelector('#modalAgenda input[id="diatodo"]').checked
    let agenda = {
        Id: id,
        Titulo: titulo,
        Descricao: descricao,
        Inicio: inicio,
        Fim: fim,
        DiaTodo: diatodo,
        Situacao: situacao,
        ClienteId: clienteid,
        PacienteId: pacienteid
    }
    console.log(agenda)
    if (id == '') {
        AddAgendaObject(agenda)
    } else {
        UpdateAgendaObject(agenda)
    }
}
/*--função acionada no click apagar--*/
document.querySelector('.deleteEvent').onclick = function () {
    console.log('apagou')
    let id = document.querySelector('#modalAgenda input[id="agendaId"]').value
    console.log(id)
    let titulo = document.querySelector('#modalAgenda input[id="titulo"]').value
    DeleteAgendaObject(Number(id), titulo)
}


