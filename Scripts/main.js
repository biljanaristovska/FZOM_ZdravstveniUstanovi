var circ;
var mylatlng;
var pharmacies = [];
var map;

//Оваа функција служи да го прилагодиме изгледот и содражината според димензиите на уредот на  кој ќе се прикажува апликацијата
$(window).resize(function () {
    /* if ($('.contentF').height() < $(window).height() / 1.2)
    $('.contentF').height($(window).height());*/
    
    if ($('#popup').is(":visible")) {
        $('#popup').bPopup().close();
    }

    if ($('#popupDejnost').is(":visible")) {
        $('#popupDejnost').bPopup().close();
    }

    //Ако должината на уредот и помала од 480 пиксели
    if ($(window).width() <= 480) {
        $('.txtVnesiIme').width(($(window).width() - 80));
        $('.aGrad').width(($(window).width() - 69));
        $('.aDejnost').width(($(window).width() - 69));
        $('.btnBaraj').width(($(window).width() - 69));
        $('.ulGradovi').css("width", $(window).width() - 69);
        $('.ulDejnosti').css("width", $(window).width() - 69);
        $('#popup').css("width", $(window).width() - 69);
        $('#popupDejnost').css("width", $(window).width() - 69);
        $('#h3MapPocetna').width(($(window).width() - 67));
        $('.prebarajAptekiBukva').width($(window).width() - 67);
        $('.backBig').hide();
        $('.backSmall').show();
        $('.homeBig').hide();
        $('.homeSmall').show();
        var headerWid = $('.headerF').width();
        var logoWid = $('.spanLogo').width();
        $('.headerF .spanLogo').css("left", (headerWid - logoWid) / 2);
        $('.winSizeLargerThen480').hide();
        $('.winSizeSmallerThen480').show();
    }
    //Ако должината на уредот и поголема од 480 пиксели
    else {
        $('.txtVnesiIme').width(($(window).width() - 80));
        $('.aGrad').width(($(window).width() - 69));
        $('.aDejnost').width(($(window).width() - 69));
        $('.btnBaraj').width(($(window).width() - 69));
        $('#h3MapPocetna').width(($(window).width() - 67));
        $('.prebarajAptekiBukva').width($(window).width() - 67);
        var divWid = $('.divGradovi').width();
        var liWid = $('.ulGradovi li a').width();
        $('.ulGradovi').css("left", (divWid - liWid) / 2);
        $('.ulGradovi li a').css("text-align", "center");
        var divWid1 = $('.divDejnosti').width();
        var liWid1 = $('.ulDejnosti li a').width();
        $('.ulDejnosti').css("left", (divWid1 - liWid1) / 2);
        $('.ulDejnosti li a').css("text-align", "center");
        $('.backBig').show();
        $('.backSmall').hide();
        $('.homeBig').show();
        $('.homeSmall').hide();
        var headerWid = $('.headerF').width();
        var logoWid = $('.spanLogo').width();
        $('.headerF .spanLogo').css("left", (headerWid - logoWid) / 2);
        $('.winSizeSmallerThen480').hide();
        $('.winSizeLargerThen480').show();
    }
});
//Во оваа ф-ја ги правиме истите работи од погоре со тоа што го прилагодиме изгледот и содражината според димензиите на уредот на  кој ќе се прикажува апликацијата кога прв пат ќе се
//пушти апликацијата и DOM ќе биде целосно вчитан
$(document).ready(function () {
    /*if ($('.contentF').height() < $(window).height()/1.2)
    $('.contentF').height($(window).height());*/
    if ($('#popup').is(":visible")) {
        $('#popup').bPopup().close();
    }
    if ($('#popupDejnost').is(":visible")) {
        $('#popupDejnost').bPopup().close();
    }

    if ($(window).width() <= 480) {
        $('.txtVnesiIme').width(($(window).width() - 80));
        $('.aGrad').width(($(window).width() - 69));
        $('.aDejnost').width(($(window).width() - 69));
        $('.btnBaraj').width(($(window).width() - 69));
        $('#h3MapPocetna').width(($(window).width() - 67));
        $('.prebarajAptekiBukva').width($(window).width() - 67);
        $('.backBig').hide();
        $('.backSmall').show();
        $('.ulGradovi').css("width", $(window).width() - 69);
        $('.ulDejnosti').css("width", $(window).width() - 69);
        $('#popup').css("width", $(window).width() - 69);
        $('#popupDejnost').css("width", $(window).width() - 69);
        $('.homeBig').hide();
        $('.homeSmall').show();
        var headerWid = $('.headerF').width();
        var logoWid = 128;
        $('.headerF .spanLogo').css("left", (headerWid - logoWid) / 2);
        $('.winSizeLargerThen480').hide();
        $('.winSizeSmallerThen480').show();
    }
    else {
        $('.txtVnesiIme').width(($(window).width() - 80));
        $('.aGrad').width(($(window).width() - 69));
        $('.aDejnost').width(($(window).width() - 69));
        $('.btnBaraj').width(($(window).width() - 69));
        $('#h3MapPocetna').width(($(window).width() - 67));
        $('.prebarajAptekiBukva').width($(window).width() - 67);
        var divWid = $('.divGradovi').width();
        var liWid = $('.ulGradovi li a').width();
        $('.ulGradovi').css("left", (divWid - liWid) / 2);
        $('.ulGradovi li a').css("text-align", "center");
        var divWid1 = $('.divDejnosti').width();
        var liWid1 = $('.ulDejnosti li a').width();
        $('.ulDejnosti').css("left", (divWid1 - liWid1) / 2);
        $('.ulDejnosti li a').css("text-align", "center");
        $('.backBig').show();
        $('.backSmall').hide();
        $('.homeBig').show();
        $('.homeSmall').hide();
        var headerWid = $('.headerF').width();
        var logoWid = 128;
        $('.headerF .spanLogo').css("left", (headerWid - logoWid) / 2);
        $('.winSizeSmallerThen480').hide();
        $('.winSizeLargerThen480').show();
    }
});




