
$("#registerForm").on("submit", function (e) {
    e.preventDefault();
    const email = $("#email").val();
    const senha = $("#senha").val();
    const senhaConfirm = $("#senhaConfirm").val();

    if (senha != senhaConfirm) {
        alert("As senhas não conferem");
        return;
    }

    const data = {
        Nome: "teste",
        Email: email,
        Senha: senha,
        Grupo: "USER"
    };
    
    $.ajax({
        url: "/api/usuario",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (response) {
            window.location.href = "/";
        },
        error: function (xhr) {
            console.log("Ocorreu um erro no registro");
        }
    });
});