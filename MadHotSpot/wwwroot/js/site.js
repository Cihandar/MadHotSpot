// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GridIslemlerButtons(data, type, full, meta, updateeUrl, deleteUrl) {
    return '<button class="btn btn-info font-weight-bold btn-pill mr-2 seridenApp-datatables-update  min-w-90px" title="Düzenle" data-endpoint="' + updateeUrl + '" data-id="' +
        full.id +
        '">Güncelle</button>' +
        '<button  class="btn btn-danger font-weight-bold btn-pill  mr-2 seridenApp-datatables-delete  min-w-90px" title = "Sil" data-endpoint="' + deleteUrl + '" data-id="' + full.id + '">Sil</button> ';
}


$(document).on("click", ".seridenApp-datatables-create", function () {
    var element = $(this);
    ModalCallWithUrl(element.data("endpoint"), null, "crud-modal");

});

$(document).on("click", ".seridenApp-datatables-update", function () {
    var element = $(this);
    ModalCallWithUrl(element.data("endpoint"), $(this).data("id") , "crud-modal");

});


$(document).on("click", ".seridenApp-datatables-delete", function () {
    var element = $(this);
    Notiflix.Confirm.Show("Uyarı", 'Kaydı Silmek istiyor musunuz?', 'Evet', 'Hayır',
        function () { // Yes button 
            Notiflix.Block.Standard('.content');
            $.ajax({
                type: "post",
                url: element.data("endpoint"),
                data: { Id: element.data("id") },
                success: function (result) {

                    if (result.success) {
                        Notiflix.Notify.Success(result.message);
                        if (GetData && typeof GetData === 'function') {
                            GetData();
                        }
                    } else {
                        Notiflix.Notify.Failure(result.message);
                    }
                    Notiflix.Block.Remove("*", 0);
                }

            });
        }, function () { // No button 

        });

});


// Ajax Form OnFailure on
var OtelAppOnFailure = function (data) {
    var failMessage = "";
    if (data && data.responseJSON && data.responseJSON.message && data.responseJSON.message.length > 0) {
        failMessage = data.responseJSON.message;
    }
    Notiflix.Notify.Failure(failMessage);
};
// Ajax Form OnFailure off

// Ajax Form OnComplete on
var OtelAppOnComplete = function (id, delay, callback) {

    Notiflix.Block.Remove("*", 0);

    if (callback && typeof callback === 'function') {
        callback();
    }



};

var OtelAppOnSuccess = function (data, callback) {

    if (data.success) { // api success
        Notiflix.Notify.Success(data.message);

        if (callback && typeof callback === 'function') {

            callback("", data); //first gridId, second data
        }

        // modal close on
        if ($('.OtelApp-forms-modal').length > 0) {
            $('.OtelApp-forms-modal').modal('hide');
        }
        // modal close off

        // success sonrasi redirectUrl varsa oraya git on
        //if (data.redirectUrl) {
        //    setTimeout(function () {
        //        window.location.href = data.redirectUrl;
        //    }, 1000);
        //}
        // success sonrasi redirectUrl varsa oraya git off
    }
    // api error
    else if (data.success !== undefined && !data.success) {


        Notiflix.Notify.Failure(data.message);

    } else { // api else
        Notiflix.Notify.Failure(data.message);
    }
};

$(document).on("click", "#BtnSignOut", function () {
    Notiflix.Confirm.Show("Uyarı", 'Çıkış Yapmak istiyor musunuz?', 'Evet', 'Hayır',
        function () { // Yes button 
            window.location.href = "/Auth/Logout";
        }, function () { // No button 

        });

});

$(document).on("click", "#InterSatisIade", function () {
    id = $(this).data("id");
    Notiflix.Block.Standard('.content');
    $.ajax({
        type: "post",
        url: "InternetSatis/IadeHesapla",
        data: { Id: id },
        success: function (result) {

            if (result.success) {

                Notiflix.Confirm.Show("Uyarı", 'İade Yapılacak.<br>Iade Gün : '+ result.iadeGun +' Iade Tutar : '+result.iadeTutar +' '+ result.doviz +'  İadeye Devam Edilsin mi ?', 'Evet', 'Hayır',
                    function () { // Yes button          
                        Notiflix.Block.Standard('.content');
                        $.ajax({
                            type: "post",
                            url: "InternetSatis/Iade",
                            data: { Id: id },
                            success: function (result) {

                                if (result.success) {
                                    Notiflix.Notify.Success(result.message);
                                    yenile();
                                } else {
                                    Notiflix.Notify.Failure(result.message);
                                }
                                Notiflix.Block.Remove("*", 0);
                            }

                        });
                    }, function () { // No button 
                        Notiflix.Notify.Failure("İşlem iptal Edildi");
                    });
 
            } else {
                Notiflix.Notify.Failure(result.message);
            }
            Notiflix.Block.Remove("*", 0);
        }

    });



});

function RandomPassCreator(length) {
    var result = '';
    var characters = '0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() *
            charactersLength));
    }
    return result;
}

function IadeYap() {

}

