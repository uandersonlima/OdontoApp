
const d = document;
const containerLoading = d.querySelector('div._container_loading');
const messageInfo = Object.freeze({ in: 1, out: 2, pending: 3, sent: 4, delivered: 5, view: 6 });

const chat = {
    containerMessages: d.querySelector('div._chat_messages'),
    inputMessage: d.querySelector('input[id="mensagem"]'),
    sendButton: d.querySelector('button.message-input-icon')
}

const scrollToBottom = async () => {
    chat.containerMessages.scrollTo(0, chat.containerMessages.scrollHeight);
}


//MENSAGEM ENVIADOR
//melhorar com a resposta do Vzinho
const appendMessage = async () => {
    if (chat.inputMessage.value.length > 0) {
        const newMessage = await createMessage(messageType.out);
        newMessage.querySelector('p.m-3').textContent = chat.inputMessage.value;
        chat.containerMessages.appendChild(newMessage);
        const msg = chat.inputMessage.value;
        chat.inputMessage.value = "";
        chat.inputMessage.focus();
        scrollToBottom();
        const response = await sendMessageApi('6e713cc6-07f7-44e9-a559-3fdc3671d91b', msg);
        if (response.status == 200) {
            console.info('ok')
            console.info(await response.json())
        }
    }
}

//MESSAGEM RECEBEDOR
const appendNewMessage = async (msg = null) => {
    const newMessage = await createMessage(messageType.in);
    newMessage.querySelector('p.m-3').textContent = msg.description + msg.timeSent;
    chat.containerMessages.appendChild(newMessage);
    scrollToBottom();
}


chat.sendButton.addEventListener("click", appendMessage

);
chat.inputMessage.addEventListener("keydown", async function (event) {

    if (event.key === "Enter") {
        event.preventDefault();
        // Do more work
        appendMessage();
    }
}
);

const createMessage = async (messageType) => {
    const message = d.createElement('d');
    const 
}
const scrollToLoad = async () => {
    if (parseInt(chat.containerMessages.scrollTop) === 0) {
        containerLoading.classList.add('_flex');
        containerLoading.querySelector('._loading').classList.add('_loading_active');
        setTimeout(() => containerLoading.querySelector('._loading').classList.remove('_loading_active'), 3000);
        setTimeout(() => containerLoading.classList.remove('_flex'), 3300);
    }
}
chat.containerMessages.addEventListener('scroll', scrollToLoad)


const messageStatusIcon = async (status) => {
    if (status == messageInfo.pending) {
        return `<span class="">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 15" width="16" height="15">
                        <path fill="currentColor" d="M9.75 7.713H8.244V5.359a.5.5 0 0 0-.5-.5H7.65a.5.5 0 0 0-.5.5v2.947a.5.5 0 0 0 .5.5h.094l.003-.001.003.002h2a.5.5 0 0 0 .5-.5v-.094a.5.5 0 0 0-.5-.5zm0-5.263h-3.5c-1.82 0-3.3 1.48-3.3 3.3v3.5c0 1.82 1.48 3.3 3.3 3.3h3.5c1.82 0 3.3-1.48 3.3-3.3v-3.5c0-1.82-1.48-3.3-3.3-3.3zm2 6.8a2 2 0 0 1-2 2h-3.5a2 2 0 0 1-2-2v-3.5a2 2 0 0 1 2-2h3.5a2 2 0 0 1 2 2v3.5z"></path>
                    </svg>
                </span>`;
    }
    else if (status == messageInfo.sent) {
        return `<span class="">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 15" width="16" height="15">
                        <path fill="currentColor" d="M15.01 3.316l-.478-.372a.365.365 0 0 0-.51.063L8.666 9.879a.32.32 0 0 1-.484.033l-.358-.325a.319.319 0 0 0-.484.032l-.378.483a.418.418 0 0 0 .036.541l1.32 1.266c.143.14.361.125.484-.033l6.272-8.048a.366.366 0 0 0-.064-.512zm-4.1 0l-.478-.372a.365.365 0 0 0-.51.063L4.566 9.879a.32.32 0 0 1-.484.033L1.891 7.769a.366.366 0 0 0-.515.006l-.423.433a.364.364 0 0 0 .006.514l3.258 3.185c.143.14.361.125.484-.033l6.272-8.048a.365.365 0 0 0-.063-.51z"></path>
                    </svg>
                </span>`;
    }
    else if (status == messageInfo.delivered) {
        return `<span class="">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 15" width="16" height="15">
                        <path fill="currentColor" d="M10.91 3.316l-.478-.372a.365.365 0 0 0-.51.063L4.566 9.879a.32.32 0 0 1-.484.033L1.891 7.769a.366.366 0 0 0-.515.006l-.423.433a.364.364 0 0 0 .006.514l3.258 3.185c.143.14.361.125.484-.033l6.272-8.048a.365.365 0 0 0-.063-.51z"></path>
                    </svg>
                </span>`;
    }
}

function create_UUID() {
    var dt = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (dt + Math.random() * 16) % 16 | 0;
        dt = Math.floor(dt / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}