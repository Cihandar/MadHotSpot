var signUpButton = document.getElementById('signUp');
var signInButton = document.getElementById('signIn');
var container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
    container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
    container.classList.remove("right-panel-active");
});

function redirectUrl() {
    window.location.href = "/";
}

function dataTableReload() {
    Notiflix.Report.Success('Kaydınız yapıldı..',
        '<b>Otel Kodunuz mail adresinize gönderildi.Göndermiş olduğumuz bu kodla giriş yapabilirsiniz</b>',
        'Giriş Sayfasına Git',
        function () {
            window.location.reload();
        });
}

$(function () {
    $.validator.unobtrusive.parse('form');
});
 