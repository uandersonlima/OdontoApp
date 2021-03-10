let connectionMsg = new signalR.HubConnectionBuilder()
    .withUrl("/messagehubservice")
    .configureLogging(signalR.LogLevel.Information)
    .build();
connectionMsg.start().then(() => connectionMsg.on("reportnewmessagesasync", (msg, cdc) => console.log(msg, cdc)));
connectionMsg.on("UserIsConnectedAsync", msg => console.log(msg))
connectionMsg.on("UserIsDisconnectedAsync", msg => console.log(msg))





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
        receiverUserId: "6e713cc6-07f7-44e9-a559-3fdc3671d91b",
        senderUser: null,
        senderUserId: "",
        timeReceived: "",
        timeSent: "",
        viewedTime: "",
    })
}
)

function teste() {
    fetch(request)
        .then(response => console.log(response.json()))
        .catch(error => console.error('Unable to update item.', error));
}

