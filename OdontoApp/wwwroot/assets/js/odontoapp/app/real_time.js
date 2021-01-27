let connectionMsg = new signalR.HubConnectionBuilder()
.withUrl("/messagehubservice")
.configureLogging(signalR.LogLevel.Information)
.build();
connectionMsg.start().then(() => connectionMsg.on("reportnewmessagesasync", (msg,cdc) => console.log(msg, cdc)));
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
            messagecode: GetUniqueIdentifier().then(resp => resp),
            receiverUser: null,
            receiverUserId: "",
            senderUser: null,
            senderUserId: "",
            timeReceived: "",
            timeSent: "",
            viewedTime: "",
        })
    }
)
async function teste()
{
    console.log(await GetUniqueIdentifier())
    // fetch(request)
    // .then(response => console.log(response.json()))
    // .catch(error => console.error('Unable to update item.', error));
}

async function GetUniqueIdentifier()
{
    response = await fetch("/api/messages/GetUniqueIdentifier");
    return response.json()
}