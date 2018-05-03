const connection = new signalR.HubConnection("/Hubs/Lobbyhub", { logger: signalR.LogLevel.Information });

/////////////////////////////////////Enter Lobby///////////////////////////////////////////

connection.on("Connect", () => {

    //get the username
    var Username = document.getElementById("LobbyUser").textContent;

    //check if the user is in alredy (reconnect) //needs to be from the table
    if (document.getElementById(Username) == null) {
        //get the lobbyName
        var Lobbyname = document.getElementById("LobbyId").textContent;
        //send to hub
        connection.invoke("OnConnectedUserAsync", Username, Lobbyname);
    }

});

connection.on("OnConnectedUser",
    (user) => {
        //create chat message
        var li = document.createElement("li");
        li.textContent = "User: " + user + " Signed On!";
        document.getElementById("Messages").appendChild(li);

        //update table
        const table = document.getElementById("UsersInLobby");
        const newrow = table.insertRow(table.rows.length);
        //set the id of the row
        newrow.id = user;
        const newcell = newrow.insertCell(0);
        //add user to table
        const newText = document.createTextNode(user);
        newcell.appendChild(newText);
    }); 

///////////////////////////////////////Messages///////////////////////////////////////////////

document.getElementById("sendButton").addEventListener("click", event => {
    //get the username
    const user = document.getElementById("LobbyUser").textContent;
    //get the message 
    const message = document.getElementById("messageInput").value;
    //get the lobbyname
    const lobby = document.getElementById("LobbyId").textContent;
    //send it to hub
        connection.invoke("SendMessageAsync", user,lobby, message).catch(err => console.error);
    event.preventDefault();
});

connection.on("ReceiveMessage", (user, message) => {
    //write the complete message
    const Message = user + " says " + message;
    //create list element
    const li = document.createElement("li");
    //add to list element
    li.textContent = Message;
    //append to chat
    document.getElementById("Messages").appendChild(li);
});

///////////////////////////leave Lobby////////////////////////////////////

//Setup click event
document.getElementById("ForLadLobby").addEventListener("click", event => {
    //get the username
    const user = document.getElementById("LobbyUser").textContent;
    //get the lobbyname
    const lobby = document.getElementById("LobbyId").textContent;
    //send it to hub
    connection.invoke("UserLeftAsync", user, lobby).catch(err => console.error);
    event.preventDefault();
});

//user left
connection.on("OnDisconnectedUser",
    (user) => {
        //create element to hold information
        const li = document.createElement("li");
        //tell others that user left
        li.textContent = "User: " + user + " Signed Off!";
        //add to chat
        document.getElementById("Messages").appendChild(li);

        //update table of online users
        var row = document.getElementById(user);
        row.deleteCell(0);

    });

connection.start().catch(err => console.error);

