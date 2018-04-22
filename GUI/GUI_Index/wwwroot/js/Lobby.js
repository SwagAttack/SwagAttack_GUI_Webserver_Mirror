const connection = new signalR.HubConnection("/Hubs/Lobbyhub", { logger: signalR.LogLevel.Information });
connection.on("Connect", () => {
    const encodedMsg = document.getElementById("LobbyUser").textContent;
    const li = document.createElement("li");
    li.textContent = "User: " + encodedMsg + " Signed On!";
    document.getElementById("Messages").appendChild(li);
    //location.reload();

});

connection.on("Disconnect", () => {
    const encodedMsg = document.getElementById("LobbyUser").textContent;
    const li = document.createElement("li");
    li.textContent = "User: " + encodedMsg + " Signed Off!";
    document.getElementById("Messages").appendChild(li);
    //location.reload();
});

document.getElementById("sendButton").addEventListener("click", event => {
    const user = document.getElementById("LobbyUser").textContent;
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(err => console.error);
    event.preventDefault();
});

connection.on("ReceiveMessage", (user, message) => {
    const encodedMsg = user + " says " + message;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("Messages").appendChild(li);
});

connection.start().catch(err => console.error);

