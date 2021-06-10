// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GridIslemlerButtons(data, type, full, meta, updateeUrl, deleteUrl) {
    return '<button class="btn btn-success font-weight-bold btn-pill mr-2 seridenApp-datatables-update  min-w-90px" style="background:#31C241 !important" title="Düzenle" data-endpoint="' + updateeUrl + '" data-id="' +
        full.id +
        '">Güncelle</button>' +
        '<button  class="btn btn-danger font-weight-bold btn-pill mt-1 mr-2 seridenApp-datatables-delete  min-w-90px" title = "Sil" style="background:#FF4301 !important"  data-endpoint="' + deleteUrl + '" data-id="' + full.id + '">Sil</button> ';
}


$(document).on("click", ".seridenApp-datatables-create", function () {
    debugger;
    var element = $(this);
    ModalCallWithUrl(element.data("endpoint"), null, "crud-modal");

});


// Ajax Form OnFailure on
var OtelAppOnFailure = function (data) {
    var failMessage = ajaxErrorMessage;
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