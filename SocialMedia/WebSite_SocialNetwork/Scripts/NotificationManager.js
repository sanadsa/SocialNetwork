$(function () {
    var connection = $.hubConnection("http://localhost:55140/");
    var hub = connection.createHubProxy("echo");
    hub.on("AddMessage", Method);
    connection.start({ jsonp: true })
        .done(function () {
            console.log('connected');
            hub.say("success?");
        })
        .fail(function (a) {
            console.log('not connected' + a);
        });
});

function Method(messageFromHub) {
    alert(messageFromHub);
}