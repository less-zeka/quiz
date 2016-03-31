$(function () {
    // Reference the auto-generated proxy for the hub.
    var quiz = $.connection.quizHub;

    //update userList
    quiz.client.currentPlayers = function (players) {
        console.log("currentPlayerFunction "+players);
        var playerText = '';
        for (var i = 0; i < players.length; i++) {
            playerText += players[i].Name + "; ";
        }
        $('#playerList').text(playerText);
    };

    quiz.client.updateConnectedUsers = function () {
        console.log("updateConnectedUsers");
        $("#playerList").load("/Default/UserLegend");
    };

    quiz.client.nextQuestion = function (nextQuestion) {
        $("#nextQuestion").removeClass("hidden");
        $("#nextQuestion").load("/Default/NextQuestion");

    };



    // Start the connection.
    // here goes the master mylord!
    $.connection.hub.start().done(function () {
        $('#readyButton').click(function () {
            // Call the Send method on the hub.
            quiz.server.playerIsReady($('#displayname').val());
        });
    });
});

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}