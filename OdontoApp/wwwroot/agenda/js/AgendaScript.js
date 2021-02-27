document.addEventListener('DOMContentLoaded', function () {
    let calendar
    const urlGet = '/Agendas/GetEvents'
    let agendaEl = document.querySelector('div#agenda')
    let formHTML = document.querySelector('form#formAgenda')
    let formObject = {
        title: formHTML.querySelector('input[id="titulo"]'),
        modal_Title: formHTML.parentNode.parentNode.querySelector('h4#TitleModal'),
        deleteButton: formHTML.parentNode.parentNode.querySelector('button.deleteEvent'),
        agendaId: formHTML.querySelector('input[id="agendaId"]'),
        horaInicio: formHTML.querySelector('input[id="inicio"]'),
        horaFim: formHTML.querySelector('input[id="fim"]'),
        dia: formHTML.querySelector('input[id="dia"]'),
        situacao:  formHTML.querySelector('select[id="situacao"]'),
        pacienteId: formHTML.querySelector('input[id="pacienteId"]'),
        medicoId: formHTML.querySelector('input[id="medicoId"]'),
        nomePaciente: formHTML.querySelector('input[id="Paciente_NomePaciente"]'),
        nomeMedico: formHTML.querySelector('input[id="Medico_NomeMedico"]'),
        usuarioId: formHTML.querySelector('input[id="usuarioId"]'),
        descricao: formHTML.querySelector('textarea[id="descricao"]'),
        span: formHTML.querySelector('span.circle')
    }

    $('.date-time').mask('00:00')

    /* Inicia o calendário e Tema
    -----------------------------------------------------------------*/
    calendar = new FullCalendar.Calendar(agendaEl, {
        plugins: ['bootstrap', 'interaction', 'dayGrid', 'timeGrid', 'list'],
        themeSystem: 'bootstrap',
        header: {
            left: 'title',
            center: 'dayGridMonth,timeGridWeek,timeGridDay',
            right: 'prev,next, today '
        },
        //muda texto dos botões
        buttonText: {
            next: 'Próximo',
            prev: 'Anterior'

        },
        //muda tema dos botões
        bootstrapFontAwesome: {
            close: 'fa-times',
            prev: ' fa-chevron-left',
            next: 'fa-chevron-right',
            prevYear: 'fa-angle-double-left',
            nextYear: 'fa-angle-double-right'
        },
        defaultView: 'timeGridWeek',
        eventColor: '#0275d8',
        locale: 'pt',
        weekNumbers: false,
        navLinks: true,
        eventLimit: true,
        selectable: true,
        editable: true,
        droppable: true,
        eventDrop: function (element) {
            let start = moment(element.event.start).format("DD/MM/YYYY HH:mm:ss")
            let end = moment(element.event.end).format("DD/MM/YYYY HH:mm:ss")
    
            let updateAgenda = {
                AgendaId: Number(element.event.id),
                Titulo: element.event.title,
                Inicio: start,
                Fim: end,
                Descricao: element.event.extendedProps.description,
                Situacao: element.event.backgroundColor,
                UsuarioId: element.event.extendedProps.usuarioId,
                PacienteId: element.event.extendedProps.pacienteId,
                MedicoId: element.event.extendedProps.medicoId,
                Realizado: element.event.extendedProps.realizado,
            }
            UpdateAgendaObject(updateAgenda)
        },
        eventClick: function (element) {
            /*Limpa o form */
            formHTML.reset()
            /*-------------*/
            $('#modalAgenda').modal('show')
            formObject.modal_Title.textContent = 'Alterar Consulta'
            formObject.deleteButton.style.display = 'flex'
            /*----------------------------Recupera valores dos campos------------------------------*/
            formObject.agendaId.value = element.event.extendedProps.agendaId
            formObject.usuarioId.value = element.event.extendedProps.usuarioId
            formObject.pacienteId.value = element.event.extendedProps.pacienteId
            formObject.medicoId.value = element.event.extendedProps.medicoId
            formObject.nomePaciente.value = element.event.extendedProps.nomePaciente
            formObject.nomeMedico.value = element.event.extendedProps.nomeMedico
            formObject.title.value = element.event.title
            formObject.dia.value = moment(element.event.start).format("YYYY-MM-DD")
            formObject.horaInicio.value = moment(element.event.start).format("HH:mm");
            formObject.horaFim.value = moment(element.event.end).format("HH:mm")
            formObject.situacao.value = element.event.backgroundColor
            formObject.span.style.backgroundColor = element.event.backgroundColor
            formObject.descricao.value = element.event.extendedProps.description
            /*-------------------------------------------------------------------------------------*/

        },
        eventResize: function (element) {
            let inicio = moment(element.event.start).format("DD/MM/YYYY HH:mm:ss")
            let fim = moment(element.event.end).format("DD/MM/YYYY HH:mm:ss")
            //let eventoreal = eventos.find(dados => dados.id == element.event.id) //busca somente o element necessário
            let updateAgenda = {
                AgendaId: Number(element.event.id),
                Titulo: element.event.title,
                Inicio: inicio,
                Fim: fim,
                Descricao: element.event.extendedProps.description,
                Situacao: element.event.backgroundColor,
                UsuarioId: element.event.extendedProps.usuarioId,
                PacienteId: element.event.extendedProps.pacienteId,
                MedicoId: element.event.extendedProps.medicoId,
                Realizado: element.event.extendedProps.realizado,
            }
            UpdateAgendaObject(updateAgenda)
        },
        select: function (element) {
            /*Limpa o form */
            formHTML.reset()
            /*-------------*/
            /*Exibe e edita o modal*/
            $('#modalAgenda').modal('show')
       
            formObject.modal_Title.textContent = 'Adicionar Consulta'
            formObject.deleteButton.style.display = 'none'
            /*-------------*/
            formObject.agendaId.value = ""

            formObject.dia.value = moment(element.start).format("YYYY-MM-DD")
            formObject.horaInicio.value = moment(element.start).format("HH:mm");
            formObject.horaFim.value = moment(element.end).format("HH:mm")

            formObject.situacao.value = '#f0ad4e'
            formObject.span.style.backgroundColor = '#f0ad4e'

            calendar.unselect()
        },
        events: function (info, solution, failure) {
            /*info---contem-as-informações-referentes-ao-FullCalendar*/
            /*solution---eventos-resultantes-da-operacao-asyncrona*/
            /*failure---retorna-a-falha-referentes-ao-da-operacao*/
            /*remove classe indesejada*/
            fetch(urlGet + `?inicio=${info.startStr}&fim=${info.endStr}`)
                .then(response => response.json())
                .then(dados => eventos = dados.map(dados => ({
                    id:dados.AgendaId,
                    agendaId: dados.AgendaId,
                    title: dados.Titulo,
                    description: dados.Descricao,
                    start: moment(dados.Inicio).format(),
                    end: moment(dados.Fim).format(),
                    color: dados.Situacao,
                    realizado: dados.Realizado,
                    usuarioId: dados.UsuarioId,
                    pacienteId: dados.PacienteId,
                    medicoId: dados.MedicoId,
                    nomeMedico: dados.Medico != null ? dados.Medico.NomeMedico : "",
                    nomePaciente: dados.Paciente != null ? dados.Paciente.NomePaciente : "",
                })))
                .then(eventos => solution(eventos))
                .catch(error => console.error('Unable to add item.', error))
            
        }
    })
    objCalendar = calendar
    calendar.render()
})
