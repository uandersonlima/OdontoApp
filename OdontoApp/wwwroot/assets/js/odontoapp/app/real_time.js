
//Configura conexão com o signalR
let connectionMsg = new signalR.HubConnectionBuilder()
    .withUrl("/messagehubservice")
    .configureLogging(signalR.LogLevel.Information)
    .build();


    //Starta conexão
    connectionMsg.start().then(() => connectionMsg.on("reportnewmessagesasync", (msg, cdc) => {
    appendNewMessage(cdc);
}
));


connectionMsg.on("UserIsConnectedAsync", msg => console.log(msg))
connectionMsg.on("UserIsDisconnectedAsync", msg => console.log(msg))

inputIdUser = document.querySelector('input[id="user"]');

//Função teste para enviar mensagem fixa; 
function teste() {
    let request = new Request("/api/messages", {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            description: "salve meu compatriota",
            isDeleted: false,
            messagecode: null,
            receiverUser: null,
            receiverUserId: inputIdUser.value,
            senderUser: null,
            senderUserId: "",
            timeReceived: "",
            timeSent: "",
            viewedTime: "",
        })
    }
    )
    fetch(request)
        .then(response => {
            if (response.status == 200) {
                console.log('ok');
            }
        })
        .catch(error => console.error('Unable to update item.', error));
}

