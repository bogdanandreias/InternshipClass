// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            contentType: 'application/json',
            data: JSON.stringify({ "Name": `${newcomerName}` }),
            method: "POST",
            url: 'api/Internship/',
            success: function (data) {
                $("#newcomer").val("");
            },
            error: function (data) {
                alert(`Failed to add ${newcomerName}`);
            },
        });
    })
    
    $("#clear").click(function () {
        $("#newcomer").val("");
    })

    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var serverId = targetMemberTag.attr('member-id');
        var clientId = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("member-id", serverId);
        $('#editClassmate').attr("memberIndex", clientId);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {

        var newName = $('#classmateName').val();
        var id = $('#editClassmate').attr("member-id");
        var clientId = $('#editClassmate').attr("clientId");
        console.log(`/Home/UpdateMember?index=${id}&name=${newName}`);
        $.ajax({
            contentType: 'application/json',
            data: JSON.stringify({ "Name": `${newName}` }),
            method: "PUT",
            url: `api/Internship/${id}`,
            error: function (data) {
                alert(`Failed to update`);
            }
        });
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })
});