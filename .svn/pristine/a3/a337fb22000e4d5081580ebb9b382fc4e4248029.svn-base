//if (window.location.protocol !== 'https:' && window.location.hostname !== 'localhost') {
//    window.location = 'https://' + window.location.hostname + window.location.pathname + window.location.hash;
//}

var PopupList = ['customPopup', 'customPopup_1', 'customPopup_2', 'customPopup_3', 'customPopup_4'];
var PopupList_sub = ['popUpBG01', 'popUpBG01_1', 'popUpBG01_2', 'popUpBG01_3', 'popUpBG01_4'];
var PopupList_sub_item = ['popUpBG02', 'popUpBG02_1', 'popUpBG02_2', 'popUpBG02_3', 'popUpBG02_4'];
const Guid = '00000000-0000-0000-0000-000000000000';
const FormatCodeAsset = ':GG-:YY:MM-:SS-:RRR';

const F_SPEC = '9A191E52-582D-4F7B-99ED-108C79DF7123';
const F_MSDS = '00028684-4BE7-49BD-98C6-25A0378DEDB3';
const F_PICTURE = 'CE62610A-D937-4B9C-9658-3E510B593ACD';
const F_COA = 'BD07894B-D451-4EB0-9DB9-A0B0F1566CFE';
const F_TESTREPORT = '378388A5-E17B-4583-8E92-E44AFD5C459F';
const F_OLD = '00000000-0000-0000-0000-000000000000';

const regexpMail = /^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$/i;
const regexpNumber = /^[0-9]+$/;
const regexpDecimal = /^\d+(?:\.\d{1,2})?$/;

const empPictureType = ["jpg", "gif", "png"];

function mvcPatch(v) {
    return $('.mvcpath').val() + v;
}

function newGuid() {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
}
$.removeCookie('AssetSystemUSERProfile');

//$(window).on('beforeunload', function (e) {
//    return 'You need to Login to the system or not.';
//});

function NewGuid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function userPopup() {
    var vindex = -1;
    for (var i = 0; i < PopupList.length; i++) {
        if ($('body').find('.' + PopupList[i]).length > 0) {
            vindex = i;
        }
    }
    if (vindex == -1) {
        return vindex = 0;
    } else {
        return vindex + 1;
    }
};

