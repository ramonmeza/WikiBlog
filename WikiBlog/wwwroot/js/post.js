"use strict";

// Connection
var connection = new signalR.HubConnectionBuilder().withUrl("/postHub").build();

// When you receive a server call
connection.on("ReceivePost", function (title, author, content) {
    /*
    var msg = author.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    */

    console.log("post made");
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

// Send a RPC
document.getElementById("sendPost").addEventListener("click", function (event) {
    var title = "title"
    var author = "author";
    var content = "content";

    connection.invoke("SendPost", title, author, content).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});