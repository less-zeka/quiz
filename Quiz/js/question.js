$(".answerButton").click(function () {
    var clickedButtonId = this.id;
    var answerId = this.id.match(/\d+/); // 123456
    $(".answerButton").attr("disabled", true);
    console.log(clickedButtonId + "test");
    $("#" + clickedButtonId).css("background-color", "red");
    //quiz.server.playerAnswer("test");
    $("#playerList").load("/Default/PlayerAnswer/", { answerId: answerId });
});