function addCommas(nStr, point) {
    nStr = parseFloat(nStr).toFixed(point);
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

var ImageStatus = {
    Old: 0,
    New: 1,
    Delete: 2
}

var RecordStatus = {
    Cancel: 0,
    Prepare: 1,
    WaitingCheck: 2,
    Pending: 3,
    Approve: 4,
    Disapproval: 5,
    Edit: 6
};

var RecordStatusItem = {
    Return: 0,
    Owner: 1,
    Transfer: 2,
    Donate: 3,
    Sell: 4,
    Destroy: 5,
    New: 6,
    Pause: 7,
    Reuse: 8
};

var TransferAssetStep = {
    EmpOwner: 1,
    DepByManagers: 2,
    DepByOwner: 3,
    EmpOwnerNew: 4
};

function htmlEscape(str) {
    return str
        .replace(/&/g, '&amp;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;');
}

function htmlUnescape(str) {
    return str
        .replace(/&quot;/g, '"')
        .replace(/&#39;/g, "'")
        .replace(/&lt;/g, '<')
        .replace(/&gt;/g, '>')
        .replace(/&amp;/g, '&');
}

function html_br(str) {
    try {
        return str.replace(new RegExp('\r?\n', 'g'), '<br />');
    } catch (v) {
        return '';
    }
}


function getImgRecordStatusItem(v) {
    var vReturn;
    if (v === RecordStatusItem.Return) {
        vReturn = 'Image/Item_Return.png';
    } else if (v === RecordStatusItem.Owner) {
        vReturn = 'Image/Item_Owner.png';
    } else if (v === RecordStatusItem.Transfer) {
        vReturn = 'Image/Item_Transfer.png';
    } else if (v === RecordStatusItem.Donate) {
        vReturn = 'Image/Item_Donate.png';
    } else if (v === RecordStatusItem.Sell) {
        vReturn = 'Image/Item_Sell.png';
    } else if (v === RecordStatusItem.Destroy) {
        vReturn = 'Image/Item_Destroy.png';
    } else if (v === RecordStatusItem.New) {
        vReturn = 'Image/Item_New.png';
    } else if (v === RecordStatusItem.Pause) {
        vReturn = 'Image/Item_Pause.png';
    } else if (v === RecordStatusItem.Reuse) {
        vReturn = 'Image/Item_Reuse.png';
    }
    return vReturn;
}

function getImgRecordStatusDoc(v) {
    var vReturn;
    if (v === RecordStatus.Cancel) {
        vReturn = 'Image/Arrow_Delete.png';
    } else if (v === RecordStatus.Prepare) {
        vReturn = 'Image/Arrow_Redo.png';
    } else if (v === RecordStatus.WaitingCheck) {
        vReturn = 'Image/Arrow_Redo.png';
    } else if (v === RecordStatus.Pending) {
        vReturn = 'Image/Arrow_Redo.png';
    } else if (v === RecordStatus.Approve) {
        vReturn = 'Image/Arrow_Approve.png';
    } else if (v === RecordStatus.Disapproval) {
        vReturn = 'Image/Arrow_Cancel.png';
    } else if (v === RecordStatus.Edit) {
        vReturn = 'Image/Arrow_Undo.png';
    }
    return vReturn;
}

function getRecordStatus(v) {
    var vReturn;
    if (v === true) {
        vReturn = 'Image/true_tab.png';
    } else {
        vReturn = 'Image/false_tab.png';
    }
    return vReturn;
}

function getTransferAssetStepText(v) {
    var vReturn;
    if (v === TransferAssetStep.EmpOwner) {
        vReturn = 'ผู้ขอโอน';
    } else if (v === TransferAssetStep.DepByManagers) {
        vReturn = 'ผู้จัดการแผนก';
    } else if (v === TransferAssetStep.DepByOwner) {
        vReturn = 'แผนกผู้ดูแลทรัพย์สิน';
    } else if (v === TransferAssetStep.EmpOwnerNew) {
        vReturn = 'ผู้ถือครอบครองทรัพย์สินใหม่';
    }
    return vReturn;
};

var RowStatus = {
    Inactive: 0,
    Active: 1
};

var ModuleType = {
    IT: '8D783B11-5AAD-49C5-876D-8407E684E258',
    Administration: '18A9335C-FA2F-4D58-8834-8C5CF7269470'
}

var ASSET_DOCU_DocumentationOfAssets = {
    IDKey: '95D81286-4D3B-49CA-9BF5-EDE082EB8E38',
    LenderBy: 0,
    WitnessBy_1: 1,
    WitnessBy_2: 2
}

function cUser() {
    this.UserID;
    this.UserName;
    this.TitleThai;
    this.FNameThai;
    this.LNameThai;
    this.NNameThai;
    this.CompanysId;
    this.CompanysName;
    this.BranchId;
    this.BranchName;
    this.DepartmentId;
    this.DepartmentName;
    this.PositionId;
    this.PositionName;
}

cUser.prototype = {
    setData: function (vdata) {
        this.UserID = vdata.item.tranId;
        this.UserName = vdata.item.userName;
        this.TitleThai = vdata.item.titleThai;
        this.FNameThai = vdata.item.fNameThai;
        this.LNameThai = vdata.item.lNameThai;
        this.NNameThai = vdata.item.nNameThai;
        this.CompanysId = vdata.companyInfo.companysId;
        this.CompanysName = vdata.companyInfo.companysName;
        this.BranchId = vdata.companyInfo.branchId;
        this.BranchName = vdata.companyInfo.branchName;
        this.DisplayProfile = vdata.companyInfo.displayProfile;
        this.DepartmentId = vdata.item.DepartmentId;
        this.DepartmentName = vdata.item.DepartmentName;
        this.PositionId = vdata.item.PositionId;
        this.PositionName = vdata.item.PositionName;
    }
}

var User = new Object();

function beginLogin(vdata) {
    User = Object.create(cUser.prototype);
    User.setData(vdata);
}

function endLogout() {
    User = null;
    $.removeCookie('AssetSystemUSERProfile');
}

function checkUndefined(val) {
    if (val === undefined) {
        return true;
    } else if (val === null) {
        return true;
    } else {
        return false;
    }
}

function showLoginPage() {
    //$('.bodyDetail').css({ 'opacity': 0 }).load('Pages/Login.aspx', function () {
    $('#datashow').css({ 'opacity': 0 }).load('Pages/Login.aspx', function () {
        $(this).stop().animate({ 'opacity': 1 }, 400, function () {
            $.ShowCustomButton();
            endLogout();
        });
    });
}

function Logout() {
    $('.displayProfile').stop().animate({ 'right': '-50px', 'opacity': 0 }, 100, function () {
        $(this).css({ 'display': 'none' });

        //if ($('.select').length === 0) {
        //    showLoginPage();
        //} else {
        //$('.bodyDetail').stop().animate({ 'opacity': 0 }, 100, function () {
        $('#datashow').stop().animate({ 'opacity': 0 }, 400, function () {
            //$('.select').stop().animate({ 'background-color': 'transparent' }, 100, function () {
            showLoginPage();
            //}).removeClass('select');
        });
        //}

    });
}

function getEmp() {
    try {
        return JSON.parse($.cookie('UserEmp'));
    } catch (e) {
        return '';
    }
}

function setDataLogin(vdata) {
    beginLogin(vdata);
    if ($('#c1').is(':checked')) {
        $.cookie('AssetSystemTranId', User.UserID, { expires: 7 });
        $.cookie('AssetSystemUserId', User.UserName, { expires: 7 });
        $.cookie('AssetSystemChecked', true, { expires: 7 });

        $.cookie('AssetSystemCompanysId', User.CompanysId, { expires: 7 });
        $.cookie('AssetSystemCompanysName', User.CompanysName, { expires: 7 });
        $.cookie('AssetSystemBranchId', User.BranchId, { expires: 7 });
        $.cookie('AssetSystemBranchName', User.BranchName, { expires: 7 });
    } else {
        $.removeCookie('AssetSystemTranId');
        $.removeCookie('AssetSystemUserId');
        $.removeCookie('AssetSystemChecked');

        $.removeCookie('AssetSystemCompanysId');
        $.removeCookie('AssetSystemCompanysName');
        $.removeCookie('AssetSystemBranchId');
        $.removeCookie('AssetSystemBranchName');
    }
    $.removeCookie('AssetSystemUSERProfile');
    $.cookie('AssetSystemUSERProfile', JSON.stringify(User), { expires: 7 });

    $('.displayProfile_LoginName').text(User.TitleThai + User.FNameThai + ' ' + User.LNameThai + User.NNameThai);
    $('.displayProfile_Company').text(User.DisplayProfile);
    var showDetail = $.trim(User.PositionName).length === 0 ? '' : '<span class="spDisplay">ตำแหน่ง : </span>';
    $('.displayProfile_Position').text($.trim(User.PositionName).length === 0 ? '' : User.PositionName).prepend(showDetail);
    showDetail = $.trim(User.DepartmentName).length === 0 ? '' : '<span class="spDisplay">แผนก : </span>';
    $('.displayProfile_Department').text($.trim(User.DepartmentName).length === 0 ? '' : User.DepartmentName).prepend(showDetail);
    //$('.bodyDetail').stop().animate({ 'opacity': 0 }, 400, function () {
    $('#datashow').stop().animate({ 'opacity': 0 }, 400, function () {
        //$(this).empty();
        $(this).load('Pages/MainMenuList.html', function () {
            $('.nameModule').text('');
            $('.iconModule').empty();
            $(this).stop().animate({ 'opacity': 1 }, 200);
        });
    });
}

function setLogin() {
    $('input[type=text], input[type=password]').off('focus').on({
        focus: function () {
            $(this).select();
        }
    })
    if (!checkUndefined($.cookie('AssetSystemChecked'))) {
        $('#txtUserName').val($.cookie('AssetSystemUserId'));
        $('#textPassword').focus();
        $('#c1').prop('checked', true);
        $('#form1').data('CompanysId', $.cookie('AssetSystemCompanysId'));
        $('#form1').data('BranchId', $.cookie('AssetSystemBranchId'));
        $('.div_changeCompany').text($.cookie('AssetSystemCompanysName'));
        $('.div_changBranch').text($.cookie('AssetSystemBranchName'));
    } else {
        $('#txtUserName').focus();
        $('#form1').data('CompanysId', -1);
        $('#form1').data('BranchId', -1);
    }
}
function checkDisplay(val) {
    if (val === true) {
        return 'block';
    } else {
        return 'none';
    }
}

function CloneDataObjectToValue(val) {
    return JSON.parse(JSON.stringify(val));
}

//function openFile(event, voutput,_thisDiv) {
//    try {
//        var input = event.target;
//        //alert(input.files[0].type.match('image.*'))
//        if (input.files[0].type.match('image.*')) {
//            var reader = new FileReader();
//            reader.onload = function () {
//                var dataURL = reader.result;
//                //var output = $(voutput);
//                voutput.prop("src", dataURL);
//            };
//            reader.readAsDataURL(input.files[0]);
//        } else {
//            voutput.prop("src", "http://180dc.org/wp-content/uploads/2016/08/default-profile.png");
//            _thisDiv.remove();
//        }
//    } catch (ex) {       
//        //$(voutput).prop("src", "http://180dc.org/wp-content/uploads/2016/08/default-profile.png");
//        voutput.prop("src", "http://180dc.org/wp-content/uploads/2016/08/default-profile.png");
//    }
//};

//------------------------------------------------------------- เกี่ยวกับวันที่
function getDateNow() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    return dd + '/' + mm + '/' + yyyy;
}

function getDateCustom(v) {
    var today = new Date(v);
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    return dd + '/' + mm + '/' + yyyy;
}

function getDateCustom2(v) {
    var today = v === undefined ? new Date() : v;
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    return dd + '/' + mm + '/' + yyyy;
}

function getTimemCustom(v, s, t) {//---v(Date), s(H="ชั่วโมง", M="นาที", S="วินาที", F="HH:mm:ss"), t(1="1 หลัก", 2="2 หลัก")
    var today = new Date(v);
    var tHH = today.getHours();
    var tMM = today.getMinutes();
    var tSS = today.getSeconds();

    if (t === 2) {
        if (tHH < 10) {
            tHH = '0' + tHH;
        }
        if (tMM < 10) {
            tMM = '0' + tMM;
        }
        if (tSS < 10) {
            tSS = '0' + tSS;
        }
    }

    var vReturn = '';
    if (s === 'H') {
        vReturn = tHH;
    } else if (s === 'M') {
        vReturn = tMM;
    } else if (s === 'S') {
        vReturn = tSS;
    } else if (s === 'F') {
        vReturn = tHH + ':' + tMM + ':' + tSS;
    }
    return vReturn;
}

function getDateCustomWithTime(v) {
    var today = new Date(v);
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var tHH = today.getHours();
    var tMM = today.getMinutes();
    var tSS = today.getSeconds();

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    if (tHH < 10) {
        tHH = '0' + tHH;
    }
    if (tMM < 10) {
        tMM = '0' + tMM;
    }
    if (tSS < 10) {
        tSS = '0' + tSS;
    }
    return dd + '/' + mm + '/' + yyyy + ' ' + tHH + ':' + tMM + ':' + tSS;
}

function getDateJson(v) {//---จาก v="วันที่รูปแบบ JSON" --> "dd/MM/yyyy"
    return getDateCustom(parseInt(v.substring(6)));
}

function getDateJsonWithTime(v) {//---จาก v="วันที่รูปแบบ JSON" --> "dd/MM/yyyy HH:mm:ss"
    return getDateCustomWithTime(parseInt(v.substring(6)));
}

function getTimeJson(v, s, t) {//---จาก v="วันที่รูปแบบ JSON", s="ค่าที่ส่งกลับ[H:ชั่วโมง, M:นาที, S:วินาที, F:เวลาเต็มรูปแบบ]", t="จำนวนหน่วยที่แสดง[1:1 หน่วย, 2:2 หน่วย]"
    return getTimemCustom(parseInt(v.substring(6)), s, t);
}

function setDateJson(v) {//---จาก v="dd/MM/yyyy" --> Date
    var vReturn = v.split('/');
    return new Date(parseInt(vReturn[2]), parseInt(vReturn[1]) - 1, parseInt(vReturn[0]));
}

function setDateJsonTime(v, t) {//---จาก v="dd/MM/yyyy", t="HH:mm:ss" --> DateTime
    var vdate = v.split('/');
    t = t === undefined ? '00:00:00' : t;
    var tdate = t.split(':');
    return new Date(parseInt(vdate[2]), parseInt(vdate[1]) - 1, parseInt(vdate[0]), parseInt(tdate[0]), parseInt(tdate[1]), parseInt(tdate[2]));
}

function setDateJsonTime_DDMMyyyHHmm(v) {//---จาก v="dd/MM/yyyy HH:mm" --> DateTime
    var _date = v.split(' ')[0].split('/');
    var _time = v.split(' ')[1].split(':');
    return new Date(parseInt(_date[2]), parseInt(_date[1]) - 1, parseInt(_date[0]), parseInt(_time[0]), parseInt(_time[1]));
}

function getDateJsonTime_DDMMyyyyHHmm(v) {//---จาก v = วันที่รูปแบบ JSON --> "dd/MM/yyyy HH:mm"
    var _date = parseInt(v.substring(6));
    var today = new Date(_date);
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var tHH = today.getHours();
    var tMM = today.getMinutes();

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    if (tHH < 10) {
        tHH = '0' + tHH;
    }
    if (tMM < 10) {
        tMM = '0' + tMM;
    }
    return dd + '/' + mm + '/' + yyyy + ' ' + tHH + ':' + tMM;
}

function getDateJsonTimeToDateTime(v) {//---จาก v = วันที่รูปแบบ JSON --> datetime
    return setDateJsonTime_DDMMyyyHHmm(getDateJsonTime_DDMMyyyyHHmm(v));
}

function jsonToDateTime(v) {//---จาก v="วันที่รูปแบบ JSON" --> DateTime
    return new Date(parseInt(v.substring(6)));
}

function jsonToJavaDate(v) {//---จาก JSON --> javascript datetime
    return new Date(parseInt(v.substring(6))).toJSON();
}
function ShowDateToJavaDate(v) {//---จาก ShowDate --> javascript datetime
    return setDateJson(v).toJSON();
}
//-------------------------------------------------------------
function checkNullAndReturn(v) {
    return v === null || v === 'null' || v === '' ? -1 : v;
}

function CloneDataToValue(v) {
    return JSON.parse(JSON.stringify(v));
}

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
    return pattern.test(emailAddress);
};

