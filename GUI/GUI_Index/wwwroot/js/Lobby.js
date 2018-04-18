const connection = new signalR.HubConnection(
    "/lobbyhub", { logger: signalR.LogLevel.Information });

//lobby oprettet til liste LobbyList
connection.on("OprettetLobby", (LobbyName) => {
    const encodedMsg = LobbyName;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("LobbyList").appendChild(li);
});

//opret lobby knap
document.getElementById("OpretButton").addEventListener("click", event => {
    //const user = document.getElementById("userInput").value;
    const LobbyName = document.getElementById("LobbyName").value;
    connection.invoke("OpretLobbyAsync", LobbyName).catch(err => console.error);
    event.preventDefault();
});

connection.start().catch(err => console.error);