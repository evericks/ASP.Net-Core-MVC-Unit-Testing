/*Image Preview*/
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#image').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#upload").change(function () {
    readURL(this);
});

/*Search*/
$(document).ready(function () {
    $("#search").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#idols div.col-lg-3").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

/*Ajax Create Comment*/
$(function () {
    let form = $("#create-comment-form");
    form.on('submit', function (e) {
        e.preventDefault();
        let url = form.attr("action");
        let data = form.serialize();
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            success: function (res) {
                if (res === "success") {
                    location.reload();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown + textStatus);
            }
        });
    });
});

/*Ajax Login*/
$(function () {
    let form = $("#form-login");
    form.on('submit', function (e) {
        e.preventDefault();
        let url = form.attr("action");
        let data = form.serialize();
        if ($('#form-login #username').val().length == 0 || $('#form-login #password').val().length == 0) {
            toastr.warning("Input data can't be blank");
            return false;
        }
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            success: function (res) {
                if (res === "failed") {
                    toastr.error('Username or password is incorrect');
                } else if (res === "success") {
                    $(location).attr('href', "Create")
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
    });
});

/*Ajax Create User*/
$(function () {
    let form = $("#form-create");
    form.on('submit', function (e) {
        e.preventDefault();
        let url = form.attr("action");
        let data = form.serialize();
        var valid = /^\w+([-+.'][^\s]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
        var email = valid.test($("#email").val());
        let username = $('#form-create #username').val();
        let password = $('#form-create #password').val();
        let confirm = $('#form-create #confirm').val();
        let name = $('#form-create #name').val();
        if (username.length == 0 || password.length == 0 || name.length == 0 || confirm.length == 0) {
            toastr.warning("Data input can't be blank");
            return false;
        } else if (password !== confirm) {
            toastr.warning("Password does not match");
            return false;
        } else if (!email) {
            toastr.warning('Email invalidate');
            return false;
        } else {
            $.ajax({
                type: "POST",
                url: url,
                data: data,
                success: function (res) {
                    if (res === "success") {
                        toastr.success('Account registration successful');
                    } else {
                        toastr.warning('Username or Email is already taken');
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            });
        }
    });
});
