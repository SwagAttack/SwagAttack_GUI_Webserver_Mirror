const connection = new signalR.HubConnection(
    "/lobbyhub", { logger: signalR.LogLevel.Information });

//lobby oprettet til liste LobbyList
connection.on("OprettetLobby", (LobbyName) => {

    var button = document.createElement("button");
    button.innerText = LobbyName;
    button.class = "btn btn-post";

    button.onclick = function onclick() {location.href = "Lobby"};

    document.getElementById("LobbyList").appendChild(button);
});

//opret lobby knap
document.getElementById("OpretButton").addEventListener("click", event => {
    //const user = document.getElementById("userInput").value;
    const LobbyName = document.getElementById("LobbyName").value;
    connection.invoke("OpretLobbyAsync", LobbyName).catch(err => console.error);
    event.preventDefault();
});

connection.start().catch(err => console.error);