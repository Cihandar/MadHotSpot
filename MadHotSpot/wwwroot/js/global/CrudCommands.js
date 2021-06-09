
function ModalCallWithUrl(url, id, formId) {
    $.get(url, { id: formId }, function (data, status) {
        $("body").append(data);
        $("#" + formId).modal("show");
        //$.validator.unobtrusive.parse('form');
    });
}

// Ajax Form OnBegin on
var OtelAppOnBegin = function (message) {

    //Block işlemi modala uygulanacaksa prevent işlemi için 
    var selector = '#' + $(this).attr('id');
    if ($(selector + ' .modal-content').length > 0) {
        Notiflix.Block.Standard(selector + ' .modal-content');
    } else {
        Notiflix.Block.Standard('#' + $(this).attr('id'));
    }
};
// Ajax Form OnBegin off

// Ajax Form OnSuccess on
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

        if (data.message && data.message.length > 0) {
            ajaxErrorMessage = data.message;
        }
        Notiflix.Notify.Failure(ajaxErrorMessage);

    } else { // api else
        Notiflix.Notify.Failure(ajaxErrorMessage);
    }
};
// Ajax Form OnSuccess off

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


$(document).on('hide.bs.modal', '.OtelApp-forms-modal', function () {
    $(this).remove();
});

var ajaxMethodDelete = function (id, endpoint, customClickEvent) {
    $.ajax({
        url: endpoint,
        type: 'POST',
        data: { id: id },
        beforeSend: function () {
            Notiflix.Block.Standard('#grid');
        },
        success: function (data) {
            if (data.success) { // api success

                // if (typeof GetData === "function")
                // GetData();

                //var redirectMessage = "";
                //if (redirectUrl) {
                //    redirectDelay = redirectDelay ? redirectDelay : 0;
                //    window.setTimeout(function () {
                //        location.href = redirectUrl;
                //    }, redirectDelay);
                //    redirectMessage = " Yönlendiriliyorsunuz..";
                //}

                Notiflix.Notify.Success(data.message);
                debugger;
                if (customClickEvent && typeof customClickEvent === 'function') {
                    customClickEvent(data);
                } else {
                    dataTableReload("");
                }

            }
            // api error
            else if (data.success !== undefined && !data.success) {

                if (data.message && data.message.length > 0) {
                    ajaxErrorMessage = data.message;
                }
                Notiflix.Notify.Failure(ajaxErrorMessage);

            } else { // api else
                Notiflix.Notify.Failure(ajaxErrorMessage);
            }
        },
        error: function (data) {
            if (data.status !== 401 && data.status !== 403) {
                Notiflix.Notify.Failure(ajaxErrorMessage);
            }
        },
        complete: function () {
            Notiflix.Block.Remove("*", 0);
        }
    });
};


function dataTableRowEdit(controller, action, modalId) {
    if (w2ui.grid.getSelection().toString() != null && w2ui.grid.getSelection().toString() != "") {
        ModalCallWithUrl(controller + "/" + (action || "Update"), (modalId || "crud-modal"), w2ui.grid.getSelection().toString());
    }
}

function dataTableReload(gridId) {
    var grid = (gridId || 'grid');
    w2ui[grid].reload();
}