function fileExtension(v) {
    var Patter = new RegExp(/\.([0-9a-z]+)(?=[?#])|(\.)(?:[\w]+)$/);
    return v.match(Patter)[0];
}

function getIconFile(v) {
    var ftext = new Array('.TXT');
    var fpdf = new Array('.PDF');
    var fword = new Array('.DOC', '.DOCX');
    var fexcel = new Array('.XLS', '.XLSX');
    var fpowerpoint = new Array('.PPT', '.PPTX');
    var fsound = new Array('.WAV', '.CDA', '.MP3', '.WMA', '.RA', '.MID');
    var fvideo = new Array('.AVI', '.XVID', '.MP4', '.MKV', '.MPEG', '.MPEG4', '.WMV', '.3GP');
    var fimage = new Array('.GIF', '.JPG', '.JPEG', '.PNG', '.TIF', '.PSD', '.BMP', '.EPS', '.PIC', '.RAW');
    var farchive = new Array('.ZIP', '.RAR', '.7Z');

    v = $.trim(fileExtension(v)).toUpperCase();
    var vReturn = '';
    if ($.inArray(v, ftext) !== -1) {
        vReturn = '<i class="fa fa-file-text-o"></i>';
    } else if ($.inArray(v, fpdf) !== -1) {
        vReturn = '<i class="fa fa-file-pdf-o"></i>';
    } else if ($.inArray(v, fword) !== -1) {
        vReturn = '<i class="fa fa-file-word-o"></i>';
    } else if ($.inArray(v, fexcel) !== -1) {
        vReturn = '<i class="fa fa-file-excel-o"></i>';
    } else if ($.inArray(v, fpowerpoint) !== -1) {
        vReturn = '<i class="fa fa-file-powerpoint-o"></i>';
    } else if ($.inArray(v, fsound) !== -1) {
        vReturn = '<i class="fa fa-file-audio-o"></i>';
    } else if ($.inArray(v, fvideo) !== -1) {
        vReturn = '<i class="fa fa-file-video-o"></i>';
    } else if ($.inArray(v, fimage) !== -1) {
        vReturn = '<i class="fa fa-file-image-o"></i>';
    } else if ($.inArray(v, farchive) !== -1) {
        vReturn = '<i class="fa fa-file-archive-o"></i>';
    } else {
        vReturn = '<i class="fa fa-file-o"></i>';
    }
    return vReturn;
}

(function ($) {
    $.fn.openPage = function (option) {
        var setting = $.extend({
            url: '',
            speed: 200,
            callback: function () { },
            data: {},
            type: 'post',
            title: ''
        }, option);

        if (setting.type === 'get')
            setting.data = null;

        return this.each(function () {
            if (setting.url == '') return;
            $(this).stop().animate({ 'opacity': 0 }, setting.speed, function () {
                //var _loading = '<div id="cssload-loader">';
                //_loading += '<div class="cssload-dot"></div>';
                //_loading += '<div class="cssload-dot"></div>';
                //_loading += '<div class="cssload-dot"></div>';
                //_loading += '<div class="cssload-dot"></div>';
                //_loading += '<div class="cssload-dot"></div>';
                //_loading += '<div class="cssload-dot"></div>';
                //_loading += '<div class="cssload-dot"></div>';
                //_loading += '<div class="cssload-dot"></div>';
                //_loading += '</div>';

                var _loading = '<div class="cssload-piano">';
                _loading += '<div class="cssload-rect1"></div>';
                _loading += '<div class="cssload-rect2"></div>';
                _loading += '<div class="cssload-rect3"></div>';
                _loading += '</div>';
                //var _loading = '<div class="openPageBGLoading"><img class="openPageLoading" src="Image/ajax-loader.gif" /></div>';
                //$(this).load(setting.url, setting.data, function () {
                //    $('div.userStyleButton').each(function (key, val) {
                //        $(val).CustomButton();
                //    });
                //    setting.callback();
                //    $(this).stop().animate({ 'opacity': 1 }, setting.speed);
                //});
                var _thisoffset = $(this).offset();
                var _thisTop = _thisoffset.top;
                var _thisLeft = _thisoffset.left;
                var _thisWidth = _thisLeft + $(this).outerWidth();
                var _thisheight = _thisTop + $(this).outerHeight();

                //var _mainBody = $(this).parents('body');
                var _mainBody = $('body#bodySystem');
                var _this = $(this);

                //_mainBody.remove('.openPageBGLoading').append(_loading).find('.openPageBGLoading')
                //_mainBody.remove('#cssload-loader').append(_loading).find('#cssload-loader')
                _mainBody.remove('.cssload-piano').append(_loading).find('.cssload-piano')
                    .css({
                        'left': _thisLeft + 'px',
                        'top': _thisTop + 'px'
                        //'width': _thisWidth + 'px',
                        //'height': _thisheight + 'px'

                    }).stop().animate({ 'opacity': 1 }, setting.speed, function () {
                        _this.load(setting.url, setting.data, function () {
                            var thisform = $(this).find('form');

                            $('div.userStyleButton').each(function (key, val) {
                                $(val).CustomButton();
                            });
                            //setting.callback();
                            _this.stop().animate({ 'opacity': 1 }, setting.speed, function () {
                                //$('.openPageBGLoading').stop().animate({ 'opacity': 0 }, setting.speed, function () {
                                //$('#cssload-loader').stop().animate({ 'opacity': 0 }, setting.speed, function () {
                                //    $(this).remove();
                                //});
                                $('.cssload-piano').stop().animate({ 'opacity': 0 }, setting.speed, function () {
                                    $(this).remove();
                                    $('.ui-timepicker-wrapper').remove();
                                    //alert(thisform.attr('id'));
                                    $('.title').text(setting.title);
                                    setting.callback(thisform);
                                });
                            });
                        });
                    });
                //    .stop().animate({ 'opacity': 0 }, setting.speed, function () {
                //    $(this).remove();
                //});
                //$(this).empty().append(_loading).stop().animate({ 'opacity': 1 }, setting.speed, function () {
                //    $(this).load(setting.url, setting.data, function () {
                //        $('div.userStyleButton').each(function (key, val) {
                //            $(val).CustomButton();
                //        });
                //    });
                //    setting.callback();
                //});
            });
        });
    };

    $.ShowCustomButton = function (option) {
        var setting = $.extend({
            buttonNew: false,
            buttonSearch: false,
            buttonNewDocuments: false,
            NewDocumentsClick: function (vthis) {
                $('.bodyDetail').openPage({
                    url: 'Pages/Transaction/T_DocumentsHistory/DocumentsHistoryAdd.aspx',
                    callback: function () {
                        //$('#hidDocumentsKey').val(vthis.attr('class'));
                        $('.documentCode').text(vthis.attr('xCode'));
                        $('.documentDescription').text(vthis.attr('xDescription'));
                        //$('.liBack').off('click').on({
                        //    click: function () {
                        //        $('.bodyDetail').openPage({
                        //            url: 'Pages/Transaction/T_DocumentsHistory/DocumentsHistoryMain.aspx'
                        //        });
                        //    }
                        //});
                        $('#form_DocumentsHistoryAdd').data('Status', 'A');
                        $('#form_DocumentsHistoryAdd').data('RowKey', Guid);
                        $('#form_DocumentsHistoryAdd').data('DocumentsKey', vthis.attr('class'));
                    }
                });
            },
            buttonBack: false,
            buttonSave: false,
            buttonSend: false,
            buttonApprove: false,
            buttonUnApprove: false,
            buttonReward: false,
            buttonPrinter: false,
            buttonRedo: false,
            buttonSort: false,
            buttonHistory: false,
            buttonExportExcel: false,
            buttonExportRemains: false,
            butCallback: function () { }
        }, option);

        $('.liAdd').css({ 'display': checkDisplay(setting.buttonNew) });
        $('.liSearch, .liSearchText').css({ 'display': checkDisplay(setting.buttonSearch) });
        $('.liAddList').css({ 'display': checkDisplay(setting.buttonNewDocuments) }).each(function (key, _this) {
            $.ajax({
                url: 'Ajax/Master/M_Documents/DocumentsByliaddList.ashx',
                type: 'post',
                dataType: 'json',
                data: { UserId: $('#hid_tranId').val() },
                success: function (data) {
                    $('.liAddList_list').remove();
                    var _html = '<div class="liAddList_list"><ul>';
                    $.each(data, function (key, val) {
                        _html += '<li class="' + val.RowKey + '" xCode="' + val.DocumentCode + '" xDescription="' + val.DocumentDescription + '">' + val.DocumentCode + ' : ' + val.DocumentDescription + '</li>';
                    });
                    _html += '</ul></div>';
                    $(_this).append(_html).off('mouseenter mouseleave').on({
                        mouseenter: function () {
                            $('.liAddList_list').css({ 'display': 'block' }).stop().animate({ 'opacity': 1 }, 100);
                        },
                        mouseleave: function () {
                            $('.liAddList_list').stop().animate({ 'opacity': 0 }, 100, function () {
                                $(this).css({ 'display': 'none' });
                            });
                        }
                    }).find('li').off('click').on({
                        click: function () {
                            setting.NewDocumentsClick($(this));
                        }
                    })
                }
            });
        });
        $('.liBack').css({ 'display': checkDisplay(setting.buttonBack) });
        $('.liSave').css({ 'display': checkDisplay(setting.buttonSave) });
        $('.liSend').css({ 'display': checkDisplay(setting.buttonSend) });
        $('.liApprove').css({ 'display': checkDisplay(setting.buttonApprove) });
        $('.liUnApprove').css({ 'display': checkDisplay(setting.buttonUnApprove) });
        $('.liReward').css({ 'display': checkDisplay(setting.buttonReward) });
        $('.liPrinter').css({ 'display': checkDisplay(setting.buttonPrinter) });
        $('.liRedo').css({ 'display': checkDisplay(setting.buttonRedo) });
        $('.liSort').css({ 'display': checkDisplay(setting.buttonSort) });
        $('.liHistory').css({ 'display': checkDisplay(setting.buttonHistory) });
        $('.liExportExcel').css({ 'display': checkDisplay(setting.buttonExportExcel) });
        $('.liExportRemains').css({ 'display': checkDisplay(setting.buttonExportRemains) });
        setting.butCallback();
    };

    $.fn.CustomButton = function () {
        return this.each(function () {
            $(this).off('mouseenter mouseleave').on({
                mouseenter: function () {
                    $(this).stop().animate({ 'background-color': '#3BC500' }, 100);
                },
                mouseleave: function () {
                    $(this).stop().animate({ 'background-color': '#2a8d00' }, 300);
                }
            })
        });
    };

    $.fn.CustomCalendar = function () {
        return this.each(function () {
            $(this).css({ 'text-align': 'center' }).off('keydown').on({
                keydown: function () {
                    return false;
                }
            }).datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'Image/Calendar.png',
            });
        });
    };

    $.fn.CustomSearchMaster = function (option) {
        var setting = $.extend({
            urlSearchPage: '',
            urlAddPage: '',
            searchWidth: '300px',
            addWidth: '300px',
            searchHeight: '300px',
            addHeight: '300px',
            searchCallback: function () { },
            addCallback: function () { },
            searchTitle: '',
            addTitle: '',
            keyValue: Guid,
            searchOnly: false,
            Only: false
        }, option);

        return this.each(function () {
            var _this = $(this);
            _this.css({ 'text-align': 'left' }).off('keydown').on({
                keydown: function () {
                    return false;
                }
            });
            var _html = '<div class="SearchMasterIcon">';

            if (setting.Only === false) {
                _html += '<img class="CSMasterSearch" src="Image/Find.png"  />';
                if (setting.searchOnly === false)
                    _html += '<img class="CSMasterNew" src="Image/New.png"  />';
            } else {
                _this.prop('readonly', true);
            }
            var hiddenId = _this.attr('class').split(' ');
            _html += '<input type="hidden" id="' + hiddenId[0] + '" />';
            _html += '</div>';
            $(_html).insertAfter(_this).find('#' + hiddenId[0]).val(setting.keyValue).parent('div').find('.CSMasterSearch').off('click').on({
                click: function () {
                    $.CustomPopup({
                        url: setting.urlSearchPage,
                        width: setting.searchWidth,
                        height: setting.searchHeight,
                        callback: setting.searchCallback,
                        title: setting.searchTitle
                    });
                }
            }).next('.CSMasterNew').off('click').on({
                click: function () {
                    $.CustomPopup({
                        url: setting.urlAddPage,
                        width: setting.addWidth,
                        height: setting.addHeight,
                        callback: setting.addCallback,
                        title: setting.addTitle
                    })
                }
            });
            //setting.callback();
        });
    };

    $.fn.CustomSearchMasterButtonOnly = function (option) {
        var setting = $.extend({
            urlSearchPage: '',
            searchWidth: '300px',
            searchHeight: '300px',
            searchCallback: function () { },
            searchTitle: ''
        }, option);

        return this.each(function () {
            var _this = $(this);
            _this.off('click').on({
                click: function () {
                    $.CustomPopup({
                        url: setting.urlSearchPage,
                        width: setting.searchWidth,
                        height: setting.searchHeight,
                        callback: setting.searchCallback,
                        title: setting.searchTitle
                    });
                }
            })
        });
    }

    $.CustomPopupReport = function (option) {
        var setting = $.extend({
            url: '',
            width: '95%',
            height: '95%',
            callback: function () { },
            data: null,
            title: ''
        }, option);

        var checkIndex = userPopup();
        var customPopup = PopupList[checkIndex];
        var popUpBG01 = PopupList_sub[checkIndex];
        var popUpBG02 = PopupList_sub_item[checkIndex];

        var _html = '<div class="' + customPopup + '"><div class="' + popUpBG01 + '">';
        _html += '</div>';
        _html += '<div class="' + popUpBG02 + '" style="width:' + setting.width + ';height:' + setting.height + ';overflow: hidden;"><div class="btnCloseReport">Exit</div>';
        _html += '<form id="form_sendvalue" action="' + setting.url + '" method="post" target="viframe">';
        _html += '<input type="hidden" id="viframe_value" name="viframe_value"></input>';
        _html += '</form>';
        _html += '</div></div>';

        var _iframe = '<iframe id="viframe" name="viframe" style="height:100%;width:100%;border-width:0px;"></iframe>';
        $('body').append(_html).find('.' + popUpBG02).each(function (keyV, valV) {
            $(valV).find('#viframe_value').val(setting.data);
        }).append(_iframe).find('#viframe').each(function (keyF, valF) {
            var thispopup = $(valF).find('form');
            $.each(thispopup.parent('div').children('link'), function (key, val) {
                var cssUrlNew = $(this).attr('href') + '?v=' + new Date().valueOf();
                $(this).prop('href', cssUrlNew);
            });
            setting.callback(thispopup);
            $('.' + popUpBG02).find('#form_sendvalue').submit();
            $('.' + popUpBG01).stop().animate({ 'opacity': 0.9 }, 300);
            $('.' + popUpBG02).stop().animate({ 'opacity': 1 }, 300, function () {
                $(this).find('.btnCloseReport').off('click').on({
                    click: function () {
                        $.CustomPopupRemove();
                    }
                });
            });
            if (setting.title !== '') {
                thispopup.find('.popUpTitle').text(setting.title);
            }
            $('bl,style').remove();
        });
    }

    $.CustomPopup = function (option) {
        var setting = $.extend({
            url: '',
            btnActin: '',
            width: '300px',
            height: '300px',
            callback: function () { },
            title: ''
        }, option);

        var checkIndex = userPopup();
        var customPopup = PopupList[checkIndex];
        var popUpBG01 = PopupList_sub[checkIndex];
        var popUpBG02 = PopupList_sub_item[checkIndex];

        var _html = '<div class="' + customPopup + '"><div class="' + popUpBG01 + '"></div>';
        _html += '<div class="' + popUpBG02 + '" style="width:' + setting.width + ';height:' + setting.height + '"></div></div>';
        $('body').append(_html).find('.' + popUpBG02).load(setting.url + '?v=' + new Date().valueOf(), function () {
            var thispopup = $(this).find('form');
            $.each(thispopup.parent('div').children('link'), function (key, val) {
                var cssUrlNew = $(this).attr('href') + '?v=' + new Date().valueOf();
                $(this).prop('href', cssUrlNew);
            });

            $('.' + popUpBG01).stop().animate({ 'opacity': 0.9 }, 300);
            $(this).stop().animate({ 'opacity': 1 }, 300, function () {
                $(this).find('#btuCancel').off('click').on({
                    click: function () {
                        $.CustomPopupRemove();
                    }
                });
                setting.callback(thispopup);
            });
            if (setting.title !== '') {
                thispopup.find('.popUpTitle').text(setting.title);
            }
            $('bl,style').remove();
        });

        //$.ajax({
        //    url: setting.url,
        //    cache: false,
        //    dataType:'html',
        //    success: function (html) {
        //        //var editHtml = $(html);
        //        //alert(editHtml.find('link').attr('href'));
        //        $('body').append(_html).find('.' + popUpBG02).append(html).each(function (key, val) {
        //            var thispopup = $(this).find('form');
        //            setting.callback(thispopup);
        //            $('.' + popUpBG01).stop().animate({ 'opacity': 0.9 }, 300);
        //            $(this).stop().animate({ 'opacity': 0.9 }, 300, function () {
        //                $(this).find('#btuCancel').off('click').on({
        //                    click: function () {
        //                        $.CustomPopupRemove();
        //                    }
        //                });
        //            });
        //            if (setting.title !== '') {
        //                thispopup.find('.popUpTitle').text(setting.title);
        //            }
        //            $('bl,style').remove();
        //        });
        //    }
        //})
    };

    $.CustomPopupRemove = function (option) {
        var setting = $.extend({
            callback: function () { }
        }, option);
        var checkIndex = userPopup();
        checkIndex = checkIndex != 0 ? checkIndex - 1 : 0;
        var customPopup = PopupList[checkIndex];
        var popUpBG01 = PopupList_sub[checkIndex];
        var popUpBG02 = PopupList_sub_item[checkIndex];

        var _this = $('.' + customPopup);
        _this.find('.' + popUpBG01 + ', .' + popUpBG02 + '').stop().animate({ 'opacity': 0 }, 300, function () {
            _this.remove();
            if ($(this).attr('class') === popUpBG02) {
                setting.callback();
            }
        });
    };

    $.fn.ShowLoading = function (option) {
        var setting = $.extend({
            action: 'show'
        }, option);

        return this.each(function () {
            var _this = $(this);
            _this.LoadingOverlay(setting.action, {
                image: mvcPatch('image/puff.svg'),
                color: 'rgba(44, 62, 80, 0.8)'
            });
        });
    }

    $.reqDataCheckUser = function (option) {
        var setting = $.extend({
            url: '',
            callback: function () { },
            data: {},
            //loanding: true,
            //loandingclose: true,
        }, option);

        //if (setting.loanding)
        //    $('#bodySystem').ShowLoading();

        $.ajax({
            url: setting.url,
            type: 'post',
            dataType: 'json',
            data: setting.data,
            success: function (data) {
                setting.callback(data);
                //if (setting.loandingclose)
                //    $('#bodySystem').ShowLoading({ action: "hide" });
            }
        });
    }

    $.reqData = function (option) {
        var setting = $.extend({
            url: '',
            callback: function () { },
            data: {},
            loanding: true,
            loandingclose: true,
        }, option);

        if (setting.loanding)
            $('#bodySystem').ShowLoading();

        $.ajax({
            url: setting.url,
            type: 'post',
            dataType: 'json',
            data: setting.data,
            success: function (data) {
                setting.callback(data);
                if (setting.loandingclose)
                    $('#bodySystem').ShowLoading({ action: "hide" });
            }
        });
    }

    $.reqFile = function (option) {
        var setting = $.extend({
            url: '',
            callback: function () { },
            data: new FormData(),
            loanding: true,
            loandingclose: true,
        }, option);

        if (setting.loanding)
            $('#bodySystem').ShowLoading();

        $.ajax({
            url: setting.url,
            type: 'post',
            dataType: 'json',
            data: setting.data,
            cache: false,
            processData: false,
            contentType: false,
            success: function (data) {
                setting.callback(data);
                if (setting.loandingclose)
                    $('#bodySystem').ShowLoading({ action: "hide" });
            }
        });
    }

    $.reqDownloadFile = function (option) {
        var setting = $.extend({
            url: '',
            callback: function () { },
            data: new FormData(),
            loanding: true,
            loandingclose: true,
        }, option);

        if (setting.loanding)
            $('#bodySystem').ShowLoading();

        $.ajax({
            url: setting.url,
            type: 'post',
            data: setting.data,
            cache: false,
            processData: false,
            contentType: false,
            success: function (data) {
                setting.callback(data);
                if (setting.loandingclose)
                    $('#bodySystem').ShowLoading({ action: "hide" });
            }
        });
    }

    $.fn.NumericOnly = function (option) {
        var setting = $.extend({
            decimalpoint: 2,
            maxnumber: -1
        }, option);
        return this.each(function () {
            if ($(this).val() === '') $(this).val((0).toFixed(setting.decimalpoint));
            $(this).off('keypress').on({
                keypress: function (evt, element) {
                    var charCode = (evt.which) ? evt.which : event.keyCode
                    if (
                        (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                        (charCode < 48 || charCode > 57))
                        return false;

                    return true;
                },
                focusout: function () {
                    try {
                        var _thisvalue = Number($(this).val());
                        if (isNaN(_thisvalue)) {
                            $(this).val((0).toFixed(setting.decimalpoint));
                        } else {
                            $(this).val(_thisvalue.toFixed(setting.decimalpoint));
                        }
                    } catch (ex) {
                        $(this).val((0).toFixed(setting.decimalpoint));
                    }
                    if (setting.maxnumber > -1) {
                        if (parseInt($(this).val()) > parseInt(setting.maxnumber)) {
                            $(this).val(setting.maxnumber);
                        } else if (parseInt($(this).val()) < 1) {
                            $(this).val(1);
                        }
                    }
                }
            })
        });
    };

    $.fn.CloneDataObject = function (option) {
        var setting = $.extend({
            dataKey: 'data',
            data: new Object()
        }, option);

        return this.each(function () {
            $(this).removeData(setting.dataKey).data(setting.dataKey, JSON.parse(JSON.stringify(setting.data)));
        });
    };

    $.fn.SetDataPost = function (option) {
        var setting = $.extend({
            data: new Object()
        }, option);

        return this.each(function () {
            var _this = $(this);
            $.each(Object.keys(setting.data), function (k, v) {
                _this.find('#' + v).remove();
                _this.append('<input type="hidden" name="' + v + '" id="' + v + '" value="' + Object.values(setting.data)[k] + '" />');
            });
        });
    };

    $.fn.TextEditCustom = function (option) {
        var setting = $.extend({

        }, option);

        return this.each(function () {
            $(this).Editor({
                'texteffects': true,
                'aligneffects': true,
                'textformats': true,
                'fonteffects': true,
                'actions': true,
                'insertoptions': true,
                'extraeffects': true,
                'advancedoptions': true,
                'screeneffects': true,
                'bold': true,
                'italics': true,
                'underline': true,
                'ol': false,
                'ul': false,
                'undo': false,
                'redo': false,
                'l_align': true,
                'r_align': true,
                'c_align': true,
                'justify': true,
                'insert_link': false,
                'unlink': false,
                'insert_img': false,
                'hr_line': false,
                'block_quote': false,
                'source': false,
                'strikeout': false,
                'indent': false,
                'outdent': false,
                //'fonts': fonts,
                'styles': false,
                'print': false,
                'rm_format': false,
                'status_bar': true,
                //'font_size': false,
                //'color': false,
                'splchars': false,
                'insert_table': false,
                'select_all': false,
                'togglescreen': false
            });
        });
    }

    $.EmpLogin = function (option) {
        var setting = $.extend({
            tranid: -1,
            callback: function () { }
        }, option);

        $.reqData({
            url: 'ajax/GetEmp.ashx',
            data: { tranId: setting.tranid },
            callback: function (data) {
                $.removeCookie('UserEmp');
                var v = data;
                v.birthday = jsonToDateTime(v.birthday);
                v.nameFull = v.title + v.fName + ' ' + v.lName;
                $.cookie('UserEmp', JSON.stringify(v), { expires: 7 });
                setting.callback();
            }
        });
    };

    $.EmpLoginByKey = function (option) {
        var setting = $.extend({
            tranRowKey: Guid,
            callback: function () { }
        }, option);

        $.reqData({
            url: 'ajax/GetEmp.ashx',
            data: { tranKey: setting.tranRowKey },
            callback: function (data) {
                $.removeCookie('UserEmp');
                var v = data;
                v.birthday = jsonToDateTime(v.birthday);
                v.nameFull = v.title + v.fName + ' ' + v.lName;
                $.cookie('UserEmp', JSON.stringify(v), { expires: 7 });
                setting.callback();
            }
        });
    };

    $.EmpLoginChk = function (option) {
        var settion = $.extend({
            cookie_name: 'UserEmp',
            callback: function () { }
        }, option);
        if (typeof $.cookie(settion.cookie_name) === 'undefined') {
            settion.callback(false);
        } else {
            settion.callback(true);
        }
    }

    $.getPermission = function (option) {
        var setting = $.extend({
            data: new Array(),
            callback: function () { }
        }, option);

        $.reqData({
            url: 'ajax/GetPermission.ashx',
            data: {
                tranKey: getEmp() === '' ? Guid : getEmp().RowKey,
                items: setting.data
            },
            callback: function (vdata) {
                setting.callback(vdata);
            }
        })
    }

    $.appImage = function (option) {
        var setting = $.extend({
            dfNew: true,
            dfUrl: '',
            dfKey: Guid,
            dfName: '#',
            eleMain: '#divImage',
            htmlDisplay: '<div><img class="imgClass" /><input type="file" name="fileName[]" style="display:none;" /></div>',
            callback: function () { }
        }, option);


        var _thisTemp = $('body#bodySystem');
        var _temp_delete = _thisTemp.find('div#div_temp').remove();
        var _temp = _thisTemp.append('<div id="div_temp" style="display:none;"></div>').find('#div_temp');

        var _thisEle = $(setting.eleMain);
        var _newId = 'f' + (new Date()).valueOf();
        var _html = setting.htmlDisplay.replace('#fid', _newId).replace('#datakey', setting.dfKey).replace('#fname', setting.dfName);

        _temp.append(_html);
        var _file = $('#' + _newId).find('input[type=file]');
        var _img = $('#' + _newId).find('img');
        var _fileName = $('#' + _newId).find('.fName');

        var chkSuccess = true;
        try {
            if (setting.dfNew) {
                _file.on('change', function (ev) {
                    try {
                        var input = ev.target;
                        if (input.files[0].type.match('image.*')) {
                            var reader = new FileReader();
                            reader.onload = function () {
                                var dataURL = reader.result;
                                _img.prop("src", dataURL);
                            };
                            reader.readAsDataURL(input.files[0]);
                            _fileName.text(input.files[0].name);
                            var _tt = _temp.find('#' + _newId);
                            _thisEle.append(_tt);
                            _temp.remove();
                        } else {
                            _temp.remove();
                        }
                    } catch (ex) {
                        _temp.remove();
                    }

                }).click();
            } else {
                var _tt = _temp.find('#' + _newId);
                _thisEle.append(_tt);
                _tt.find('img').prop("src", setting.dfUrl);
                //alert(setting.dfUrl);
            }
        } catch (ex2) {
            chkSuccess = false;
        }
        setting.callback(chkSuccess);
    }


}(jQuery));