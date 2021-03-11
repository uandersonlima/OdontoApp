const chat = {
    boxMessage: document.querySelector('div.chat'),
    ballonMessage1: document.querySelector('div.balao1'),
    ballonMessage2: document.querySelector('div.balao2'),
    inputMessage: document.querySelector('input[id="mensagem"]'),
    sendButton: document.querySelector('button#butaozinho')
}

const scrollToBottom = () => {
    let shouldScroll = chat.boxMessage.scrollTop + chat.boxMessage.clientHeight === chat.boxMessage.scrollHeight;
    if (!shouldScroll)
        chat.boxMessage.scrollTop = chat.boxMessage.scrollHeight;
}

//MENSAGEM ENVIADOR
//melhorar com a resposta do Vzinho
const appendMessage = () => {
    if (!chat.inputMessage.value) {
    }
    else {
        const newMessage = chat.ballonMessage2.parentNode.parentNode.cloneNode(true);
        newMessage.querySelector('p.m-3').textContent = chat.inputMessage.value;
        chat.boxMessage.appendChild(newMessage);
        chat.inputMessage.value = "";
        scrollToBottom();
        teste();
    }
}

//MESSAGEM RECEBEDOR
const appendNewMessage = (msg = null) => {
    const newMessage = chat.ballonMessage1.parentNode.parentNode.cloneNode(true);
    newMessage.querySelector('p.m-3').textContent = msg.description + msg.timeSent;
    chat.boxMessage.appendChild(newMessage);
    scrollToBottom();
}


chat.sendButton.addEventListener("click", appendMessage

);
chat.inputMessage.addEventListener("keydown", function (event) {

    if (event.key === "Enter") {
        event.preventDefault();
        // Do more work
        appendMessage();
    }
}
);


