function onSubmitClickResetPass(e) {
    e.event.preventDefault();
    var $form = $('form[name="resetPassword"]');
    var url = $form.attr('action');
    var method = $form.attr('method');
    var email = $('input[name="Email"]').val();

    $.ajax({
        url: url,
        cache: false,
        type: method,
        dataType: "json",
        data: $form.serialize(),
        success: function (result) {
            if (result.Result == 1) {
                Swal.fire(
                    email,
                    'success'
                ).then(function () {
                    window.location = "/Account/Logout/";
                });
            }
            else if (result.Result == 4) {
                let editor = $("#Email").dxTextBox("instance");
                editor.option({
                    validationStatus: "invalid",
                    validationErrors: [{ message: result.Message }]
                });
            }

        }
    })
}