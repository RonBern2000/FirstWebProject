const connection = new signalR.HubConnectionBuilder().withUrl("/mainHub").build();
const commentList = document.getElementById("commentList");
const commentCount = document.getElementById('commentCount');

connection.on("ReceiveComment", (comment,numComments) => {
    const p = document.createElement('p');
    p.textContent = comment;
    commentList.appendChild(p);
    commentCount.textContent = `Number of comments: ${numComments}`;
});

connection.start().catch( (err) => {
    return console.error(err.toString());
});