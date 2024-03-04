function changePasswordMode() {
    var passwordEditor = $("#password").dxTextBox("instance");
    var buttonEditor = $("#passwordButton").dxButton("instance");
    passwordEditor.option("mode", passwordEditor.option("mode") === "text" ? "password" : "text");
    buttonEditor.option("icon", buttonEditor.option("icon") === "fa fa-eye-slash" ? "fa fa-eye" : "fa fa-eye-slash");
} 