//Следниот код се извршува кога се кликне на копчето Најгоре кое го враќа корисникот на врвот од страната
$('body').clearQueue();
$('div').live('pagebeforecreate', function() {
    $('#uptotop').live('click', function() {
        $('html, body').animate({ scrollTop: '0px' }, 1500);
    });
});


//Следниот код се извршува со цел кога се внесе нешто во текст полето и истото се избрише но не со кликнување врз x-от од полето за да овој x што значи избришај да отстрани 
// бидејќи не е ништо внесено во текстуалното поле
$('.txtVnesiIme').live('keyup', function () {
   if ($('txtVnesiIme').val() != "") {
        $('#imgBrisi').fadeIn(250);
    }
    else {
        $('#imgBrisi').fadeOut(250);
    }
});

//Следниот код се извршува кога корисникот кликне на x-oт во текст полето со што полето добива фокус и се брише содражината која била впишана
$(document).ready(function () {
    $('#imgBrisi').live('click', function () {
        $('.txtVnesiIme').val("");
        $('.txtVnesiIme').focus();
        $(this).delay(250).fadeOut(250);
    });

});



$(document).ready(function () {
//Кога корисникот внесува нешто во текст полето за пребарување нема потреба да го менува фонтот туку директно се што пишува е на кирилица бидејќи и сите податоци се на кирилица во базата
    $('.txtVnesiIme').addClass('appliedCyrillic').cyrillic();
    $('.txtVnesiIme').focus(function () {
    //Кога корисникот пишува нешто ја менуваме бојата од сива која ја поставуваме кога полето нема фокус во црна кога полето има фокус
        if ($('.txtVnesiIme').val() != "" && $('.txtVnesiIme').val() != "Внеси име на аптека..")
        {
            $('.txtVnesiIme').css("color", "black");
        }
        else {
            $(this).val("");
            $('.txtVnesiIme').css("color", "black");
        }

    });
    //Ја враќеме бојата на она што е внесено(ако има било што) на сива бидејќи полето веќе нема фокус
    $('.txtVnesiIme').blur(function () {
        if ($('.txtVnesiIme').val() != ""){
            $('.txtVnesiIme').css("color", "rgb(128,128,128)");
            if ($('#imgBrisi').is(":visible") && $('.txtVnesiIme').val() == "Внеси име на аптека..")
                $('#imgBrisi').delay(250).fadeOut(250);}
        else {
            $('.txtVnesiIme').css("color", "rgb(128,128,128)");
            $('.txtVnesiIme').val("Внеси име на аптека..");
            if ($('#imgBrisi').is(":visible") && $('.txtVnesiIme').val() == "Внеси име на аптека..")
                $('#imgBrisi').delay(250).fadeOut(250);
        }
    });
});
//Во оваа ф-ја го поставуваме градот кој корисникот го одбрал како текст на копчето кое досега беше Избери град. Сега веќе е градот кој го одбрал.
//Исто така и додаваме опција Сите градови во случај корисникот да се предомисли и пак сака да пребарува по сите градови. Вушност оваа опција на почеток е само скриена. 
//Сега ја правиме само видлива на корисникот
function Grad(grad) {
    $('.spanGrad').text(grad);
    if (grad != "Сите Градови") {
        $(".liBerovo .ui-btn-text").text("Сите Градови");
        $(".liSiteGradovi .ui-btn-text").text("Берово");
        $('.liSiteGradovi').show();
        $('#popup').bPopup().close();
    }
  
    else {
        $(".liSiteGradovi .ui-btn-text").text("Сите Градови");
        $(".liBerovo .ui-btn-text").text("Берово");
        $('.spanGrad').text("Избери град");
        $('.liSiteGradovi').hide();
        $('#popup').bPopup().close();
    }
}


