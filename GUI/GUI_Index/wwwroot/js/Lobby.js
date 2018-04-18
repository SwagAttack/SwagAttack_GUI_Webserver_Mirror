const connection = new signalR.HubConnection(
    "/lobbyhub", { logger: signalR.LogLevel.Information });

//lobby oprettet til liste LobbyList
connection.on("OprettetLobby", (LobbyName) => {
    
    const li = document.createElement("li");
    li.textContent = LobbyName;
    li.id = "lobby";
    //var button = document.createElement("button");
    //button.innerText = LobbyName;
    //button.class = "btn btn-post";
    
    li.addEventListener("click", function () { location.href = "Lobby" });
    li.addEventListener("mouseover", function () { li.style.fontWeight = 'bold'; });
    li.addEventListener("mouseleave", function () { li.style.fontWeight = 'normal'; });
    //button.onclick = function onclick() {location.href = "Lobby"};

    document.getElementById("LobbyList").appendChild(li);
});

//Tilslut en exsisterende lobby ved hjælp af dens knap
//document.getElementById()

//opret lobby knap
document.getElementById("OpretButton").addEventListener("click", event => {
    //const user = document.getElementById("userInput").value;
    const LobbyName = document.getElementById("LobbyName").value;
    connection.invoke("OpretLobbyAsync", LobbyName).catch(err => console.error);
    event.preventDefault();
});

connection.start().catch(err => console.error);