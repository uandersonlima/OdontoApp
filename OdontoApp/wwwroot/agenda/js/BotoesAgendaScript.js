/*---------------URLs-----------------*/
const urlAdd = '/Agendas/AddEvent'
const urlUpdate = '/Agendas/UpdateEvent'
const urlDelete = '/Agendas/DeleteEvent'

let formHTML = document.querySelector('form#formAgenda')
let formObject = {
    title: formHTML.querySelector('input[id="titulo"]'),
    deleteButton: formHTML.parentNode.parentNode.querySelector('button.deleteEvent'),
    agendaId: formHTML.querySelector('input[id="agendaId"]'),
    horaInicio: formHTML.querySelector('input[id="inicio"]'),
    horaFim: formHTML.querySelector('input[id="fim"]'),
    dia: formHTML.querySelector('input[id="dia"]'),
    situacao:  formHTML.querySelector('select[id="situacao"]'),
    pacienteId: formHTML.querySelector('input[id="pacienteId"]'),
    medicoId: formHTML.querySelector('input[id="medicoId"]'),
    usuarioId: formHTML.querySelector('input[id="usuarioId"]'),
    descricao: formHTML.querySelector('textarea[id="descricao"]'),
    span: formHTML.querySelector('span.circle')
}

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
                //fecha modal
                $('#modalAgenda').modal('hide')
                //atualiza eventos
                objCalendar.refetchEvents()
            }
        },
        error: function () {
            alert('Falha na hora de adicionar')
        }
    })
}
/*função responsável por deletar o dado no banco de dados*/
function DeleteAgendaObject(id, titulo) {
    if (confirm(`Tem certeza que deseja excluir ${titulo}?`))
    $.ajax({
        type: "POST",
        url: urlDelete,
        data: {
            __RequestVerificationToken: token,
            id: id
        },
        success: function (dados) {
            if (dados.status) {
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
        alert('Título necessário')
        return
    }
    if ($('#dia').val().trim() == "") {
        alert('dia necessário')
        return
    }    
    if ($('#inicio').val().trim() == "") {
        alert('hora inicial necessária')
        return
    }
    if ($('#fim').val().trim() == "") {
        alert('hora final necessária')
        return
    }
    else {
        let inicio = moment(formObject.horaInicio.value, "DD/MM/YYYY HH:mm:ss").format("YYYY/MM/DD HH:mm:ss")
        let fim = moment(formObject.horaFim.value, "DD/MM/YYYY HH:mm:ss").format("YYYY/MM/DD HH:mm:ss")
        if (inicio > fim) {
            alert('horário final não pode ser menor que o horário inicial');
            return
        }
    }

    
    let inicio = new Date(formObject.dia.value + " " + formObject.horaInicio.value)
    let fim = new Date(formObject.dia.value + " " + formObject.horaFim.value)

    
    let agenda = {
        AgendaId: formObject.agendaId.value,
        Titulo: formObject.title.value,
        Descricao: formObject.descricao.value,
        Inicio: inicio.toJSON(),
        Fim: fim.toJSON(),
        Realizado: false,
        Situacao: formObject.situacao.value,
        UsuarioId: formObject.usuarioId.value,
        PacienteId: formObject.pacienteId.value,
        MedicoId: formObject.medicoId.value
    }

    if (agenda.AgendaId == '') {
        AddAgendaObject(agenda)
    } else {
        UpdateAgendaObject(agenda)
    }
}
/*--função acionada no click apagar--*/
formObject.deleteButton.onclick = function () {
    let id = document.querySelector('#modalAgenda input[id="agendaId"]').value
    let titulo = document.querySelector('#modalAgenda input[id="titulo"]').value
    DeleteAgendaObject(Number(id), titulo)
}

let inputPaciente = $('input#Paciente_NomePaciente')
let inputPacienteId = $('input#pacienteId')
let loadingPaciente = document.querySelector('div#loadPaciente.autocomplete-feedback')
inputPaciente.autocomplete({
        autoFocus: false,
        source: (request, response) => {                   
            fetch('/pacientes/autocomplete?query=' + request.term)
            .then(resp => resp.json())
            .then(dados => {                       
                    if(!dados.length){
                        var result = [
                            {
                                label: 'Nenhum resultado encontrado', 
                                value: ''
                            }
                        ];
                        response(result);
                    }
                    else{
                        // normal response
                        response($.map(dados, item => {
                            return {
                                label: item.nomePaciente,
                                nomePaciente: item.nomePaciente,
                                pacienteId: item.pacienteId,
                            }
                        }));
                    }
            })                  
        },
        search: () => {
            loadingPaciente.style.display = 'flex';
        },
        response: () => {
            loadingPaciente.style.display = 'none';                      
        },
        select: (event, ui) => {
            inputPaciente.val(ui.item.nomePaciente);
            inputPacienteId.val(ui.item.pacienteId);
            event.preventDefault();
        },
        focus: (event, ui) => {
            inputPaciente.val(ui.item.nomePaciente);
            inputPacienteId.val(ui.item.pacienteId);
            event.preventDefault();
        },
})
let inputMedico = $('input#Medico_NomeMedico')
let inputMedicoId = $('input#medicoId')
let loadingMedico = document.querySelector('div#loadMedico.autocomplete-feedback')
inputMedico.autocomplete({
        autoFocus: false,
        source: (request, response) => {                   
            fetch('/medicos/autocomplete?query=' + request.term)
            .then(resp => resp.json())
            .then(dados => {                       
                    if(!dados.length){
                        var result = [
                            {
                                label: 'Nenhum resultado encontrado', 
                                value: ''
                            }
                        ];
                        response(result);
                    }
                    else{
                        // normal response
                        response($.map(dados, item => {
                            return {
                                label: item.nomeMedico,
                                nomeMedico: item.nomeMedico,
                                medicoId: item.medicoId,
                            }
                        }));
                    }
            })                  
        },
        search: () => {
            loadingMedico.style.display = 'flex';
        },
        response: () => {
            loadingMedico.style.display = 'none';                      
        },
        select: (event, ui) => {
            inputMedico.val(ui.item.nomeMedico);
            inputMedicoId.val(ui.item.medicoId);
            event.preventDefault();
        },
        focus: (event, ui) => {
            inputMedico.val(ui.item.nomeMedico);
            inputMedicoId.val(ui.item.medicoId);
            event.preventDefault();
        },
})