function Dejnost(dejnost) {
    $('.spanDejnost').text(dejnost);
    if (dejnost != "Сите Дејности") {
        $(".liSiteDejnosti .ui-btn-text").text("Сите Дејности");
        $(".liOpshta .ui-btn-text").text("ПЗЗ - Општа Медицина");
        $('.liSiteDejnosti').show();
        $('#popupDejnost').bPopup().close();
    }

    else {
        $(".liSiteDejnosti .ui-btn-text").text("Сите Дејности");
        $(".liOpshta .ui-btn-text").text("ПЗЗ - Општа Медицина");
        $('.spanDejnost').text("Избери дејност");
        $('.liSiteDejnosti').hide();
        $('#popupDejnost').bPopup().close();
    }
}



//Оваа ф-ја се повикува кога корисникот сака да се врати чекор наназад
function btnBackClick() {
    // za da ja zemam vrednota na hidden poleto -> slednite dva reda bidejki $('.btnHidden').val(); ne mi vrakase nisto
    var ind = $('.btnHidden').index(this);
    //Ја поставуваме оваа променлива со цел да знаеме каде да го вратиме корисникот во зависнот дали отишол во детали на аптеката од пребарување по буква или по град и/или име
    var odKade = $('.btnHidden').eq(ind).val();
    if ($(".divRezBaraj").is(':visible')) {
        $(".divRezBaraj").hide();
        $(".spanBack").hide();
        $('.spanHome').hide();
        $('.divPrebaruvanjeApteka').fadeIn(1000);
        $('.divMaps').fadeIn(1100);
        $('.divBarajPoBukva').fadeIn(1200);
    } else if ($('.divPoBukva').is(':visible')) {
        $(".divPoBukva").hide();
        $(".spanBack").hide();
        $('.spanHome').hide();
        $('.divPrebaruvanjeApteka').fadeIn(1000);
        $('.divMaps').fadeIn(1100);
        $('.divBarajPoBukva').fadeIn(1200);
    }
    else if($('.divInfoZaApteka').is(":visible") && odKade=="baraj") {
        $(".divInfoZaApteka").fadeOut(100);
        $(".divRezBaraj").fadeIn(700);
        if ($('.aMapaGoogle').text() == "Сокриј мапа") {
            $('.aMapaGoogle').text("Види мапа");
            $('#mapaApt').hide();
        }
    }
    else if ($('.divInfoZaApteka').is(":visible") && odKade == "barajPoBukva") {
        $(".divInfoZaApteka").fadeOut(100);
        $(".divPoBukva").fadeIn(700);
        if ($('.aMapaGoogle').text() == "Сокриј мапа") {
            $('.aMapaGoogle').text("Види мапа");
            $('#mapaApt').hide();
        }
    }
}
//Ф-ја со која се корисникот се враќа во положба како на почетокот кога прв пат ја отворил апликацијата
function vratiPocetna() {
    window.location.href = 'Home';
    $(".divPoBukva").hide("slow");
    $(".divRezBaraj").hide("slow");
    $(".divBarajPoBukva").hide("slow");
    $('.divInfoZaApteka').hide("slow");
    $(".spanBack").hide("slow");
    $('.spanHome').hide();
        $('.divPrebaruvanjeApteka').fadeIn(1000);
    $(function () {
        if (navigator) {
            if (navigator.geolocation) {
                $('.divMaps').show();
                navigator.geolocation.getCurrentPosition(function (coord) {
                    $('#mapaPocetna').gmap({
                        'center': coord.coords.latitude + ',' + coord.coords.longitude,
                        'zoom': 16,
                        'disableDefaultUI': true,
                        'callback': function () {
                            var self = this;
                            self.addMarker({ 'position': this.get('map').getCenter() }).click(function () {
                                self.openInfoWindow({ 'content': '' }, this);
                            });
                        }
                    });
                });
            }
        }
    });
    $('.divMaps').fadeIn(1100);
    $('.divBarajPoBukva').fadeIn(1200);
    $(".liSiteGradovi .ui-btn-text").text("Сите Градови");
    $(".liBerovo .ui-btn-text").text("Берово");
    $('.spanGrad').text("Избери град");
    $('.spanDejnost').text("Избери дејност");
    $('.liSiteGradovi').hide();
    if ($('.aMapaGoogle').text() == "Сокриј мапа") {
        $('.aMapaGoogle').text("Види мапа");
        $('#mapaApt').hide();
    }
    
}





