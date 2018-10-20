/*
 * WARNING!!!!
 * 
 * This code is not DRY. It is very WET! This is not it's final form, but it is functional.
 * 
 * Just a friendly reminder. DON'T REPEAT YOURSELF!
 * 
 * */

"use strict";

// Connection
var connection = new signalR.HubConnectionBuilder().withUrl("/postHub").build();

// Receive a new post
connection.on("ReceivePost", function (title, author, content) {
    // This is a really sketchy way of doing this
    var id = $("#NewPostCard").next().children().first().data("postid") + 1;

    // Get substring for content
    var subContent = content.substring(0, 500);
    if (content.length > 500)
        subContent += "...";

    // HTML string to append when a new post is made from someone else
    // It's long...
    var newPostCard = '<a href="/Posts/' + id + '" class="custom-card"><div class="card post" data-postid="' + id + '" data-target="#postModal"><div class="card-body"><h5 class="card-title font-weight-bold">' + title + '</h5><hr /><p class="card-text">' + subContent + '</p><p class="card-text font-italic text-center"><small class="text-muted">Posted by ' + author + '</small></p></div></div></a>';

    $("#NewPostCard").after(newPostCard);
});

// Receive an edited post
connection.on("ReceiveEdit", function (id, title, author, content) {
    var postToEdit = $("[data-postid='" + id + "']");

    postToEdit.find(".card-title").text(title);
    postToEdit.find(".card-text:first").text(content);
    postToEdit.find("text-mmuted").text(author);
});

// Receive a delete post
connection.on("ReceiveDelete", function (id) {
    $("[data-postid='" + id + "']").parent().remove();
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

$(document).ready(function () {
    // Current work-around
    // Create a fake form so that I can send to SignalR and update real-time
    // Create a hidden form to send data to SQL server.
    // There's definitely a better way to do this...
    document.getElementById("NewPostButton").addEventListener("click", function (event) {
        // Get form data
        var title = $("#NewPostTitle").val();
        var author = $("#NewPostAuthor").val();
        var content = $("#NewPostContent").val();

        // Compensating for required attribute in form, but I'm not using a form anymore.
        if (title.length > 0 && author.length > 0 && content.length > 0) {
            // Send a RPC
            connection.invoke("SendPost", title, author, content).catch(function (err) {
                return console.error(err.toString());
            });

            // Create a form to actually post to the DB
            var form = $("#HiddenNewPostForm");

            var titleInput = document.createElement("input");
            titleInput.type = "hidden";
            titleInput.name = "Title";
            titleInput.value = title;

            var authorInput = document.createElement("input");
            authorInput.type = "hidden";
            authorInput.name = "Author";
            authorInput.value = author;

            var contentInput = document.createElement("input");
            contentInput.type = "hidden";
            contentInput.name = "Content";
            contentInput.value = content;

            form.append(titleInput);
            form.append(authorInput);
            form.append(contentInput);

            form.submit();

            event.preventDefault();
        }
    });

    document.getElementById("EditPostButton").addEventListener("click", function (event) {
        // Get form data
        var id = $(this).data("postid");
        var title = $("#EditPostTitle").val();
        var author = $("#EditPostAuthor").val();
        var content = $("#EditPostContent").val();

        if (title.length > 0 && author.length > 0 && content.length > 0) {
            // Send a RPC
            connection.invoke("SendEdit", id, title, author, content).catch(function (err) {
                return console.error(err.toString());
            });

            // Create a form to actually post to the DB
            var form = $("#HiddenEditPostForm");

            var titleInput = document.createElement("input");
            titleInput.type = "hidden";
            titleInput.name = "Title";
            titleInput.value = title;

            var authorInput = document.createElement("input");
            authorInput.type = "hidden";
            authorInput.name = "Author";
            authorInput.value = author;

            var contentInput = document.createElement("input");
            contentInput.type = "hidden";
            contentInput.name = "Content";
            contentInput.value = content;

            form.append(titleInput);
            form.append(authorInput);
            form.append(contentInput);

            form.submit();

            event.preventDefault();
        }
    });

    // Delete post
    document.getElementById("DeletePostButton").addEventListener("click", function (event) {
        // Get element id
        var id = $(this).data("postid");
            
        // Send a RPC
        connection.invoke("SendDelete", id).catch(function (err) {
            return console.error(err.toString());
        });
    });
});