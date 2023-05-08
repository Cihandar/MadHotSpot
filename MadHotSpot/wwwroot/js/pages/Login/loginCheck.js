
$('#lang').change(function () {
    changeLang();
})


function changeLang() {
    debugger;
    let lang = $("#lang").val();
    var b_Misafir = "";
    var b_internet = "";
    var tabs_meet_tab = "";
    var tabs_staff_tab = "";
    var tabs_spa_tab = "";
    var tabs_gb_tab = "";
    var l_dogum_tarihi = "";
    var day = "";
    var month = "";
    var year = "";
    var l_odano = "";
    var roomNumber = "";
    var l_email_adresi = "";
    var guestEmail = "";
    var l_telefon_no = "";
    var guestPhoneNumber = "";

    switch (lang) {

        case "TR":
            b_Misafir = "Misafir";
            b_internet = "İnternet";
            tabs_meet_tab = "Toplantı";
            tabs_staff_tab = "Personel";
            tabs_spa_tab = "Spa";
            tabs_gb_tab = "Günü Birlik";
            l_dogum_tarihi = "Doğum Tarihiniz";
            day = "Gün";
            month = "Ay";
            year = "Yıl";
            l_odano = "Oda Numaranız";
            roomNumber = "Oda Numaranız";
            l_email_adresi = "Email Adresiniz";
            guestEmail = "Email Adresiniz";
            l_telefon_no = "Telefon Numaranız";
            guestPhoneNumber = "Telefon Numaranız";
            break;

        case "EN":
            b_Misafir = "Guest";
            b_internet = "Internet";
            tabs_meet_tab = "Meets";
            tabs_staff_tab = "Staff";
            tabs_spa_tab = "Spa";
            tabs_gb_tab = "Daily Guest";
            l_dogum_tarihi = "Your Birth Day";
            day = "Day";
            month = "Month";
            year = "Year";
            l_odano = "Room Number";
            roomNumber = "Room Number";
            l_email_adresi = "Your Email";
            guestEmail = "Your Email";
            l_telefon_no = "Your Phone Number";
            guestPhoneNumber = "Your Phone Number";
            break;

        case "DE":
            b_Misafir = "Gast";
            b_internet = "Internet";
            tabs_meet_tab = "Besprechung";
            tabs_staff_tab = "Personal";
            tabs_spa_tab = "Spa";
            tabs_gb_tab = "Täglicher Gast";
            l_dogum_tarihi = "Dein Geburtsdatum";
            day = "Tag";
            month = "Mond";
            year = "Jahr";
            l_odano = "Zimmernummer";
            roomNumber = "Zimmernummer";
            l_email_adresi = "Deine E-Mail";
            guestEmail = "Deine E-Mail";
            l_telefon_no = "Deine Telefonnummer";
            guestPhoneNumber = "Deine Telefonnummer";
            break;

        case "RU":
            b_Misafir = "Гость";
            b_internet = "Интернет";
            tabs_meet_tab = "Встреча";
            tabs_staff_tab = "Персонал";
            tabs_spa_tab = "Спа";
            tabs_gb_tab = "Ежедневный гость";
            l_dogum_tarihi = "Дата вашего рождения";
            day = "День";
            month = "Месяц";
            year = "Год";
            l_odano = "Номер вашей комнаты";
            roomNumber = "Номер вашей комнаты";
            l_email_adresi = "Электронная почта";
            guestEmail = "Электронная почта";
            l_telefon_no = "Ваш номер телефона";
            guestPhoneNumber = "Ваш номер телефона";
            break;
    }

    $("#b_Misafir").html(b_Misafir);
    $("#b_internet").html(b_internet);
    $("#tabs_meet_tab").html(tabs_meet_tab);
    $("#tabs_staff_tab").html(tabs_staff_tab);
    $("#tabs_spa_tab").html(tabs_spa_tab);
    $("#tabs_gb_tab").html(tabs_gb_tab);
    $("#l_dogum_tarihi").html(l_dogum_tarihi);
    $("#day").attr("placeholder", day);
    $("#month").attr("placeholder", month);
    $("#year").attr("placeholder", year);
    $("#l_odano").html(l_odano);
    $("#roomNumber").attr("placeholder", roomNumber);
    $("#l_email_adresi").html(l_email_adresi);
    $("#guestEmail").attr("placeholder", guestEmail);
    $("#l_telefon_no").html(l_telefon_no);
    $("#guestPhoneNumber").attr("placeholder", guestEmail);

}

function MisafirEmailTelefonKontrol() {

    var dtarih = "";
    if ($("#day").val().length < 2) dtarih = "0" + $("#day").val(); else dtarih = $("#day").val();
    if ($("#month").val().length < 2) dtarih = dtarih + ".0" + $("#month").val(); else dtarih = dtarih + "." + $("#month").val();
    dtarih = dtarih + "." + $("#year").val();

    Notiflix.Block.Standard('.login-box');
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "post",
        url: "/Login/LoginCheck",
        data: JSON.stringify({
            "FirmaId": $("#FirmaId").val(),
            "BirthDate": dtarih,
            "Email": $("#guestemail").val(),
            "PhoneNumber": $("#guestPhoneNumber").val(),
            "Password": $("#roomNumber").val(),
            "LoginType": 0,
            "Mac": $("#ClientMac").val(),
            "LocalIp": $("#ClientIp").val(),
            "RoomNumber": $("#roomNumber").val(),
            "HotspotType": "Guest",
            "UserName": dtarih,
            "IdNumber": ""
        }),
        success: function (result) {

            if (result.success) {
                Notiflix.Block.Remove("*", 0);
                location.href = result.message;
                return true;

            } else {
                Notiflix.Report.Failure("Bilgiler Eşleşmedi");
                Notiflix.Block.Remove("*", 0);
                return false;
            }
            Notiflix.Block.Remove("*", 0);
        }

    });


}

