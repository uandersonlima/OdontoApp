const chat = {
    boxMessage: document.querySelector('div.chat'),
    ballonMessage: document.querySelector('div.balao2'),
    inputMessage: document.querySelector('input[id="mensagem"]'),
    sendButton: document.querySelector('button#butaozinho')
}

const scrollToBottom = () => {
    let shouldScroll = chat.boxMessage.scrollTop + chat.boxMessage.clientHeight === chat.boxMessage.scrollHeight;
    if(!shouldScroll)
        chat.boxMessage.scrollTop = chat.boxMessage.scrollHeight;
}

const appendMessage = () => {
    teste();
    const newMessage = chat.ballonMessage.parentNode.parentNode.cloneNode(true);
    newMessage.querySelector('p.m-3').textContent = chat.inputMessage.value;
    chat.boxMessage.appendChild(newMessage);
    chat.inputMessage.value = "";
    scrollToBottom();
}

chat.sendButton.addEventListener("click", appendMessage
);


