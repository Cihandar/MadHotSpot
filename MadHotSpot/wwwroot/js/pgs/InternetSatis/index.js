//Variable---------------
var controllerName = "InternetSatis";
//Variable---------------

 function KontratGridOlustur (AcentaId) {

    //var columns = [
    //    { field: 'kodu', caption: 'İndirim Kodu', size: '250px' },
    //    { field: 'adi', caption: 'Adı', size: '150px'},
    //    { field: 'rezKTarihBas', caption: 'Pansiyon', size: '150px'},
    //    { field: 'rezKTarihSon', caption: 'OdaTipi', size: '150px'},
    //    { field: 'periodBas', caption: 'Per. Başlangıç', size: '150px' },
    //    { field: 'periodSon', caption: 'Per. Son', size: '150px' },
    //    { field: 'indTanimAdi', caption: 'İndirim Türü', size: '100%' },
    //    { field: 'tutar', caption: 'Oran', size: '100%' },
    //    { field: 'maxGun', caption: 'maxGun', size: '100%', hidden: true },
    //    { field: 'minGun', caption: 'minGun', size: '100%', hidden: true },
    //    {
    //        field: 'girisBazli',
    //        caption: 'Giriş Bazlı',
    //        size: '100%',
    //        render: function (record) {

    //            if (!record.pasif) return "<span style='color:#29D21B'>Evet</span>";
    //            else return "<span style='color:#D21B1B'>Hayır</span>";
    //        }
    //    },
 

    //];
    //var searches = [
    //    { type: 'text', field: 'kodu', caption: 'Kontrat Adı' },
    //    { type: 'dateTime', field: 'periodBas', caption: 'Period Başlangıç Tarihi' },
    //    { type: 'dateTime', field: 'periodSon', caption: 'Period Son Tarihi' },
    //    { type: 'text', field: 'adi', caption: 'İndirim Adı' },
 


    //];

     //GridDataTable(columns, searches, controllerName, null, null, "?AcentaId=" + AcentaId);

     var customItems = [{ type: 'button', id: 'Cakisan', text: 'Çakışan İndirimler', icon: 'fa fa-bolt' }];

     ToolbarCreate('Indirim', controllerName, customItems, CustomToolbarCallBack, "Create?AcentaId=" + AcentaId);
 
} 

function CakisanKontrol() {
    if (w2ui.grid.getSelection().toString() != null && w2ui.grid.getSelection().toString() != "") {
        ModalCallWithUrl("Indirim/GetAllIndirimCakisan?IndirimId=" + w2ui.grid.getSelection().toString(), "crud-modal", w2ui.grid.getSelection().toString());
    }
}

function CustomToolbarCallBack(event) {
    switch (event.target) {
        case 'Cakisan':
            CakisanKontrol();
            break;
    }
}



//function CustomToolbarCallBack(event) {
//    switch (event.target) {
//        case 'kontrat':
//            window.location.assign("/Kontrat?Id=" + w2ui.grid.getSelection().toString());
 
//            break;
//    }
//}