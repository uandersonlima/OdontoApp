document.addEventListener('DOMContentLoaded', function () {
    let calendar
    let eventos = []
    const urlGet = '/Agendas/GetEvents'
    let agendaEl = document.querySelector('div#agenda')

    $('.date-time').mask('00/00/0000 00:00:00')

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
        defaultView: '',
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
            //let eventoreal = eventos.find(dados => dados.id == element.event.id) //busca somente o element necessário
            console.log(element.event)
            let updateAgenda = {
                Id: Number(element.event.id),
                Titulo: element.event.title,
                Inicio: start,
                Fim: end,
                Descricao: element.event.extendedProps.description,
                Situacao: element.event.backgroundColor,
                DiaTodo: element.event.allDay,
                ClienteId: element.event.extendedProps.clienteid,
                PacienteId: element.event.extendedProps.pacienteid
            }
            UpdateAgendaObject(updateAgenda)
        },
        eventClick: function (element) {
            console.log(element)
            /*Limpa o form */
            document.querySelector('#modalAgenda #formAgenda').reset()
            /*-------------*/
            $('#modalAgenda').modal('show')
            document.querySelector('h4#TitleModal').textContent = 'Alterar Consulta'
            document.querySelector('#modalAgenda button.deleteEvent').style.display = 'flex'
            /*----------------------------Recupera valores dos campos------------------------------*/
            let id = element.event.id
            document.querySelector('#modalAgenda input[id="agendaId"]').value = id

            let clienteid = element.event.extendedProps.clienteid
            document.querySelector('#modalAgenda input[id="clienteId"]').value = clienteid

            let pacienteid = element.event.extendedProps.pacienteid
            document.querySelector('#modalAgenda select[id="NomePaciente"]').value = pacienteid

            let titulo = element.event.title
            document.querySelector('#modalAgenda input[id="titulo"]').value = titulo

            let inicio = moment(element.event.start).format("DD/MM/YYYY HH:mm:ss")
            document.querySelector('#modalAgenda input[id="inicio"]').value = inicio

            let fim = moment(element.event.end).format("DD/MM/YYYY HH:mm:ss")
            document.querySelector('#modalAgenda input[id="fim"]').value = fim

            let diatodo = element.event.allDay
            if (diatodo) {
                $('#divfim').hide()
            } else {
                $('#divfim').show()
            }

            document.querySelector('#modalAgenda input[id="diatodo"]').checked = diatodo
            let situacao = element.event.backgroundColor
            document.querySelector('#modalAgenda select[id="situacao"]').value = situacao
            let descricao = element.event.extendedProps.description
            document.querySelector('#modalAgenda textarea[id="descricao"]').value = descricao
            /*-------------------------------------------------------------------------------------*/

        },
        eventResize: function (element) {
            let inicio = moment(element.event.start).format("DD/MM/YYYY HH:mm:ss")
            let fim = moment(element.event.end).format("DD/MM/YYYY HH:mm:ss")
            //let eventoreal = eventos.find(dados => dados.id == element.event.id) //busca somente o element necessário
            let updateAgenda = {
                Id: Number(element.event.id),
                Titulo: element.event.title,
                Inicio: inicio,
                Fim: fim,
                Descricao: element.event.extendedProps.description,
                Situacao: element.event.backgroundColor,
                DiaTodo: element.event.allDay,
                ClienteId: element.event.extendedProps.clienteid,
                PacienteId: element.event.extendedProps.pacienteid
            }
            UpdateAgendaObject(updateAgenda)
        },
        select: function (element) {
            /*Limpa o form */
            document.querySelector('#modalAgenda #formAgenda').reset()
            /*-------------*/
            /*Exibe e edita o modal*/
            $('#modalAgenda').modal('show')
            $('#divfim').show()
            document.querySelector('h4#TitleModal').textContent = 'Adicionar Consulta'
            document.querySelector('#modalAgenda button.deleteEvent').style.display = 'none'
            /*-------------*/
            document.querySelector('#modalAgenda input[id="agendaId"]').value = ""
            let inicio = moment(element.start).format("DD/MM/YYYY HH:mm:ss")
            document.querySelector('#modalAgenda input[id="inicio"]').value = inicio
            let fim = moment(element.end).format("DD/MM/YYYY HH:mm:ss")
            document.querySelector('#modalAgenda input[id="fim"]').value = fim
            document.querySelector('#modalAgenda select[id="situacao"]').value = '#f0ad4e'
            calendar.unselect()
        },
        events: function (info, solution, failure) {
            /*info---contem-as-informações-referentes-ao-FullCalendar*/
            /*solution---eventos-resultantes-da-operacao-asyncrona*/
            /*failure---retorna-a-falha-referentes-ao-da-operacao*/
            /*remove classe indesejada*/
            eventos = []
            fetch(urlGet + `?inicio=${info.startStr}&fim=${info.endStr}`)
                .then(response => response.json())
                .then(dados => eventos = dados.map(dados => ({
                    id: dados.Id,
                    title: dados.Titulo,
                    description: dados.Descricao,
                    start: moment(dados.Inicio).format(),
                    end: moment(dados.Fim).format() != null ? moment(dados.Fim).format() : null,
                    color: dados.Situacao,
                    allDay: dados.DiaTodo,
                    clienteid: dados.ClienteId,
                    pacienteid: dados.PacienteId
                })))
                .then(eventos => solution(eventos))
                .catch(error => console.error('Unable to add item.', error))
            
        }
    })
    objCalendar = calendar
    calendar.render()
})