function ToolbarCreate(title, controller, customItems, customClickEvent, createAction, deleteAction, toolbarId, deleteClickEvcnt) {
    var toolbar = (toolbarId || 'toolbar');

    var toolbarItems = $.merge([
        { type: 'button', id: 'append', text: 'Yeni Ekle', icon: 'w2ui-icon-plus' },
        { type: 'break', id: 'break1' },
        { type: 'button', id: 'insert', text: 'Düzelt', icon: 'fa fa-pen' },
        { type: 'button', id: 'delete', text: 'Sil', icon: 'w2ui-icon-cross' },
    ],
        (customItems || {}));

    $('#' + toolbar).w2toolbar({
        name: 'toolbar',
        items: toolbarItems,
        onClick: function (event) {
            switch (event.target) {
                case 'append':
                    //this.add({ type: 'button', id: 'button' + btnCount, text: 'button ' + btnCount, img: 'icon-page' });
                    //btnCount++;
                    ModalCallWithUrl(controller + "/" + (createAction || "Create"), "crud-modal");

                    break;
                case 'insert':
                    dataTableRowEdit(controller);
                    break;
                case 'delete':
                    Notiflix.Confirm.Show(title, 'Seçili Kayıt Silinecek. Devam Edilsin mi?', 'Evet', 'Hayır',
                        function () { // Yes button 
                            ajaxMethodDelete(w2ui.grid.getSelection(), controller + "/" + (deleteAction || "Delete"), deleteClickEvcnt);
                        }, function () { // No button 

                        });

                    break;
            }

            if (customClickEvent && typeof customClickEvent === 'function') {
                customClickEvent(event);
            }
        }
    });
}


function GridDataTable(columns, searches, controller, action, gridId,parametre) {
    $.each(columns, function(index,value) {
        value.sortable = true;
    })
 
    $('#grid').w2grid({
        name: 'grid',
        url: controllerName + '/GetAll' + (parametre||""),
        method: 'GET',
        recid: 'id',
        recordHeight: 40,
        sortData: [{ field: 'recid', direction: 'asc' }],
        show: {
            toolbar: true,
            footer: true,
        },
        columns: columns,
        multiSearch: true,
        searches: searches,
        onDblClick: function (event) {
            dataTableRowEdit(controller);
        }
    });
}



function GridDataTableWithAjax(columns, searches, controller, action, gridId) {
    $.each(columns, function (index, value) {
        value.sortable = true;
    })

    $.ajax({
        url: controllerName + '/GetAll?request={}',
        type: 'GET',
        beforeSend: function () {
            Notiflix.Block.Standard('#grid');
        },
        success: function (data) {
            if (data) { // api success
                if (w2ui['grid'])
                    w2ui['grid'].destroy();
                // if (typeof GetData === "function")
                // GetData();

                //var redirectMessage = "";
                //if (redirectUrl) {
                //    redirectDelay = redirectDelay ? redirectDelay : 0;
                //    window.setTimeout(function () {
                //        location.href = redirectUrl;
                //    }, redirectDelay);
                //    redirectMessage = " Yönlendiriliyorsunuz..";
                //}

                Notiflix.Notify.Success(data.message);
                console.log(data);
                $('#grid').w2grid({
                    name: 'grid',
                    method: 'GET',
                    recid: 'id',
                    recordHeight: 40,
                    sortData: [{ field: 'recid', direction: 'asc' }],
                    show: {
                        toolbar: true,
                        footer: true,
                    },
                    columns: columns,
                    multiSearch: true,
                    searches: searches,
                    onDblClick: function (event) {
                        dataTableRowEdit(controller);
                    },
                    records: data.records
                });

            }
            // api error
            else if (data.success !== undefined && !data.success) {

                if (data.message && data.message.length > 0) {
                    ajaxErrorMessage = data.message;
                }
                Notiflix.Notify.Failure(ajaxErrorMessage);

            } else { // api else
                Notiflix.Notify.Failure(ajaxErrorMessage);
            }
        },
        error: function (data) {
            if (data.status !== 401 && data.status !== 403) {
                Notiflix.Notify.Failure(ajaxErrorMessage);
            }
        },
        complete: function () {
            Notiflix.Block.Remove("*", 0);
        }
    });


}

function setLayoutContainerHeight() {
    // Get top position of layout container, subtract from screen height, subtract a bit for padding
    if ($('#grid').length > 0) {
        var y = $('#grid').position().top;
        var layoutHeight = $(window).height() - y - 70;
        $('#grid').css('height', layoutHeight + 'px');
    }

}

// Whenever the window changes size, recalculate the layout container height
setLayoutContainerHeight();
$(window).resize(setLayoutContainerHeight);
