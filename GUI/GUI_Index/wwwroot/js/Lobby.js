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

connection.start().catch(err => console.error);

