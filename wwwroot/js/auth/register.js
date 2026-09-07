
$("#registerForm").on("submit", function (e) {
    e.preventDefault();
    const email = $("#email").val();
    const senha = $("#senha").val();
    

    const data = {
        email: email,
        senha: senha
    };

    $.ajax({
        url: "/api/auth/register",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (response) {
            console.log(response);
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
});