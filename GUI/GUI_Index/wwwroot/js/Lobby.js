const connection = new signalR.HubConnection("/Hubs/Lobbyhub", { logger: signalR.LogLevel.Information });

connection.on("Connect", () => {
    const encodedMsg = "A user signed on";
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("Messages").appendChild(li);
    location.reload(forceRedraw);
});

connection.on("Disconnect", () => {
    location.reload();
});

connection.start().catch(err => console.error);