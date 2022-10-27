
//Configura conexão com o signalR
const connectionMessageHub = new signalR.HubConnectionBuilder()
    .withUrl("/messagehubservice")
    .configureLogging(signalR.LogLevel.Information)
    .build();

//inicia conexão
const connectionstartMessageHub = async () => {
    connectionMessageHub.start().then(
        () => {
            enableCHAT();
            console.info("Connected!");
        }
    ).catch(function (err) {
        if (connection.state == 0) {
            console.error(err.toString());
            setTimeout(connectionstartMessageHub, 3000);
        }
    });
}
connectionMessageHub.onclose(async () => await connectionstartMessageHub());

const enableCHAT = async () => {
    connectionMessageHub.on("reportnewmessagesasync", (msg, cdc) => appendNewMessage(cdc))
    connectionMessageHub.on("UserIsConnectedAsync", msg => console.info(msg))
    connectionMessageHub.on("UserIsDisconnectedAsync", msg => console.info(msg))
}



//Função teste para enviar mensagem fixa; 

const sendMessageApi = async (destination, msg) => {
    const request = new Request("/api/messages", {
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
        body: JSON.stringify({
            description: msg,
            receiverUserId: destination,
        })
    });
    const response = await fetch(request);
    return response
}

connectionstartMessageHub()