//Ф-ја за креирање на мапата т.е. лоцирање на аптеката при прегледување на информациите и кликање на линкот Види мапа
function googleMaps(longitude, latitude, name) {
 mapaApteki = new google.maps.LatLng(latitude, longitude);
                //opcii za mapata
                myOptions = {
                    zoom: 10,
                    center: mapaApteki,
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    noClear: false,
                    overviewMapControl: true
                };
                mapAp = new google.maps.Map(document.getElementById('map_canvas'), myOptions);
                //mylatlng = new google.maps.LatLng(coord.coords.latitude, coord.coords.longitude);
                map.setCenter(mapaApteki);
               var marker = new google.maps.Marker({
                   position: mapaApteki,
                    title: name,
                    map: mapAp,
                    info: name,
                    icon: 'Content/Images/pharmacy-building.png'
                });
            
}
//Оваа ф-ја се повикува на клик на Види мапа линкот кој е дел од детали при прелистувањ на аптеките
function otvoriMapa() {
    if ($('.aMapaGoogle').text() != "Сокриј мапа") {
        $('.aMapaGoogle').text("Сокриј мапа");
        $('#mapaApt').slideDown(1000);
        //$('#map_canvas').gmap('refresh'); 
    } else {
        $('.aMapaGoogle').text("Види мапа");
        $('#mapaApt').slideUp(1000);
    }
}
//Оваа ф-ја ги зема координатите на корисникот со цел да го лоцира и му ги прикаже сите аптеки кои се во негова близина
$(function () {
    if (navigator) {
        if (navigator.geolocation) {
            $('.divMaps').show();
            navigator.geolocation.getCurrentPosition(function (coord) {
                mylatlng = new google.maps.LatLng(coord.coords.latitude, coord.coords.longitude);
                //opcii za mapata
                myOptions = {
                    zoom: 10,
                    center: mylatlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    noClear: false,
                    overviewMapControl: true
                };
               
                map = new google.maps.Map(document.getElementById('mapaPocetna'), myOptions);
                //mylatlng = new google.maps.LatLng(coord.coords.latitude, coord.coords.longitude);
                var marker = new google.maps.Marker({
                    position: mylatlng,
                    title: 'Моја Локација',
                    map: map,
                    info: 'Моја локација'
                });
                //map.addMarker(marker);
            });
        }
    }
});
//Оваа ф-ја е за поставување на одрдени својства на popup-oт со листата на градови
$(document).ready(function () {
    $('.spanHome').hide();
    $('a.aGrad').click(function() {

        //Getting the variable's value from a link 
        var loginBox = $(this).attr('href');

        //Fade in the Popup
        $(loginBox).fadeIn(300);

        //Set the center alignment padding + border see css style
        var popMargTop = ($(loginBox).height() + 24) / 2;
        var popMargLeft = ($(loginBox).width() + 24) / 2;

        $(loginBox).css({
            'margin-top': -popMargTop,
            'margin-left': -popMargLeft
        });
 });
        $('a.close').live('click', function() {
            $('.login-popup').fadeOut(300);
        });
        return false;

    });



    //Оваа ф-ја е за поставување на одрдени својства на popup-oт со листата на дејности
    $(document).ready(function () {
        $('a.aDejnost').click(function () {

            //Getting the variable's value from a link 
            var loginBox = $(this).attr('href');

            //Fade in the Popup
            $(loginBox).fadeIn(300);

            //Set the center alignment padding + border see css style
            var popMargTop = ($(loginBox).height() + 24) / 2;
            var popMargLeft = ($(loginBox).width() + 24) / 2;

            $(loginBox).css({
                'margin-top': -popMargTop,
                'margin-left': -popMargLeft
            });
        });
        $('a.close').live('click', function () {
            $('.login-popup').fadeOut(300);
        });
        return false;

    });




    

    // за searchBasic

    


    function IsPharmacyInRadius(myLat, myLong, pLat, pLong, radius) {
        var myLatLng = new google.maps.LatLng(myLat, myLong);
        var dst = new google.maps.LatLng(pLat, pLong);
        var distance = google.maps.geometry.spherical.computeDistanceBetween(myLatLng, dst);
        if (distance <= radius)
            return true;
        else
            return false;
    }

    //funkcija koja sto go crta krugot okolu mojot marker so zadaden radius
    function setRadius() {
        var txtRadius = document.getElementById("txtRadius").value;
        var val = parseFloat(txtRadius);
        if (txtRadius != "" && txtRadius != null && val > 0) {

            var color = "#fff";
            var str = "#738F9B";
            if (circ) {
                circ.setMap(map);
                circ.setRadius(val);
            } 
            else {
                circ = new google.maps.Circle({
                    center: mylatlng,
                    fillColor: color,
                    fillOpacity: 0.4,
                    map: map,
                    radius: val,
                    strokeColor: str
                });
            }
            
        }
    }


    // za searchBasic

    function setRadiusBasic() {

        var val = 10000;       

            var color = "#fff";
            var str = "#738F9B";
            if (circ) {
                circ.setMap(map);
                circ.setRadius(val);
            }
            else {
                circ = new google.maps.Circle({
                    center: mylatlng,
                    fillColor: color,
                    fillOpacity: 0.4,
                    map: map,
                    radius: val,
                    strokeColor: str
                });
            }

        }
    
    function removeRadius() {
        $("#btnRadius").button("option", "label", "Постави");
        document.getElementById("btnRadius").onclick = function () { setRadius(); };
        $("#btnRadius").button("option", "icons", { primary: 'ui-icon-circle-check' });
        document.getElementById("txtRadius").value = "";
        if (circ) {
            circ.setMap(null);
            //filtriraj();
        }
    }
    function changeRadius() {
        $("#btnRadius").button("option", "label", "Постави");
        document.getElementById("btnRadius").onclick = function () { setRadius(); };
        $("#btnRadius").button("option", "icons", { primary: 'ui-icon-circle-check' });
    }


    //функција за креирање маркер и инфо прозорец
    function createMark(m_lat, m_long, name) {
        //kreiraj tocka
        blatlng = new google.maps.LatLng(m_lat, m_long);
        //opcii za markerot
        var marker = new google.maps.Marker({
            position: blatlng,
            title: name,
            map: map,
            info: name,
            icon: 'Content/Images/pharmacy-building.png'
        });

        //dodadi go markerot vo globalnata niza
        pharmacies.push(marker);
    }



    function searchBasic() {

        getPharmaciesInRadiusBasic();
    
    }


    function setPassword() {

        var x;

        var password = prompt("Внесете лозинка", "");
        //alert(password);
        if (password == "admin") {
            window.location.href = 'Maps';
        }
        
        else if (password!=null) {
            
            setPassword();
            
        }

    }


    function IsPharmacyInRadius(pLat, pLong, radius) {

        var dst = new google.maps.LatLng(pLat, pLong);
        var distance = google.maps.geometry.spherical.computeDistanceBetween(mylatlng, dst);
        if (distance <= radius) {

            return true;
        }
        else
            return false;
    }