// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GridIslemlerButtons(data, type, full, meta, updateeUrl, deleteUrl) {
    return '<button class="btn btn-success font-weight-bold btn-pill mr-2 seridenApp-datatables-update  min-w-90px" style="background:#31C241 !important" title="Düzenle" data-endpoint="' + updateeUrl + '" data-id="' +
        full.id +
        '">Güncelle</button>' +
        '<button  class="btn btn-danger font-weight-bold btn-pill mt-1 mr-2 seridenApp-datatables-delete  min-w-90px" title = "Sil" style="background:#FF4301 !important"  data-endpoint="' + deleteUrl + '" data-id="' + full.id + '">Sil</button> ';
}


$(".seridenApp-datatables-create").on("click", function () {
    var element = $(this);
    ModalCallWithUrl(element.data("endpoint"), null, "crud-modal");

});