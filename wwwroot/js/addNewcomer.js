﻿// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            url: `/Home/AddMember?memberName=${newcomerName}`,
            success: function (data) {
                // Remember string interpolation
                $("#list").append(`<li class="member">
		            <span class="name">${newcomerName}</span><span class="delete fa fa-remove"></span><i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
		        </li>`);
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
            url: `/Home/UpdateMember?id=${id}&newName=${newName}`,
            type: 'PUT',
            success: function (response) {
                $('.name').eq(clientId).replaceWith(newName);
            },
            error: function (data) {
                alert(`Failed to update`);
            }
        });
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })
});