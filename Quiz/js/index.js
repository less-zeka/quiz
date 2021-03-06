﻿$(function () {
    // Reference the auto-generated proxy for the hub.
    var quiz = $.connection.quizHub;

    quiz.client.playerAnswer = function (answerId) {
        console.log("playerAnswer what to do now?");
        quiz.server.playerAnswer(answerId);
    };

    quiz.client.updateConnectedUsers = function () {
        console.log("updateConnectedUsers");
        $("#playerList").load("/Default/UserLegend");
    };

    quiz.client.nextQuestion = function () {
        console.log("nextQuestion");
        $("#nextQuestion").removeClass("hidden");
        $("#nextQuestion").load("/Default/NextQuestion");

    };

    // Start the connection.
    // here goes the master mylord!
    $.connection.hub.start().done(function () {
        $("#readyButton").click(function () {
            // Call the Send method on the hub.
            quiz.server.playerIsReady($("#displayname").val());
            $(this).addClass("hidden");
        });
    });
});

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $("<div />").text(value).html();
    return encodedValue;
}