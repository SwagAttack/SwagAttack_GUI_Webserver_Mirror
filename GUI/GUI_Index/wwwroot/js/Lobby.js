const connection = new signalR.HubConnection("/Hubs/Lobbyhub", { logger: signalR.LogLevel.Information });

connection.on("Connect", () => {
    var  encodedMsg = document.getElementById("LobbyUser").textContent;
    connection.invoke("OnConnectedUserAsync", encodedMsg);
});

connection.on("OnConnectedUser",
    (user) => {
        var li = document.createElement("li");
        li.textContent = "User: " + user + " Signed On!";
        document.getElementById("Messages").appendChild(li);
        //location.reload();

        const table = document.getElementById("UsersInLobby");
        const newrow = table.insertRow(table.rows.length);
        const newcell = newrow.insertCell(0);

        const newText = document.createTextNode(user);
        newcell.appendChild(newText);
    }); 

connection.on("Disconnect", () => {
    //location.reload();
    var encodedMsg = document.getElementById("LobbyUser").textContent;
    connection.invoke("OnDisconnectedUserAsync", encodedMsg);
});

connection.on("OnDisconnectedUser",
    (user) => {
        const li = document.createElement("li");
        li.textContent = "User: " + user + " Signed Off!";
        document.getElementById("Messages").appendChild(li);
        
    });

document.getElementById("sendButton").addEventListener("click", event => {
    const user = document.getElementById("LobbyUser").textContent;
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessageAsync", user, message).catch(err => console.error);
    event.preventDefault();
});

connection.on("ReceiveMessage", (user, message) => {
    const encodedMsg = user + " says " + message;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("Messages").appendChild(li);
});

connection.start().catch(err => console.error);