function ToplantiEmailTelefonKontrol() {

    Notiflix.Block.Standard('.login-box');
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "post",
        url: "/Login/LoginCheck",
        data: JSON.stringify({
            "FirmaId": $("#FirmaId").val(),
            "UserName": $("#meetIdNumber").val(),
            "Email": $("#meetEmail").val(),
            "PhoneNumber": $("#meetPhoneNumber").val(),
            "Password": $("#meetPassword").val(),
            "LoginType": 2,
            "Mac": $("#ClientMac").val(),
            "LocalIp": $("#ClientIp").val(),
            "BirthDate": "",
            "RoomNumber": "",
            "HotspotType": "Meet",
            "IdNumber": ""
        }),
        success: function (result) {

            console.log(result);

            if (result.success) {
                Notiflix.Block.Remove("*", 0);
                location.href = result.message;
            } else {
                Notiflix.Notify.Failure("Kullanıcı Adı veya Şifre Yanlış");
                Notiflix.Block.Remove("*", 0);
                return false;
            }

        }
    });
}

function SpaEmailTelefonKontrol() {

    Notiflix.Block.Standard('.login-box');
    $.ajax({
        type: "post",
        url: "/Login/LoginCheck",
        data: JSON.stringify({
            "FirmaId": $("#FirmaId").val(),
            "BirthDate": $("#meetIdNumber").val(),
            "Email": $("#meetEmail").val(),
            "PhoneNumber": $("#meetPhoneNumber").val(),
            "RoomNumber": $("#meetPassword").val(),
            "LoginType": 3
        }),
        success: function (result) {

            if (result.success) {

                return true;

            } else {
                Notiflix.Notify.Failure("Bilgiler Eşleşmedi");
                return false;
            }
            Notiflix.Block.Remove("*", 0);
        }

    });


}

function StaffKontrol() {


    Notiflix.Block.Standard('.login-box');
    var dtarih = "";
    if ($("#s_day").val().length < 2) dtarih = "0" + $("#s_day").val(); else dtarih = $("#s_day").val();
    if ($("#s_month").val().length < 2) dtarih = dtarih+".0" + $("#s_month").val(); else dtarih = dtarih+"."+ $("#s_month").val();
    dtarih = dtarih + "." + $("#s_year").val();
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "post",
        url: "/Login/LoginCheck",
        data: JSON.stringify({
            "FirmaId": $("#FirmaId").val(),
            "BirthDate": dtarih,
            "Email": "",
            "PhoneNumber": "",
            "Password": dtarih,
            "LoginType": 1,
            "Mac": $("#ClientMac").val(),
            "LocalIp": $("#ClientIp").val(),
            "RoomNumber": "",
            "HotspotType": "Guest",
            "UserName": $("#staffIdNumber").val(),
            "IdNumber": $("#staffIdNumber").val()
        }),
        success: function (result) {

            if (result.success) {
                Notiflix.Block.Remove("*", 0);
                location.href = result.message;
                return true;
            } else {
                Notiflix.Report.Failure("Bilgiler Eşleşmedi");
                Notiflix.Block.Remove("*", 0);
                return false;
            }
            Notiflix.Block.Remove("*", 0);
        }

    });


}

function MikrotikUcretli() {

    return false;
}

function MikrotikUcretsiz() {

    if ($("#MisafirEmailZorunlu").val() == "True") {
        if ($("#guestemail").val() == null) {
            Notiflix.Report.Failure("Lütfen Email Adresi Giriniz");
            return false;
        }
    }

    if ($("#MisafirTelefonZorunlu").val() == "True") {
        if ($("#guestPhoneNumber").val() == null) {
            Notiflix.Report.Failure("Lütfen Telefon Numaranızı Giriniz");
            return false;
        }
    }



    MisafirEmailTelefonKontrol();

    return false;
}

function MikrotikToplanti() {
    if ($("#ToplantiEmailZorunlu").val() == "true") {
        if ($("#meetEmail").val() == null || $("#meetEmail").val() == "") {
            Notiflix.Notify.Failure("Lütfen Email Adresi Giriniz");
            return false;
        }
    }

    if ($("#ToplantiTelefonZorunlu").val() == "true") {
        if ($("#meetPhoneNumber").val() == null || $("#meetPhoneNumber").val() == "") {
            Notiflix.Notify.Failure("Lütfen Telefon Numaranızı Giriniz");
            return false;
        }
    }


    ToplantiEmailTelefonKontrol();

    return false;
}

function MikrotikSpa() {

    if ($("#SpaEmailZorunlu").val() == "True") {
        if ($("#spaEmail").val() == null) {
            Notiflix.Notify.Failure("Lütfen Email Adresi Giriniz");
            return false;
        }
    }

    if ($("#SpaTelefonZorunlu").val() == "True") {
        if ($("#spaPhoneNumber").val() == null) {
            Notiflix.Notify.Failure("Lütfen Telefon Numaranızı Giriniz");
            return false;
        }
    }


    ToplantiEmailTelefonKontrol();


    return false;
}

function MikrotikPersonel() {
    StaffKontrol();
    return false;
}
